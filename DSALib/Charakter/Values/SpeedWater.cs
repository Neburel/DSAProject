using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedWater : IValue
    {
        public int Value { get; }
        public string Name => "Geschwindigkeit Wasser";
        public string InfoText => "";
        public event EventHandler ValueChanged;
    }
}
