using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AgCubio
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasicCubeProperties()
        {
            int mass = 50;
            object obj = new
            {
                loc_x = 800,
                loc_y = 400,
                argb_color = -2987746,
                uid = 5318,
                food = false,
                Name = "Myname",
                Mass = mass
            };
            string cubeStr = JsonConvert.SerializeObject(obj);
            Cube c = JsonConvert.DeserializeObject<Cube>(cubeStr);
            double MassPow = Math.Pow(mass, 0.65);

            float top = c.loc_y + (c.width / 2);
            float bottom = c.loc_y - (c.width / 2);
            float left = c.loc_x - (c.width / 2);
            float right = c.loc_x + (c.width / 2);

            Assert.AreEqual(c.width, (float)MassPow);
            Assert.AreEqual(c.top, top);
            Assert.AreEqual(c.bottom, bottom);
            Assert.AreEqual(c.left, left);
            Assert.AreEqual(c.right, right);
            Assert.AreEqual(c.position, new Tuple<double, double>(c.loc_x, c.loc_y));
        }

        [TestMethod]
        public void TestBasicAdd()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());
            object obj = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 5318,
                food = false,
                Name = "Myname",
                Mass = 1029.1106844616961
            };
            string cubeStr = JsonConvert.SerializeObject(obj);
            Cube c = JsonConvert.DeserializeObject<Cube>(cubeStr);
            w.addCube(c);

            Dictionary<string, Cube> cubes = w.GetCubes();
            Assert.AreEqual(cubes.Count, 1);

            w.removeCube(c);
            Dictionary<string, Cube> cubesAgain = w.GetCubes();
            Assert.AreEqual(cubesAgain.Count, 0);
        }

        [TestMethod]
        public void TestGetCubeById()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());
            object obj = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 5318,
                food = false,
                Name = "Myname",
                Mass = 1029.1106844616961
            };

            string cubeStr = JsonConvert.SerializeObject(obj);
            Cube c = JsonConvert.DeserializeObject<Cube>(cubeStr);
            w.addCube(c);

            Cube cubeFromWorld = w.getCubeById(c.uid);
            Assert.AreEqual(c, cubeFromWorld);
        }

        [TestMethod]
        public void TestUpdateCubes()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());
            string cubeStr = "";
            for (int i = 0; i < 5; i++)
            {
                object obj = new
                {
                    loc_x = 813.0,
                    loc_y = 878.0,
                    argb_color = -2987746,
                    uid = i,
                    food = false,
                    Name = "Myname" + i,
                    Mass = i * 5
                };

                cubeStr += JsonConvert.SerializeObject(obj) + '\n';
            }
            cubeStr += "{ \"loc_x\": 813.0 ";

            w.updateCubes(cubeStr);

            Dictionary<string, Cube> cubes = w.GetCubes();
            Assert.AreEqual(cubes.Count, 4);
        }

        [TestMethod]
        public void TestGetFoodCount()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());

            String cubeStr = "";
            for (int i = 0; i < 5; i++)
            {
                object obj = new
                {
                    loc_x = i * 3,
                    loc_y = i * 3,
                    argb_color = -2987746,
                    uid = i + 1,
                    food = true,
                    Name = "",
                    Mass = 30
                };

                cubeStr += JsonConvert.SerializeObject(obj) + '\n';
            }
            object playerCube = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 500,
                food = false,
                Name = "Player 1",
                Mass = 50
            };
            cubeStr += JsonConvert.SerializeObject(playerCube) + '\n';
            w.updateCubes(cubeStr);
            Assert.AreEqual(w.getFoodCount(), 5);
        }

        [TestMethod]
        public void testGetCubeByIdNull()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());
            Assert.AreEqual(null, w.getCubeById("200"));
        }

        [TestMethod]
        public void testDestroyingExistingCube()
        {
            World w = new World(1000, 1000, new Dictionary<string, Cube>());

            object playerCube = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 500,
                food = false,
                Name = "Player 1",
                Mass = 50
            };
            string cubeStr = JsonConvert.SerializeObject(playerCube) + '\n';
            w.updateCubes(cubeStr);


            object playerCubeUpdated = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 500,
                food = false,
                Name = "Player 1",
                Mass = 60
            };
            string cubeStrUpdated = JsonConvert.SerializeObject(playerCubeUpdated) + '\n';
            w.updateCubes(cubeStrUpdated);
            Dictionary<String, Cube> cubes = w.GetCubes();
            Cube c = w.getCubeById("500");
            Assert.AreEqual(cubes.Count, 1);
            Assert.AreEqual(c.mass, 60);

            object playerCubeUpdatedAgain = new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 500,
                food = false,
                Name = "Player 1",
                Mass = 0
            };
            string cubeStrUpdatedAgain = JsonConvert.SerializeObject(playerCubeUpdatedAgain) + '\n';
            w.updateCubes(cubeStrUpdatedAgain);
            Dictionary<String, Cube> cubesAgain = w.GetCubes();
            Cube cu = w.getCubeById("500");
            Assert.AreEqual(cu, null);
            Assert.AreEqual(cubesAgain.Count, 0);



        }
    }
}
