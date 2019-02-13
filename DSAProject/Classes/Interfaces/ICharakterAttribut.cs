using DSAProject.Classes.Charakter;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Interfaces
{
    public interface ICharakterAttribut 
    {
        event EventHandler<CharakterAttribut> ChangedAttributAKTEvent;

        List<CharakterAttribut> UsedAttributs { get; }
        void SetAttributAKTValue(CharakterAttribut attribut, int value, out Error error);
        int GetAttributAKTValue(CharakterAttribut attribut, out Error error);
    }
}
