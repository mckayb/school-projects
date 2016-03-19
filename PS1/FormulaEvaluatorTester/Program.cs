using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaEvaluator;


namespace FormulaEvaluatorTester
{

    /// <summary>
    /// Class used to test the Evaluate Function from the Evaluator Class Library.
    /// </summary>
    class EvaluatorTester
    {
        /// <summary>
        /// Runs a few quick tests to make sure everything works as expected.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int failedCount = 0;
            int passedCount = 0;
            Double test;
            String exp;

            exp = "2*(8/2)";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 8, test, ref passedCount, ref failedCount);
           
            exp = "(2 + 3) * (5 + 2)";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 35, test, ref passedCount, ref failedCount);

            exp = "A6 + B3";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 8, test, ref passedCount, ref failedCount);

            exp = "(2 + X6) * 5 + 2";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 47, test, ref passedCount, ref failedCount);

            exp = "(X6 + 8) / B3";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 3, test, ref passedCount, ref failedCount);

            exp = "( ( 1 + 2 ) + ( 3 + 4 ) ) / ( 3 + 2 )";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 2, test, ref passedCount, ref failedCount);

            exp = "6 / (2 * 3)";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 1, test, ref passedCount, ref failedCount);

            exp = "(AC234 - X6) - (A6 + B3)";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 0, test, ref passedCount, ref failedCount);

            exp = "(X6 * A6) - (4 * B3 - AC234)";
            test = Evaluator.Evaluate(exp, variableLookup);
            checkTest(exp, 16, test, ref passedCount, ref failedCount);

            Console.WriteLine("Passed: " + passedCount);
            Console.WriteLine("Failed: " + failedCount);
            Console.ReadLine();

        }

        /// <summary>
        /// Quick method to check whether a test passed or not.
        /// </summary>
        /// <param name="exp">
        /// The expression we are evaluating.
        /// </param>
        /// <param name="expected">
        /// The value we should expect from the Evaluate Function.
        /// </param>
        /// <param name="actual">
        /// The value we got back from the Evaluate Function.
        /// </param>
        /// <param name="passedCount">
        /// The number of passed tests.
        /// </param>
        /// <param name="failedCount">
        /// The number of failed tests.
        /// </param>
        private static void checkTest(String exp, Double expected, Double actual, ref int passedCount, ref int failedCount)
        {
            if ( expected.Equals(actual) )
            {
                passedCount++;
            } else
            {
                failedCount++;
                Console.WriteLine("Fail: Expected " + exp + " to evaluate to " + expected + ". Instead, we got " + actual);
            }
        }

        /// <summary>
        /// A sample variable lookup function. This is used to evaluate
        /// any variables that would be used in the Evaluate method we are testing.
        /// </summary>
        /// <param name="s">
        /// The variable that you want evaluated.
        /// </param>
        /// <returns>
        /// The value of the variable you want evaluated.
        /// </returns>
        public static int variableLookup(String s)
        {
            Dictionary<String, int> values = new Dictionary<string, int>();
            values.Add("A6", 3);
            values.Add("B3", 5);
            values.Add("X6", 7);
            values.Add("AC234", 15);

            int rv = 0;
            bool containsKey = values.TryGetValue(s, out rv);
            if (containsKey)
            {
                return rv;
            }
            else
            {
                throw new KeyNotFoundException("Couldn't find " + s + " in the dictionary.");
            }
        }
    }
}
