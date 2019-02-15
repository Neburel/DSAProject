using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class ArtefactControll : IValue
    {
        public int Value => 0;
        public string Name => "Artefaktkontrolle";
        public event EventHandler ValueChanged;
    }
}
