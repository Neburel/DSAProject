using DSALib.Utils;
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
        string Name { get; set; }
        ICharakterValues Values { get; }
        ICharakterAttribut Attribute { get; }
        CharakterTalente CharakterTalente { get; }
        CharakterDescription CharakterDescriptions { get; }
        

        void Save(out Error error);
        void Load(string fileName, out Error error);
    }
}
