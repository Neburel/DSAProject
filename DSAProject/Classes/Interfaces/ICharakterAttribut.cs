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
        #region Events
        event EventHandler<CharakterAttribut> ChangedAttributAKTEvent;
        event EventHandler<CharakterAttribut> ChangedAttributMODEvent;
        event EventHandler<CharakterAttribut> ChangedAttributMAXEvent;
        #endregion
        #region Properties
        List<CharakterAttribut> UsedAttributs { get; }
        #endregion
        #region Setter
        void SetAttributAKTValue(CharakterAttribut attribut, int value, out Error error);
        #endregion
        #region Getter
        int GetAttributAKTValue(CharakterAttribut attribut, out Error error);
        int GetAttributMODValue(CharakterAttribut attribut, out Error error);
        int GetAttributMAXValue(CharakterAttribut attribut, out Error error);
        #endregion
    }
}
