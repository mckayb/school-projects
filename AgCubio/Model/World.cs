using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AgCubio
{
    /// <summary>
    /// Represents our World in AgCubio
    /// </summary>
    public class World
    {
        /// <summary>
        /// The width of the world
        /// </summary>
        protected readonly Double width = 1000;

        /// <summary>
        /// The height of the world.
        /// </summary>
        protected readonly Double height = 1000;

        /// <summary>
        /// All of the cubes stored in the world. Each cube is indexed off of it's uid.
        /// </summary>
        protected Dictionary<String, Cube> cubes;

        /// <summary>
        /// Constructor for the cube based off of the width, height, and cubes.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cubes"></param>
        public World( double width, double height, Dictionary<String, Cube> cubes)
        {
            this.width = width;
            this.height = height;
            this.cubes = cubes;
        }

        /// <summary>
        /// Removes a cube from our dictionary, if it exists.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool removeCube(Cube c)
        {
            if (c != null && cubes.ContainsKey(c.uid))
                return cubes.Remove(c.uid);
            return false;
        }

        /// <summary>
        /// Adds a new cube to our dictionary.
        /// </summary>
        /// <param name="c"></param>
        public void addCube(Cube c)
        {
            if (c != null)
            {
                cubes.Add(c.uid, c);
            }
        }

        /// <summary>
        /// Returns the count of all the food cubes.
        /// </summary>
        /// <returns></returns>
        public int getFoodCount()
        {
            int count = 0;
            foreach ( KeyValuePair<String, Cube> kv in cubes )
            {
                Cube c = kv.Value;
                if ( c.food == true )
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns the cube given by the unique uid, if it exists.
        /// Returns null otherwise.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Cube getCubeById( string uid )
        {
            if ( cubes.ContainsKey( uid ) )
            {
                return cubes[uid];
            }
            return null;
        }

        /// <summary>
        /// Updates all of the cubes to be consistent with all of the objects in our json string.
        /// </summary>
        /// <param name="jsonString"></param>
        public string updateCubes(string jsonString) {
            string unfinishedLine = "";
            if (jsonString != null)
            {
                string[] lines = Regex.Split(jsonString, "[\r\n]+");
                foreach (string l in lines)
                {
                    if (l.EndsWith("}"))
                    {
                        Cube cube = JsonConvert.DeserializeObject<Cube>(l);
                        if (cubes.ContainsKey(cube.uid))
                        {
                            if (cube.mass != 0)
                            {
                                cubes[cube.uid] = cube;
                            }
                            else
                            {
                                removeCube(cube);
                            }
                        }
                        else
                        {
                            if (cube.mass != 0)
                            {
                                addCube(cube);
                            }
                            else
                            {
                                removeCube(cube);
                            }
                        }
                    } else
                    {
                        unfinishedLine = l;
                    }
                }
            }
            return unfinishedLine;

        }


        /// <summary>
        /// Returns all of the cubes in the world.
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Cube> GetCubes()
        {
            return cubes;
        }
    }
}
