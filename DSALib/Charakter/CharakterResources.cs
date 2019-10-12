using DSALib.Interfaces;
using DSALib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib.Charakter
{
    public class CharakterResources
    {
        #region Events
        public event EventHandler<IResource> ChangedAKTEvent;
        public event EventHandler<IResource> ChangedMODEvent;
        public event EventHandler<IResource> ChangedMAXEvent;
        #endregion
        #region Properties
        public List<IResource> UsedValues { get => AktValues.Keys.ToList(); }
        #endregion
        #region Variables
        private Dictionary<IResource, int> AktValues;
        private Dictionary<IResource, int> ModValue;
        #endregion

        public CharakterResources(List<IResource> values)
        {
            AktValues   = new Dictionary<IResource, int>();
            ModValue    = new Dictionary<IResource, int>();

            foreach (var item in values)
            {
                AktValues.Add(item, item.Value);
                ModValue.Add(item, 0);
                item.ValueChanged += (sender, args) =>
                {
                    AktValues[item] = item.Value;
                    ChangedAKTEvent?.Invoke(this, item);
                };
            }
        }

        #region Getter
        public int GetAKTValue(IResource value, out Error error)
        {
            error = null;
            var ret = -1;

            if (AktValues.TryGetValue(value, out int currentValue) == false)
            {
                error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
            }
            else
            {
                ret = AktValues[value];
            }

            return ret;
        }
        public int GetMODValue(IResource value, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var regularValue = ModValue.TryGetValue(value, out int currentValue);

                if (regularValue == false)
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                }
                else
                {
                    ret = ModValue[value];
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        public int GetMAXValue(IResource value, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var akt = GetAKTValue(value, out error);
                if (error == null)
                {
                    var mod = GetMODValue(value, out error);
                    if (error == null)
                    {
                        ret = akt + mod;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        #endregion
    }
}
