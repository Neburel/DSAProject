﻿using System;

namespace DSAProject.Classes.Interfaces
{
    public interface IValue
    {   
        int Value { get; }
        string Name { get; }
        string ShortName { get; }
        string InfoText { get; }
    }
}
