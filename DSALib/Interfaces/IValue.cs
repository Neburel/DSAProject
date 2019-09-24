using System;

namespace DSAProject.Classes.Interfaces
{
    public interface IValue
    {
        event EventHandler ValueChanged;
        int Value { get; }
        string Name { get; }
    }
}
