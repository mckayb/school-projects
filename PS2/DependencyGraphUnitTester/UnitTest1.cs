using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace DependencyGraphUnitTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(0, dg.Size);
        }

        [TestMethod]
        public void TestAddRemoveDependency()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");

            Assert.IsTrue(dg.HasDependents("a"));
            Assert.IsTrue(dg.HasDependees("b"));
            Assert.AreEqual(1, dg.Size);

            dg.RemoveDependency("a", "b");
            Assert.AreEqual(0, dg.Size);
            Assert.IsFalse(dg.HasDependents("a"));
            Assert.IsFalse(dg.HasDependees("b"));
        }

        [TestMethod]
        public void TestDependenceeSizeInvoke()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(dg["a"], 0);

            dg.AddDependency("c", "a");
            Assert.AreEqual(dg["a"], 1);

            dg.AddDependency("d", "a");
            Assert.AreEqual(dg["a"], 2);

            dg.RemoveDependency("c", "a");
            Assert.AreEqual(dg["a"], 1);

            dg.RemoveDependency("d", "a");
            Assert.AreEqual(dg["a"], 0);
        }

        [TestMethod]
        public void TestGetDependencies()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");

            IEnumerable<string> dependencies = dg.GetDependents("a");
            List<string> listDeps = dependencies.ToList();
            Assert.IsTrue(listDeps.IndexOf("b") >= 0);
            Assert.IsTrue(listDeps.IndexOf("c") >= 0);
        }

        [TestMethod]
        public void TestGetDependees()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("c", "b");

            IEnumerable<string> dependees = dg.GetDependees("b");
            List<string> listDeps = dependees.ToList();
            Assert.IsTrue(listDeps.IndexOf("a") >= 0);
            Assert.IsTrue(listDeps.IndexOf("c") >= 0);
        }

        [TestMethod]
        public void TestReplaceDependents()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("b", "e");

            List<string> newDependents = new List<string>();
            newDependents.Add("e");
            newDependents.Add("f");
            newDependents.Add("g");
            dg.ReplaceDependents("a", newDependents);

            IEnumerable<string> dependents = dg.GetDependents("a");
            List<string> listDeps = dependents.ToList();
            Assert.IsTrue(listDeps.IndexOf("e") >= 0);
            Assert.IsTrue(listDeps.IndexOf("f") >= 0);
            Assert.IsTrue(listDeps.IndexOf("g") >= 0);
        }

        [TestMethod]
        public void TestReplaceDependees()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("b", "a");
            dg.AddDependency("c", "a");
            dg.AddDependency("d", "a");

            List<string> newDependees = new List<string>();
            newDependees.Add("b");
            newDependees.Add("c");
            newDependees.Add("f");
            newDependees.Add("e");
            dg.ReplaceDependees("a", newDependees);
            IEnumerable<string> dependees = dg.GetDependees("a");
            List<string> listDeps = dependees.ToList();

            Assert.IsTrue(listDeps.IndexOf("b") >= 0);
            Assert.IsTrue(listDeps.IndexOf("f") >= 0);
            Assert.IsTrue(listDeps.IndexOf("e") >= 0);
            Assert.IsTrue(listDeps.IndexOf("c") >= 0);
        }
    }
}
