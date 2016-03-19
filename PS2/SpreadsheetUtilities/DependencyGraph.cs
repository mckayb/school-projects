// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// s1 depends on t1 --> t1 must be evaluated before s1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// (Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.)
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private Dictionary<string, List<string>> pairs;
        private Dictionary<string, List<string>> dependents;
        private Dictionary<string, List<string>> dependees;


        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            pairs = new Dictionary<string, List<string>>();
            dependents = new Dictionary<string, List<string>>();
            dependees = new Dictionary<string, List<string>>();
        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get
            {
                return pairs.Count;
            }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (HasDependees(s))
                {
                    return dependees[s].Count;
                }
                return 0;
            }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            return dependents.ContainsKey(s);
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            return dependees.ContainsKey(s);
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if ( dependents.ContainsKey(s) )
            {
                return dependents[s];
            }
            return new List<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if ( dependees.ContainsKey(s) )
            {
                return dependees[s];
            }
            return new List<string>();
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   s depends on t
        ///
        /// </summary>
        /// <param name="s"> s cannot be evaluated until t is</param>
        /// <param name="t"> t must be evaluated first.  S depends on T</param>
        public void AddDependency(string s, string t)
        {
            // Pairs
            if ( pairs.ContainsKey(s) )
            {
                if ( pairs[s].IndexOf(t) < 0 )
                {
                    pairs[s].Add(t);
                }
            } else
            {
                List<string> pairsWithFirstCoordS = new List<string>();
                pairsWithFirstCoordS.Add(t);
                pairs.Add(s, pairsWithFirstCoordS);
            }

            // Dependents
            if ( HasDependents(s) )
            {
                if (dependents[s].IndexOf(t) < 0)
                {
                    dependents[s].Add(t);
                }
            } else
            {
                List<string> newDependents = new List<string>();
                newDependents.Add(t);
                dependents.Add(s, newDependents);
            }

            // Dependees
            if ( HasDependees(t) )
            {
                if ( dependees[t].IndexOf(s) < 0 )
                {
                    dependees[t].Add(s);
                }

            } else
            {
                List<string> newDependees = new List<string>();
                newDependees.Add(s);
                dependees.Add(t, newDependees);
            }
        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            // Pairs
            if ( pairs.ContainsKey(s) && pairs[s].IndexOf( t ) >= 0 )
            {
                pairs[s].Remove(t);
                if ( pairs[s].Count.Equals(0) )
                {
                    pairs.Remove(s);
                }
            }

            // Dependents
            if ( HasDependents(s) && dependents[s].IndexOf(t ) >= 0 )
            {
                dependents[s].Remove(t);
                if ( dependents[s].Count.Equals(0) )
                {
                    dependents.Remove(s);
                }
            }

            // Dependees
            if ( HasDependees(t) && dependees[t].IndexOf( s ) >= 0 )
            {
                dependees[t].Remove(s);
                if ( dependees[t].Count.Equals(0) )
                {
                    dependees.Remove(t);
                }
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            // Pairs
            if ( pairs.ContainsKey( s ) )
            {
                pairs.Remove(s);
            }

            List<string> dependentsOfS = new List<string>();

            // Dependents
            if ( dependents.ContainsKey( s ) )
            {
                dependentsOfS = dependents[s];
                dependents.Remove(s);
            }

            // Dependees
            if ( dependentsOfS.Count > 0 )
            {
                foreach( string dep in dependentsOfS )
                {
                    if ( dependees.ContainsKey( dep ) && dependees[dep].IndexOf( s ) >= 0 )
                    {
                        dependees[dep].Remove(s);
                    }
                }
            }

            // Finally, add the new ones all in
            List<string> listNewDeps = newDependents.ToList();
            foreach( string newDep in listNewDeps )
            {
                AddDependency(s, newDep);
            }

        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            List<string> dependentsOfS = new List<string>();
            if ( dependents.ContainsKey( s ) )
            {
                dependentsOfS = dependents[s];
            }

            // Pairs and Dependents
            foreach( string dep in dependentsOfS )
            {
                if ( pairs.ContainsKey( dep ) && pairs[dep].IndexOf( s ) >= 0 )
                {
                    pairs[dep].Remove(s);
                }

                if ( dependents.ContainsKey( dep ) && dependents[dep].IndexOf(s) >= 0 )
                {
                    dependents[dep].Remove(s);
                }
            }

            // Dependees
            if ( dependees.ContainsKey( s ) )
            {
                dependees.Remove(s);
            }

            // Finally, add in the new ones.
            List<string> listNewDep = newDependees.ToList();
            foreach( string newDep in listNewDep )
            {
                AddDependency(newDep, s);
            }
        }
    }
}
