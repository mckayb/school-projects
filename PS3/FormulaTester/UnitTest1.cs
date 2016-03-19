using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace FormulaTester
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests to make sure you can create a simple formula.
        /// </summary>
        [TestMethod]
        public void PublicTestConstructor()
        {
            Formula test = new Formula("X2 + y3 - 24");
            Assert.AreEqual(test.ToString(), "X2+y3-24");

            Formula test2 = new Formula("5");
            Assert.AreEqual(test2.ToString(), "5");
        }

        /// <summary>
        /// Tests to make sure you can pass in your own normalize and isValid functions.
        /// </summary>
        [TestMethod]
        public void PublicTestCustomFunctionsConstructor()
        {
            Formula test = new Formula("x2 + y3", normalize, isValid);
            Assert.AreEqual(test.ToString(), "X2+Y3");

            Formula test2 = new Formula("a33 + b334", normalize, isValid);
            Assert.AreEqual(test2.ToString(), "A33+B334");   
        }

        /// <summary>
        /// Tests to make sure that it throws an argument exception if there
        /// are undefined variables you are trying to look up.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PublicTestUndefinedVariables()
        {
            Formula a = new Formula("x3558", normalize, isValid);
            a.Evaluate(lookup);
        }

        /// <summary>
        /// Tests to make sure that underscores are allowed in the variable names.
        /// </summary>
        [TestMethod]
        public void PublicTestOtherAllowedVariableNames()
        {
            Double answer;
            Formula a = new Formula("_");
            Double.TryParse(a.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 400);
        }

        /// <summary>
        /// Tests to make sure that you can't just pass in an empty string to the formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestNoTokens()
        {
            Formula test = new Formula("");
        }

        /// <summary>
        /// Tests to make sure that you don't start a formula with a wrong operator.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestWrongStartingToken()
        {
            Formula test = new Formula("+ 7");
        }

        /// <summary>
        /// Tests to make sure that you don't end a formula with the wrong operator.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestWrongEndingToken()
        {
            Formula test = new Formula("8 - 2 -");
        }

        /// <summary>
        /// Tests to make sure the formula follows the parenthesis following rule
        /// defined in the assignment.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestParenthesisFollowingRule()
        {
            Formula test = new Formula("a1 + A1 + (-2)");
        }

        /// <summary>
        /// Tests to make sure the formula follows the parenthesis following rule
        /// defined in the assignment.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestParenthesisFollowingRule2()
        {
            Formula test = new Formula("a1 + A1 + ()");
        }

        /// <summary>
        /// Tests to make sure the formula follows the extra following rule defined
        /// in the assignment.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestExtraFollowingRule()
        {
            Formula test = new Formula("45(13)");
        }

        /// <summary>
        /// Tests to make sure the formula follows the extra following rule defined
        /// in the assignment.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestExtraFollowingRule2()
        {
            Formula test = new Formula("x86)");
        }

        /// <summary>
        /// Tests to make sure the formula follows the extra following rule defined
        /// in the assignment.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestExtraFollowingRule3()
        {
            Formula test = new Formula("a1 + )");
        }

        /// <summary>
        /// Tests to make sure you can't just have 'x' as a variable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestInvalidVariablesConstructor()
        {
            Formula test = new Formula("x + 2", normalize, isValid);
        }

        /// <summary>
        /// Function to make sure there is a formula format exception when there
        /// is an invalid variable listed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestInvalidVariablesConstructor2()
        {
            Formula test = new Formula("2x + y3", normalize, isValid);
        }

        /// <summary>
        /// Function to test if spaces between variables throws an exception. Shut that down!
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestInvalidSyntax()
        {
            Formula test = new Formula("22 + x 5");
        }

        /// <summary>
        /// Function to make sure there is an exception thrown if there
        /// are mismatched parentheses.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestMismatchedParentheses()
        {
            Formula test = new Formula("( a1 + A1 ))");
        }

        /// <summary>
        /// Function to make sure there is an exception thrown if there
        /// are mismatched parentheses.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestMismatchedParentheses2()
        {
            Formula test = new Formula("(( a1 + A1)");
        }

        /// <summary>
        /// Function to make sure that it handles mistakes when typing in
        /// parentheses for a formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestWrongParentheses()
        {
            Formula test = new Formula("6 + )2+3(");
        }

        /// <summary>
        /// Function to make sure it handles nested parentheses smoothly.
        /// </summary>
        [TestMethod]
        public void PublicTestExtraParentheses()
        {
            double answer;
            Formula test = new Formula("((((((((4)+(x86)-(2*1))))))))");
            Double.TryParse(test.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 10002);

        }

        /// <summary>
        /// Function to make sure an exception is thrown when there is a character
        /// that is not allowed, such as %.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PublicTestInvalidCharacters()
        {
            Formula test = new Formula("32%");
        }


        /// <summary>
        /// Function to test if GetVariables returns all the variables even
        /// when normalization is required.
        /// </summary>
        [TestMethod]
        public void PublicTestGetVariables()
        {
            Formula test = new Formula("x22+X22 + 44 - (18 / B7)", normalize, isValid);
            List<String> vars = test.GetVariables().ToList();
            List<String> correctVars = new List<String>()
            {
                "X22",
                "B7"
            };
            Assert.IsTrue(vars.Except(correctVars).Count().Equals(0));
        }

        /// <summary>
        /// Tests to see if the == and .Equals works properly.
        /// </summary>
        [TestMethod]
        public void PublicTestFormulaEquals()
        {
            Formula a = new Formula("X2+Y33");
            Formula b = new Formula("x2 + Y33", normalize, isValid);
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
        }

        /// <summary>
        /// Tests to see if the != operator works properly.
        /// </summary>
        [TestMethod]
        public void PublicTestFormulaNotEquals()
        {
            Formula a = new Formula("x2 + y3");
            Formula b = new Formula("X2+Y3");
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        /// <summary>
        /// Tests to determine basic answers to basic formulaic evaluations.
        /// </summary>
        [TestMethod]
        public void PublicTestEvaluateNoVariables()
        {
            double answer;

            Formula a = new Formula("5");
            Double.TryParse(a.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 5);

            Formula b = new Formula("12 + 24");
            Double.TryParse(b.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 36);

            Formula c = new Formula("8 - 2 - 4");
            Double.TryParse(c.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 2);

            Formula d = new Formula("150 * 2 / 50");
            Double.TryParse(d.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 6);
        }

        /// <summary>
        /// Test to determine if it can handle multiple variables, and if when normalizing,
        /// the variables turn out to be the same, it still evaluates it correctly.
        /// </summary>
        [TestMethod]
        public void PublicTestEvaluateWithVariables()
        {
            double answer;

            Formula a = new Formula("a1 + A1");
            Double.TryParse(a.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 35);

            Formula b = new Formula("a1 + A1", normalize, isValid);
            Double.TryParse(b.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 40);
        }

        /// <summary>
        /// Tests to determine if you can evaluate formulas properly
        /// when there are plenty of parentheses mixed in.
        /// </summary>
        [TestMethod]
        public void PublicTestEvaluateWithParentheses()
        {
            double answer;
            Formula a = new Formula("(a1 + A1) / 5");
            Double.TryParse(a.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 7);

            Formula b = new Formula("(( 3 * 7 ) - (2+4))/a1");
            Double.TryParse(b.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 1);

            Formula c = new Formula("( (a1 / 3) * (A1 * 2) / ( a1 - 5 ))");
            Double.TryParse(c.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(answer, 20);
        }

        /// <summary>
        /// Tests to make sure that we can handle floating point math, with
        /// numbers such as 5x10^3 (5e3 or 5E3)
        /// </summary>
        [TestMethod]
        public void PublicTestFloatingPointMath()
        {
            double answer;
            Formula a = new Formula("5e1 - 4E1");
            Double.TryParse(a.Evaluate(lookup).ToString(), out answer);
            Assert.AreEqual(10, answer);
        }

        /// <summary>
        /// Test to determine if GetHashCode returns the same value, when they end up
        /// being the same formula string.
        /// </summary>
        [TestMethod]
        public void PublicTestGetHashCode()
        {
            Formula a = new Formula("X2+Y33");
            Formula b = new Formula("x2 + Y33", normalize, isValid);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            Formula c = new Formula("x2 + y3");
            Formula d = new Formula("X2+Y3");
            Assert.AreNotEqual(c.GetHashCode(), d.GetHashCode());
        }

        /// <summary>
        /// Test to determine if the code catches division by 0 mishaps.
        /// </summary>
        [TestMethod]
        public void PublicTestDivisionByZero()
        {
            Formula a = new Formula("8 / 0");
            Assert.IsInstanceOfType(a.Evaluate(lookup), typeof(FormulaError));

            Formula b = new Formula("a1 / f22");
            Assert.IsInstanceOfType(b.Evaluate(lookup), typeof(FormulaError));

            Formula c = new Formula("(a1 / 0)");
            Assert.IsInstanceOfType(a.Evaluate(lookup), typeof(FormulaError));
        }

        /// <summary>
        /// A test lookup function. Just returns the value of whichever variable you want in it.
        /// If the variable doesn't exist, it throws an argument exception.
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        private double lookup( string var )
        {
            Dictionary<String, Double> values = new Dictionary<String, Double>()
            {
                { "a1", 15 },
                { "A1", 20 },
                { "x86", 10000 },
                { "f22", 0 },
                { "_", 400 }
            };

            if ( values.ContainsKey( var ) )
            {
                return values[var];
            } else
            {
                throw new ArgumentException("Couldn't find the word in the dictionary.");
            }
        }

        /// <summary>
        /// A test validation function. This also validates that the formula variables
        /// must start with exactly one letter followed by exactly one number.
        /// </summary>
        /// <param name="formula">The current formula string.</param>
        private bool isValid(string formula)
        {
            var pattern = @"[a-zA-Z][0-9]";
            return Regex.IsMatch(formula, pattern);
        }

        /// <summary>
        /// A test normalization function. This makes every variable in the string upper case.
        /// </summary>
        /// <param name="formula">The current formula string</param>
        private string normalize(string formula)
        {
            return formula.ToUpper();
        }
    }
}
