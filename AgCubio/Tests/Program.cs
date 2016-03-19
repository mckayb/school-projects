using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgCubio;
using Newtonsoft.Json;



namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string obj = JsonConvert.SerializeObject(new
            {
                loc_x = 813.0,
                loc_y = 878.0,
                argb_color = -2987746,
                uid = 5318,
                food = false,
                Name = "myName",
                Mass = 1029.1106844616961
            });

            Cube rebuilt = JsonConvert.DeserializeObject<Cube>(obj);
        }
    }
}
