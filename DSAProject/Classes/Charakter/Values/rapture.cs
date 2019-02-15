using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class Rapture : IValue
    {
        public int Value => 0;
        public string Name => "Entrückung";
        public event EventHandler ValueChanged;
    }
}
