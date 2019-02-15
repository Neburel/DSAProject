using DSAProject.Classes.Charakter.Values.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedAir : AbstractRaceValues
    {
        public override string Name => "Geschwindigkeit Luft";
        public SpeedAir(Interfaces.IRace race) : base(race)
        {
            Value = race.SpeedAir;
        }
    }
}
