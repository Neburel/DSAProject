using DSAProject.Classes.Interfaces;
using System;

namespace DSAProject.Classes.Charakter.Values
{
    public class ControllValue : IValue
    {
        public event EventHandler ValueChanged;
        public int Value => 0;
        public string Name => "Beherschungswert";
        public string InfoText => "";
    }
}
