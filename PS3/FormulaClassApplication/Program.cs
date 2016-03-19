using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FormulaClassApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b;
            Double.TryParse("5e1", NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out a);
            Double.TryParse("5e1", out b);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.ReadLine();
        }

        private double lookup(string var)
        {
            return 12;
        }

        private bool isValid(string formula)
        {
            var pattern = @"[a-zA-Z][0-9]";
            return Regex.IsMatch(formula, pattern);
        }

        private string normalize(string formula)
        {
            return formula.ToUpper();
        }
    }
}
