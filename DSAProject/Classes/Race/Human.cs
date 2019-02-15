using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Race
{
    public class Human : IRace
    {
        public int SpeedAir => 7;
        public int SpeedLand => 7;
        public int SpeedWater => 7;
    }
}
