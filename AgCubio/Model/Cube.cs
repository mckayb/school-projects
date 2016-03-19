using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCubio
{
    /// <summary>
    /// Represents a cube that is placed on the screen when playing AgCubio.
    /// </summary>
    public class Cube
    {
        /// <summary>
        /// Identifer for the cube.
        /// </summary>
        public String uid;

        /// <summary>
        /// Color of the cube.
        /// </summary>
        public int argb_color;

        /// <summary>
        /// The x-axis location in the world
        /// </summary>
        public float loc_x;

        /// <summary>
        /// The y-axis location in the world
        /// </summary>
        public float loc_y;

        /// <summary>
        /// Whether this is a food cube or not.
        /// </summary>
        public Boolean food;

        /// <summary>
        /// The name of the cube. This is empty if it's a food cube.
        /// </summary>
        public String name;

        /// <summary>
        /// The mass of the cube.
        /// </summary>
        public Double mass;

        /// <summary>
        /// The id of the team this cube belongs to.
        /// It will be the uid of the player cube.
        /// </summary>
        public String team_id;

        /// <summary>
        /// Keep track of the top of the cube.
        /// </summary>
        public float top
        {
            get { return loc_y + (width / 2); }
        }

        /// <summary>
        /// Keep track of the bottom of the cube.
        /// </summary>
        public float bottom
        {
            get { return loc_y - (width / 2); }
        }

        /// <summary>
        /// Keep track of the left side of the cube.
        /// </summary>
        public float left
        {
            get { return loc_x - (width / 2); }
        }

        /// <summary>
        /// Keep track of the right side of the cube.
        /// </summary>
        public float right
        {
            get { return loc_x + (width / 2); }
        }

        /// <summary>
        /// Keep track of the width of the cube.
        /// Note: the assignment specifications said to return the sqrt of the mass.
        /// But that didn't work. So we're using the power to the .65.
        /// </summary>
        public float width
        {
            //get { return (float)Math.Sqrt(mass); }
            get { return (float)Math.Pow(mass, 0.65); }
        }

        public Tuple<double, double> position
        {
            get { return new Tuple<double, double>(loc_x, loc_y); }
        }
    }
}
