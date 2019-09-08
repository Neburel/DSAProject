using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Interfaces
{
    public interface IValue
    {
        event EventHandler ValueChanged;
        int Value { get; }
        string Name { get; }
    }
}
