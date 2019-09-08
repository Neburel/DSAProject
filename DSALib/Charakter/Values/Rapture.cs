using DSAProject.Classes.Interfaces;
using System;

namespace DSAProject.Classes.Charakter.Values
{
    public class Rapture : IValue
    {
        public int Value => 0;
        public string Name => "Entrückung";
        public event EventHandler ValueChanged;
    }
}
