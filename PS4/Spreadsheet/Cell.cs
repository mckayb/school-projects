using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;

namespace SS
{
    /// <summary>
    /// Class to represent a cell in a spreadsheet.
    /// A cell is identified by a name, which consists of a letter or underscore, followed
    /// by zero or more letters or underscores.
    /// Each cell contains two items, content and value. 
    /// The content of the cell is the actual text that the user has typed into the cell.
    /// The value of the cell is what the actual text evaluates to.
    /// 
    /// The user is able to type in either a string, a double, or a Formula.
    /// If the user has typed in a string or a double, the content and
    /// the value of the cell are identically that string or that double.
    /// If the user has typed in a Formula, then the content is the formula string itself,
    /// and the value is what the Formula ends up evaluating as.
    /// 
    /// Example:
    /// If the user has typed 8 into the cell, the content and value are both 8
    /// If the user has typed "My dog" into the cell, the content and value are both "My dog"
    /// If the user has typed "8 + 5" into the cell, the content is "8 + 5", but the value is 13
    /// </summary>
    class Cell
    {

        /// <summary>
        /// Constructor for the cell.
        /// All validation has been done when setting the contents of the cell. So we don't
        /// need to worry about that. Just set the name, content and value with whatever
        /// is given.
        /// </summary>
        /// <param name="name">The identifier for the cell, like "A3"</param>
        /// <param name="content">The content of the cell, like 20, "Test" or Formula("A1 + 2")</param>
        /// <param name="value">The value of the cell, like 20, "Test", or an evaluated formula</param>
        public Cell(string name, object content, object value)
        {
            this.name = name;
            this.content = content;
            this.value = value;
        }

        /// <summary>
        /// Getter and setter for the name of the cell, the identifier
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Getter and setter for the content of the cell, the stuff the user has typed in to the cell.
        /// </summary>
        public object content { get; set; }

        /// <summary>
        /// Getter and setter for the value of the cell, what the content evaluates to.
        /// </summary>
        public object value { get; set; }

        /// <summary>
        /// Method to test if the cell has a valid name or not.
        /// Tests if it starts with a letter or underscore, followed by zero or more
        /// letters, numbers, or underscores.
        /// </summary>
        /// <param name="name">The name to test</param>
        /// <returns>Whether the name is valid or not</returns>
        public static bool IsValidName(string name)
        {
            String varPattern = @"^[a-zA-Z_](?: [a-zA-Z_]|\d)*$";
            return Regex.IsMatch(name, varPattern);
        }
    }
}
