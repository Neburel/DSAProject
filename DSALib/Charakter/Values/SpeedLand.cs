using DSAProject.Classes.Interfaces;
using System;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedLand : IValue
    {
        public int Value { get; }
        public string Name => "Geschwindigkeit";

        public string InfoText => "";

        public event EventHandler ValueChanged;
    }
}
