using DSALib;

using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using System;

using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class CharakterValues : ICharakterValues
    {
        #region Events
        public event EventHandler<IValue> ChangedAKTEvent;
        public event EventHandler<IValue> ChangedMODEvent;
        public event EventHandler<IValue> ChangedMAXEvent;
        #endregion
        #region Properties
        public List<IValue> UsedValues { get => AktValues.Keys.ToList(); }
        #endregion
        #region Variables
        private Dictionary<IValue, int> AktValues;
        private Dictionary<IValue, int> ModValue;
        #endregion
        public CharakterValues(List<IValue> values)
        {
            AktValues   = new Dictionary<IValue, int>();
            ModValue    = new Dictionary<IValue, int>();

            foreach(var item in values)
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
        public int GetAKTValue(IValue value, out Error error)
        {
            error   = null;
            var ret = -1;

            try
            {
                var regularValue = AktValues.TryGetValue(value, out int currentValue);

                if (regularValue == false)
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                } else
                {
                    ret = AktValues[value];
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        public int GetMODValue(IValue value, out Error error)
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
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        public int GetMAXValue(IValue value, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var akt = GetAKTValue(value, out error);
                if(error == null)
                {
                    var mod = GetMODValue(value, out error);
                    if(error == null)
                    {
                        ret = akt + mod;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        #endregion
    }
}
