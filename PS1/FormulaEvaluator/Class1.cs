using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    /// <summary>
    /// This class is used to evaluate strings containing various operations,
    /// such as "5*(3+2)" and return the result.
    /// </summary>
    public static class Evaluator
    {

        /// <summary>
        /// Delegate to help with the evaluation. This is used
        /// as a way of finding out variables and their values.
        /// </summary>
        /// <param name="v">
        /// String containing which variable you want to find the value of.
        /// </param>
        /// <returns>
        /// The value of the variable parameter.
        /// </returns>
        public delegate int Lookup(String v);

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
        private static void rightParenAddSubtract(Stack valStack, Stack opStack)
        {
            if (valStack.Count > 1 && opStack.Count > 0 && (opStack.Peek().Equals("+") || opStack.Peek().Equals("-")))
            {
                Double val1, val2;
                Boolean val1IsNum = Double.TryParse(valStack.Pop().ToString(), out val1);
                Boolean val2IsNum = Double.TryParse(valStack.Pop().ToString(), out val2);

                String opPop = opStack.Pop().ToString();
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
            else
            {
                throw new Exception("Did you forget to end your parentheses?");
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
        private static void rightParenMultiplyDivide(Stack valStack, Stack opStack)
        {
            if (valStack.Count > 1 && opStack.Count > 0 && (opStack.Peek().Equals("*") || opStack.Peek().Equals("/")))
            {
                Double val1, val2;
                Boolean val1IsNum = Double.TryParse(valStack.Pop().ToString(), out val1);
                Boolean val2IsNum = Double.TryParse(valStack.Pop().ToString(), out val2);

                String opPop = opStack.Pop().ToString();

                if (opPop.Equals("*"))
                {
                    valStack.Push(val2 * val1);
                }
                else if (opPop.Equals("/"))
                {
                    valStack.Push(val2 / val1);
                }
            }
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
        private static void integerMultiplyDivide(Double tokenDouble, Stack valStack, Stack opStack)
        {
            if (opStack.Count > 0 && (opStack.Peek().Equals("*") || opStack.Peek().Equals("/")))
            {
                String opPop = opStack.Pop().ToString();
                String valPop = valStack.Pop().ToString();

                double val;
                Boolean valIsNum = Double.TryParse(valPop, out val);

                if (valIsNum)
                {
                    if (opPop.Equals("*"))
                    {
                        valStack.Push(val * tokenDouble);
                    }

                    else if (opPop.Equals("/"))
                    {
                        valStack.Push(val / tokenDouble);
                    }
                }
            }
            else
            {
                valStack.Push(tokenDouble.ToString());
            }
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
        private static void integerAddSubtract(String token, Stack valStack, Stack opStack)
        {
            if (opStack.Count > 0 && (opStack.Peek().Equals("+") || opStack.Peek().Equals("-")))
            {
                String opPop = opStack.Pop().ToString();
                String firstValPop = valStack.Pop().ToString();
                String secondValPop = valStack.Pop().ToString();

                double firstVal, secondVal;
                Boolean firstIsNum = Double.TryParse(firstValPop, out firstVal);
                Boolean secondIsNum = Double.TryParse(secondValPop, out secondVal);
                if (firstIsNum && secondIsNum)
                {
                    if (opPop.Equals("+"))
                    {
                        valStack.Push(secondVal + firstVal);
                    }
                    else if (opPop.Equals("-"))
                    {
                        valStack.Push(secondVal - firstVal);
                    }
                    opStack.Push(token);
                }
            }
            else
            {
                opStack.Push(token);
            }

        }



        /// <summary>
        /// Function to evaluate a string such as "5*(3+2)" and return the result.
        /// </summary>
        /// <param name="exp">
        /// The expression you want evaluated.
        /// </param>
        /// <param name="variableEvaluator">
        /// The lookup function used to find the value of any variables that have been passed in.
        /// </param>
        /// <returns>
        /// The answer to the evaluated expression.
        /// </returns>
        public static double Evaluate(String exp, Lookup variableEvaluator)
        {
            Stack valStack = new Stack();
            Stack opStack = new Stack();

            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            for (int i = 0; i < substrings.Length; i++)
            {
                // Variable names must be one or more letters followed by one or more numbers.
                // This is what we match off of.
                String trimmedSubstring = substrings[i].Trim();
                bool isVariable = Regex.IsMatch(trimmedSubstring, "^([a-zA-Z])+([0-9])+$");

                double res;
                // If we have an integer
                if (Double.TryParse(substrings[i], out res))
                {
                    integerMultiplyDivide(res, valStack, opStack);
                }

                // If we have a variable
                else if (isVariable)
                {
                    double variable;
                    Object evaluatedVariable = variableEvaluator(trimmedSubstring);
                    if ( Double.TryParse(evaluatedVariable.ToString(), out variable))
                    {
                        integerMultiplyDivide(variable, valStack, opStack);
                    } else
                    {
                        throw new KeyNotFoundException("Variable not found in the lookup function.");
                    }
                }

                // If it's either the addition or subtraction operation
                else if (substrings[i].Equals("+") || substrings[i].Equals("-"))
                {
                    integerAddSubtract(substrings[i], valStack, opStack);
                }

                // If we're looking at multiplication, division, or the start of a parenthetic statement.
                else if (substrings[i].Equals("*") || substrings[i].Equals("/") || substrings[i].Equals("("))
                {
                    opStack.Push(substrings[i]);
                }

                // If we're at the end of the parenthetic statement.
                else if (substrings[i].Equals(")"))
                {
                    rightParenAddSubtract(valStack, opStack);
                    rightParenMultiplyDivide(valStack, opStack);
                }
            }

            double finalAnswer = 0;
            // There are no operations left, so the final answer is just the only thing left on the value stack.
            if (opStack.Count.Equals(0))
            {
                Double.TryParse(valStack.Pop().ToString(), out finalAnswer);
            }
            // There is still an operation, so apply the operation to the two remaining values. This becomes our final answer.
            else
            {
                double valA, valB;
                Boolean valAIsNum = Double.TryParse(valStack.Pop().ToString(), out valA);
                Boolean valBIsNum = Double.TryParse(valStack.Pop().ToString(), out valB);
                String op = opStack.Pop().ToString();
                if (valAIsNum && valBIsNum)
                {
                    if (op.Equals("+"))
                    {
                        finalAnswer = valB + valA;
                    }
                    else if (op.Equals("-"))
                    {
                        finalAnswer = valB - valA;
                    }
                }
            }
            return finalAnswer;
        }
    }
}
