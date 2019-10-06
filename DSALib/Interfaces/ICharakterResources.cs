using DSALib.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Interfaces
{
    public interface ICharakterResources
    {
        #region Events
        event EventHandler<IResource> ChangedAKTEvent;
        event EventHandler<IResource> ChangedMODEvent;
        event EventHandler<IResource> ChangedMAXEvent;
        #endregion
        #region Properties
        List<IResource> UsedValues { get; }
        #endregion
        #region Getter
        int GetAKTValue(IResource attribut, out Error error);
        int GetMODValue(IResource attribut, out Error error);
        int GetMAXValue(IResource attribut, out Error error);
        #endregion
    }
}
