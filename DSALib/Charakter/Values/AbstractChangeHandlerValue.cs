using DSAProject.Classes.Interfaces;
using System;

namespace DSALib.Charakter.Values
{
    public abstract class AbstractChangeHandlerValue : IValue
    {
        public abstract event EventHandler ValueChanged;
        public abstract int Value { get; }
        public abstract string Name { get; }
        public abstract string ShortName { get; }
        public abstract string InfoText { get; }
    }
}
