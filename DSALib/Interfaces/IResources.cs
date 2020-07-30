
using System;
using System.Collections.Generic;

namespace DSALib.Interfaces
{
    public interface IResource
    {
        event EventHandler ValueChanged;
        int Value { get; }
        string Name { get; }
        string ShortName { get; }
        string InfoText { get; }
    }
}
