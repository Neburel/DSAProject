using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedAir : IValue
    {
        public int Value { get; }
        public string Name => "Geschwindigkeit Luft";

        public event EventHandler ValueChanged;
    }
}
