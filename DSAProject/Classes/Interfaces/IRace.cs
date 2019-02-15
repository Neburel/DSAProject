using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Interfaces
{
    public interface IRace
    {
        int SpeedAir {get;}
        int SpeedLand { get; }
        int SpeedWater { get; }
    }
}
