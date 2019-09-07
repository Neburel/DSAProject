using DSALib;
using DSALib.Utils;
using DSAProject.util.ErrrorManagment;

using System;
using System.Collections.Generic;

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
        int GetSumValueAttributeAKT { get; }
        int GetSumValueAttributMod { get; }
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
