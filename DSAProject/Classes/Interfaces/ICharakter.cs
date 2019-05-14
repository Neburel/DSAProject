using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
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
        ICharakterValues Values { get; }
        ICharakterAttribut Attribute { get; }
        CharakterDescription CharakterDescriptions { get; }

        void Save(string fileName, out Error error);
        void Load(string fileName, out Error error);
    }
}
