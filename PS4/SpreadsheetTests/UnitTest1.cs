using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System.Linq;

namespace SpreadsheetTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAbstractSpreadsheetConstructor()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            Assert.IsTrue(s is Spreadsheet);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsInvalidName()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("x");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsInvalidName2()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsStringNullValue()
        {
            string str = null;
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A32", str);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsFormulaNullValue()
        {
            Formula f = null;
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A32", f);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNullName()
        {
            string str = null;
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(str, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidName()
        {
            string str = "test";
            string name = "25";
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(name, str);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidName2()
        {
            string str = "test";
            string name = "2x";
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(name, str);
        }

        [TestMethod]
        public void TestSetCellContentsDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 15.25);
        }

        [TestMethod]
        public void TestSetCellContentsString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "Test");
        }

        [TestMethod]
        public void TestSetCellContentsFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 5);
            s.SetCellContents("B1", 10);
            s.SetCellContents("C1", new Formula("A1 + B1"));
        }

        [TestMethod]
        public void TestSetCellContentsExistingCellString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "Test");
            Assert.AreEqual(s.GetCellContents("A1").ToString(), "Test");
            s.SetCellContents("A1", 45);
            double cellCont;
            bool cellContIsDouble = Double.TryParse(s.GetCellContents("A1").ToString(), out cellCont);
            Assert.AreEqual(cellCont, 45);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsEmpty()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 23.02);
            s.SetCellContents("A1", "");
            s.GetCellContents("A1");
        }

        [TestMethod]
        public void TestGetNamesOfAllNonemptyCells()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 3);
            s.SetCellContents("B1", new Formula("A1 * A1"));
            s.SetCellContents("C1", new Formula("B1 + A1"));
            s.SetCellContents("D1", new Formula("B1 - C1"));
            s.SetCellContents("E1", "test");
            List<string> allCells = new List<string>()
            {
                "A1", "B1", "C1", "D1", "E1"
            };
            List<string> cells = s.GetNamesOfAllNonemptyCells().ToList();
            List<string> exceptCells = allCells.Except(cells).ToList();
            Assert.IsTrue(exceptCells.Count.Equals(0));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestSetCellContentsCircular()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("B1 + 10"));
            s.SetCellContents("B1", new Formula("A1 + 5"));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestSetCellContentsCircularChange()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 20);
            s.SetCellContents("A1", new Formula("A1 + 10"));
        }

        [TestMethod]
        public void TestSetCellContentsReturnValue()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 20);
            s.SetCellContents("B1", 20);

            List<string> dependents = s.SetCellContents("C1", new Formula("A1 + B1")).ToList();
            List<string> correctDependents = new List<string>()
            {
                "A1", "B1"
            };
            Assert.AreEqual(correctDependents.Except(dependents).Count(), 0);
        }

        [TestMethod]
        public void TestSetCellContentsNestedReturnValue()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", 1);
            s.SetCellContents("C1", 2);
            s.SetCellContents("D1", 3);
            s.SetCellContents("E1", 4);
            s.SetCellContents("A2", new Formula("B1 + C1"));
            List<string> dependents = s.SetCellContents("B2", new Formula("A2 + D1 + E1")).ToList();
            List<string> correctDependents = new List<string>()
            {
                "B2", "A2", "B1", "C1", "D1", "E1"
            };
            Assert.AreEqual(correctDependents.Except(dependents).Count(), 0);

            dependents = s.SetCellContents("A3", new Formula("B2 + A2")).ToList();
            correctDependents.Add("A3");
            Assert.AreEqual(correctDependents.Except(dependents).Count(), 0);

        }

        [TestMethod]
        public void TestGetCellContents()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 25.998);
            object A1Contents = s.GetCellContents("A1");
            double A1ContentsDouble;
            Double.TryParse(A1Contents.ToString(), out A1ContentsDouble);
            Assert.AreEqual(A1ContentsDouble, 25.998);

            s.SetCellContents("A2", "This is a test.");
            object A2Contents = s.GetCellContents("A2");
            Assert.AreEqual(A2Contents.ToString(), "This is a test.");

            s.SetCellContents("A3", new Formula("(9 / 3)*2"));
            object A3Contents = s.GetCellContents("A3");
            Assert.AreEqual(A3Contents.ToString(), "(9/3)*2");
        }
    }
}
