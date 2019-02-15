﻿using DSAProject.Classes.Charakter;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Interfaces
{
    public interface ICharakter
    {
        ICharakterAttribut Attribute { get; }
        IRace Race { get; }
        void Save(string fileName, out Error error);
        void Load(string fileName, out Error error);
    }
}
