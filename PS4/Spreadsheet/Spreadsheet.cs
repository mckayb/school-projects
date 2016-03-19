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
    /// A Spreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string is a valid cell name if and only if:
    ///   (1) its first character is an underscore or a letter
    ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
    /// Note that this is the same as the definition of valid variable from the PS3 Formula class.
    /// 
    /// For example, "x", "_", "x2", "y_15", and "___" are all valid cell  names, but
    /// "25", "2x", and "&" are not.  Cell names are case sensitive, so "x" and "X" are
    /// different cell names. 
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  (This
    /// means that a spreadsheet contains an infinite number of cells.)  In addition to 
    /// a name, each cell has a contents and a value.  The distinction is important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Variable to keep track of all cells that have a value in them.
        /// Simply relates the string name of the cell to the Cell object itself.
        /// </summary>
        private Dictionary<string, Cell> cells;

        /// <summary>
        /// Variable to keep track of all relations between cells.
        /// </summary>
        private DependencyGraph cellRelations;

        /// <summary>
        /// Constructor for our Spreadsheet. Initializes a set of empty cells
        /// and an empty set of relations for the cells.
        /// </summary>
        public Spreadsheet()
        {
            cellRelations = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (Cell.IsValidName(name) && cells.ContainsKey(name))
                return cells[name].content;
            else
                throw new InvalidNameException();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            HashSet<string> nonemptyCells = new HashSet<string>();
            foreach (KeyValuePair<string, Cell> cell in cells)
            {
                nonemptyCells.Add(cell.Key);
            }
            return nonemptyCells;
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            return SetContents(name, formula);
        }
              
        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, string text)
        {
            return SetContents(name, text);
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, double number)
        {
            return SetContents(name, number);
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //if ( name == null || ! Cell.IsValidName( name ) )
            //{
            //    throw new ArgumentNullException("Make sure the cell name is valid.");
            //}
            return cellRelations.GetDependents(name);
        }

        /// <summary>
        /// If the parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        private ISet<string> SetContents( string name, object content )
        {
            // Make sure the content is not null
            if (content == null)
            {
                throw new ArgumentNullException("Make sure the formula is not null.");
            }

            if ( name == null || ! Cell.IsValidName(name) )
            {
                throw new InvalidNameException();
            }

            // Every time we set the contents, remove all of the dependents and dependees for this name.
            cellRelations.ReplaceDependents(name, new List<string>());

            // Set the value to the content. Value will change if content is a formula.
            object value = content;


            // Now, handle formula-specific actions
            if ( content is Formula )
            {
                // First make sure that adding this new content would not create a circular dependency.
                Formula contentAsFormula = (Formula)content;
                checkForCircularDependency(name, contentAsFormula);

                // Add in all of the dependencies for this cell
                List<string> variablesInFormula = contentAsFormula.GetVariables().ToList();
                foreach ( string v in variablesInFormula )
                {
                    cellRelations.AddDependency(name, v);
                }

                // Get the value and content prepped for cell creation
                value = contentAsFormula.Evaluate(lookup);
                content = contentAsFormula.ToString();
            }


            // If the cell already exists, just change the content and value.
            if ( cells.ContainsKey( name ) )
            {
                // If they are setting the content to "", remove the cell.
                if (value.ToString().Equals(""))
                {
                    cells.Remove(name);
                }
                else
                {
                    cells[name].content = content;
                    cells[name].value = value;
                }
            }
            // Otherwise, create a new cell object with the proper name, content, and value
            // This will also check that the name of the cell is valid or not.
            else
            {
                Cell cellForName = new Cell(name, content, value);
                cells.Add(name, cellForName);
            }

            // Get all of the dependees for the named cell and return them.
            HashSet<string> dependsOnNamedCell = new HashSet<string>() { name };
            List<string> namedCellDependees = GetCellsToRecalculate(name).ToList();
            foreach( string dep in namedCellDependees )
            {
                dependsOnNamedCell.Add(dep);
            }
            return dependsOnNamedCell;
        }

        /// <summary>
        /// Gets any cells that depend ( directly or indirectly ) on the named cell.
        /// Then grabs all of the variables in our formula and sees if any of those appear
        /// in the cells that depend on it. If they do, we have a circular dependency.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        private void checkForCircularDependency( string name, Formula content )
        {
            List<string> cellsToRecalc = GetCellsToRecalculate(name).ToList();
            List<string> variablesInFormula = content.GetVariables().ToList();

            foreach (string v in variablesInFormula)
            {
                if (cellsToRecalc.Contains(v))
                {
                    throw new CircularException();
                }
            }
        }

        /// <summary>
        /// Finds any cells by the name s, which hold a double.
        /// If we find one, return the double that the cell is holding.
        /// Otherwise, throw a KeyNotFound exception.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double lookup(string s)
        {
            foreach( KeyValuePair<string, Cell> cell in cells )
            {
                if (cell.Value.name.Equals(s))
                {
                    object val = cell.Value.value;
                    double cellVal;
                    bool isDouble = Double.TryParse(val.ToString(), out cellVal);
                    if ( isDouble )
                    {
                        return cellVal;
                    }
                }
            }
            return 0;
        } 
    }
}
