using DSAProject.Classes.Charakter.Values.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedLand : AbstractRaceValues
    {
        public override string Name => "Geschwindigkeit";
        public SpeedLand(Interfaces.IRace race) : base(race)
        {
            Value = race.SpeedLand;
        }
    }
}
