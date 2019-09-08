using DSALib.Utils;
using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Interfaces
{
    public interface ICharakterValues
    {
        #region Events
        event EventHandler<IValue> ChangedAKTEvent;
        event EventHandler<IValue> ChangedMODEvent;
        event EventHandler<IValue> ChangedMAXEvent;
        #endregion
        #region Properties
        List<IValue> UsedValues { get; }
        #endregion
        #region Getter
        int GetAKTValue(IValue attribut, out Error error);
        int GetMODValue(IValue attribut, out Error error);
        int GetMAXValue(IValue attribut, out Error error);
        #endregion
    }
}
