// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax; variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        private Func<string, string> normalizeFunction = null;
        private Func<string, bool> validationFunction = null;
        private string normalizedFormulaString = null;
        private List<string> normalizedVariablesInFormula = new List<string>();


        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            // Set the normalize and validation functions so we can use them in other methods.
            normalizeFunction = normalize;
            validationFunction = isValid;

            // Check if the expression is syntactically valid.
            normalizedFormulaString = checkSyntax(formula);
        }



        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<Double> valStack = new Stack<Double>();
            Stack<String> opStack = new Stack<String>();

            List<String> tokens = GetTokens(normalizedFormulaString).ToList();
            foreach( string token in tokens )
            {
                String normalizedToken = normalizeFunction(token);
                Boolean isVariable = isVar(normalizedToken);
                double res;
                // If we have an integer
                if (Double.TryParse(token, out res))
                {
                    object multDivResError = doubleMultiplyDivide(res, valStack, opStack);
                    if ( multDivResError is FormulaError )
                    {
                        return multDivResError;
                    }
                }

                // If it's either the addition or subtraction operation
                else if (token.Equals("+") || token.Equals("-"))
                {
                    doubleAddSubtract(token, valStack, opStack);
                }

                // If we're looking at multiplication, division, or the start of a parenthetic statement.
                else if (token.Equals("*") || token.Equals("/") || token.Equals("("))
                {
                    opStack.Push(token);
                }

                // If we're at the end of the parenthetic statement.
                else if (token.Equals(")"))
                {
                    rightParenAddSubtract(valStack, opStack);
                    object rpMultDivResError = rightParenMultiplyDivide(valStack, opStack);
                    if ( rpMultDivResError is FormulaError )
                    {
                        return rpMultDivResError;
                    }
                }

                // If we have a variable
                else if (isVariable)
                {
                    double evaluatedVariable = lookup(normalizedToken);
                    object vMultDivResError = doubleMultiplyDivide(evaluatedVariable, valStack, opStack);
                    if ( vMultDivResError is FormulaError )
                    {
                        return vMultDivResError;
                    }
                }
            }

            double finalAnswer = 0;
            // There are no operations left, so the final answer is just the only thing left on the value stack.
            if (opStack.Count.Equals(0))
            {
                finalAnswer = valStack.Pop();
            }
            // There is still an operation, so apply the operation to the two remaining values. This becomes our final answer.
            else
            {
                Double valA = valStack.Pop();
                Double valB = valStack.Pop();
                String op = opStack.Pop().ToString();
              
                if (op.Equals("+"))
                {
                    finalAnswer = valB + valA;
                }
                else if (op.Equals("-"))
                {
                    finalAnswer = valB - valA;
                }
                
            }
            return finalAnswer;
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            return normalizedVariablesInFormula;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            return this.normalizedFormulaString;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens, which are compared as doubles, and variable tokens,
        /// whose normalized forms are compared as strings.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Formula)
            {
                return obj.ToString().Equals(this.ToString());
            }
            return false;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return f1.Equals(f2);
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return ! f1.Equals(f2);
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }


        /// <summary>
        /// Function to handle if we have reached the end of an expression inside parentheses.
        /// </summary>
        /// <param name="valStack">
        /// The current Stack of values we are dealing with. This will contain all of the values
        /// we will need to operate on.
        /// </param>
        /// <param name="opStack">
        /// The current Stack of operations we are dealing with. This will contain all of the operations
        /// that will need to be applied to the values. It will also contain the left parenthese that tells
        /// us when we need to stop computing.
        /// </param>
        private static void rightParenAddSubtract(Stack<Double> valStack, Stack<String> opStack)
        {
            if (valStack.Count > 1 && opStack.Count > 0 && (opStack.Peek().Equals("+") || opStack.Peek().Equals("-")))
            {
                Double val1 = valStack.Pop();
                Double val2 = valStack.Pop();

                String opPop = opStack.Pop();
                if (opPop.Equals("+"))
                {
                    valStack.Push(val2 + val1);
                }
                else if (opPop.Equals("-"))
                {
                    valStack.Push(val2 - val1);
                }
            }

            // The next thing remaining should be the left parenthese. Get rid of it.
            if (opStack.Peek().Equals("("))
            {
                opStack.Pop();
            }
        }

        /// <summary>
        /// The second part of dealing with a right parenthese. Goes through and computes items that were
        /// before the opening of the parenthese.
        /// </summary>
        /// <param name="valStack">
        /// The current Stack of values we are dealing with. This will contain all of the values
        /// we will need to operate on.
        /// </param>
        /// <param name="opStack">
        /// The current Stack of operations we are dealing with. This will contain all of the operations
        /// that will need to be applied to the values. It will also contain the left parenthese that tells
        /// us when we need to stop computing.
        /// </param>
        private static object rightParenMultiplyDivide(Stack<Double> valStack, Stack<String> opStack)
        {
            if (valStack.Count > 1 && opStack.Count > 0 && (opStack.Peek().Equals("*") || opStack.Peek().Equals("/")))
            {
                Double val1 = valStack.Pop();
                Double val2 = valStack.Pop();

                String opPop = opStack.Pop();

                if (opPop.Equals("*"))
                {
                    valStack.Push(val2 * val1);
                }
                else if (opPop.Equals("/"))
                {
                    if ( ! val1.Equals(0) )
                    {
                        valStack.Push(val2 / val1);
                    } else
                    {
                        return new FormulaError("Division by zero.");
                    }
                }
            }
            return true;
        }



        /// <summary>
        /// Function to handle multiplication or division when we already have both numbers that
        /// we need to apply the operation too.
        /// If we don't have both numbers, just push the operation onto the stack and we'll deal with it
        /// on the next pass.
        /// </summary>
        /// <param name="tokenDouble">
        /// The number that we are wanting to compute with.
        /// </param>
        /// <param name="valStack">
        /// The current Stack of values we are dealing with. This will contain all of the values
        /// we will need to operate on.
        /// </param>
        /// <param name="opStack">
        /// The current Stack of operations we are dealing with. This will contain all of the operations
        /// that will need to be applied to the values. It will also contain the left parenthese that tells
        /// us when we need to stop computing.
        /// </param>
        private static object doubleMultiplyDivide(Double tokenDouble, Stack<Double> valStack, Stack<String> opStack)
        {
            if (opStack.Count > 0 && (opStack.Peek().Equals("*") || opStack.Peek().Equals("/")))
            {
                String opPop = opStack.Pop();
                Double val = valStack.Pop();


                if (opPop.Equals("*"))
                {
                    valStack.Push(val * tokenDouble);
                }

                else if (opPop.Equals("/"))
                {
                    if ( ! tokenDouble.Equals( 0 ) )
                    {
                        valStack.Push(val / tokenDouble);
                    }
                    else
                    {
                        return new FormulaError("Error: Division by zero.");
                    }
                }
            }
            else
            {
                valStack.Push(tokenDouble);
            }
            return null;
        }


        /// <summary>
        /// Function to handle addition or subtraction when we already have both numbers that
        /// we need to apply the operation too.
        /// If we don't have both numbers, just push the operation onto the stack and we'll deal with it
        /// on the next pass.
        /// </summary>
        /// <param name="token">
        /// The operation that we are currently dealing with.
        /// </param>
        /// <param name="valStack">
        /// The current Stack of values we are dealing with. This will contain all of the values
        /// we will need to operate on.
        /// </param>
        /// <param name="opStack">
        /// The current Stack of operations we are dealing with. This will contain all of the operations
        /// that will need to be applied to the values. It will also contain the left parenthese that tells
        /// us when we need to stop computing.
        /// </param>
        private static void doubleAddSubtract(String token, Stack<Double> valStack, Stack<String> opStack)
        {
            if (opStack.Count > 0 && (opStack.Peek().Equals("+") || opStack.Peek().Equals("-")))
            {
                String opPop = opStack.Pop();
                Double firstVal = valStack.Pop();
                Double secondVal = valStack.Pop();


                if (opPop.Equals("+"))
                {
                    valStack.Push(secondVal + firstVal);
                }
                else if (opPop.Equals("-"))
                {
                    valStack.Push(secondVal - firstVal);
                }
            }
            opStack.Push(token);
        }

        /// <summary>
        /// Checks whether the formula has any syntax errors in it or not.
        /// If any syntax errors were found, it throws a FormulaFormatException
        /// Also, this puts all variables into a "normalizedVariablesInFormula" List
        /// that is useful in grabbing all of the variables in a formula.
        /// </summary>
        /// <param name="formula"></param>
        /// <returns>The normalized formula, with no spaces and all variables normalized</returns>
        private string checkSyntax( String formula )
        {
            List<String> tokens = GetTokens(formula).ToList();
            String normalizedFormula = "";

            verifyAtLeastOneToken(tokens.Count);
            verifyFirstToken(tokens.First());
            verifyLastToken(tokens.Last());

            // Variables to basically keep track of all of the parentheses we have found.
            // This is useful for keeping track of invalid or mismatched parentheses.
            Stack<String> leftParenStack = new Stack<String>();
            Stack<String> rightParenStack = new Stack<String>();

            String lastToken = null;
            double lastTokenIsDouble;
            double tokenIsDouble;
            foreach( String token in tokens )
            {
                // Check the last token and make sure the proper token follows it.
                // This checks the parenthesis following and the extra following rules.
                if (lastToken != null)
                {
                    if (lastToken.Equals("(") || isOperator(lastToken))
                    {
                        verifyParenthesisFollowing(token);
                    }
                    else if (lastToken.Equals(")") ||
                      Double.TryParse(lastToken, out lastTokenIsDouble) ||
                      isVar(lastToken))
                    {
                        verifyExtraFollowing(token);
                    }
                }


                String normalizedToken = normalizeFunction(token);
                Boolean tokenIsVar = isVar(normalizedToken);
                if (token.Equals("("))
                {
                    leftParenStack.Push(token);
                }
                else if (token.Equals(")"))
                {
                    rightParenStack.Push(token);
                    verifyRightParenRule(leftParenStack.Count, rightParenStack.Count);
                } else if ( ! ( isOperator( token ) 
                                || tokenIsVar 
                                || Double.TryParse(token, out tokenIsDouble) 
                              ) )
                {
                    throw new FormulaFormatException("Invalid token: " + token + ". " +
                        "Every token must either be a number, variable, parenthese, or operator.");
                }

                if (tokenIsVar)
                {
                    normalizedVariablesInFormula.Add(normalizedToken);
                    normalizedFormula += normalizedToken;
                }
                else
                {
                    normalizedFormula += token;
                }
                lastToken = normalizedToken;
            }
            verifyBalancedParenRule(leftParenStack.Count, rightParenStack.Count);
            return normalizedFormula;
            
        }
        
        /// <summary>
        /// Tests to see if the token we are looking at is a variable or not.
        /// </summary>
        /// <param name="token"></param>
        private bool isVar(String token)
        {
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            if ( Regex.IsMatch(token, varPattern) )
            {
                return validationFunction(token) && !isOperator(token);
            }
            return false;
        }

        /// <summary>
        /// Tests to see if the token we are looking at is an operator or not.
        /// </summary>
        /// <param name="token"></param>
        private static bool isOperator(String token)
        {
            return token.Equals("+") || token.Equals("-") || token.Equals("*") || token.Equals("/");
        }

        /// <summary>
        /// Tests to make sure that any token that comes after an opening parenthese or an operator
        /// must either be a number, variable or a left parenthese.
        /// </summary>
        /// <param name="token"></param>
        private void verifyParenthesisFollowing( string token )
        {
            double tokenDouble;
            if ( ! ( token.Equals("(" ) || Double.TryParse(token, out tokenDouble) || isVar( token ) ) )
            {
                throw new FormulaFormatException("Any token following an opening parenthese or operator must be a number, variable, or left parenthese.");
            }

        }

        /// <summary>
        /// Tests to make sure that the first token in a formula is a number, variable, or left parenthese.
        /// </summary>
        /// <param name="token"></param>
        private void verifyFirstToken( string token )
        {
            double tokenDouble;
            if ( ! ( token.Equals("(") || Double.TryParse(token, out tokenDouble) || isVar( token ) ) )
            {
                throw new FormulaFormatException("The first token in a formula must be a number, variable, or left parenthese.");
            }
        }

        /// <summary>
        /// Tests to make sure that the last token in a formula is a number, variable, or right parenthese.
        /// </summary>
        /// <param name="token"></param>
        private void verifyLastToken( string token )
        {
            double tokenDouble;
            if ( ! ( token.Equals(")") || Double.TryParse(token, out tokenDouble) || isVar( token ) ) )
            {
                throw new FormulaFormatException("The last token in a formula must be a number, variable, or right parenthese.");

            }
        }

        /// <summary>
        /// Tests to make sure that there is at least one token in our formula.
        /// </summary>
        /// <param name="count"></param>
        private static void verifyAtLeastOneToken( int count )
        {
            if ( count <= 0 )
            {
                throw new FormulaFormatException("You must have at least one token.");
            }
        }

        /// <summary>
        /// Tests to make sure that the number of left parentheses equals the number of right parentheses.
        /// </summary>
        /// <param name="leftParenCount"></param>
        /// <param name="rightParenCount"></param>
        private static void verifyBalancedParenRule( int leftParenCount, int rightParenCount )
        {
            if ( ! leftParenCount.Equals(rightParenCount) )
            {
                throw new FormulaFormatException("The number of open parentheses must be the same as the number of closing parentheses.");
            }

        }

        /// <summary>
        /// Tests to make sure that the number of right parentheses so far, has not exceeded the
        /// number of left parentheses so far.
        /// </summary>
        /// <param name="leftParenCount"></param>
        /// <param name="rightParenCount"></param>
        private static void verifyRightParenRule(int leftParenCount, int rightParenCount)
        {
            if ( rightParenCount > leftParenCount )
            {
                throw new FormulaFormatException("The number of closing parentheses must not be greater than the number of open parentheses.");
            }

        }

        /// <summary>
        /// Tests to make sure that any token that comes directly after a number, variable or closing
        /// parenthse is either an operator or another closing parenthese.
        /// </summary>
        /// <param name="token"></param>
        private static void verifyExtraFollowing(string token)
        {
            if ( ! ( token.Equals(")") || isOperator(token) ) )
            {
                throw new FormulaFormatException("Any token that immediately follows a number, variable, " +
                    "or closing parenthesis must be either an operator or closing parenthese.");
            }
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}
