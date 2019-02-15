using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values
{
    public class ControllValue : IValue
    {
        public event EventHandler ValueChanged;
        public int Value => 0;
        public string Name => "Beherschungswert";
    }
}
