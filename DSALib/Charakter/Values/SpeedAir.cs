using DSAProject.Classes.Interfaces;
using System;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedAir : IValue
    {
        public int Value { get; }
        public string Name => "Geschwindigkeit Luft";
        public string InfoText => "";

        public event EventHandler ValueChanged;
    }
}
