using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;
using System;

using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class CharakterValues
    {
        #region Events
        public event EventHandler<IValue> ChangedAKTEvent;
        public event EventHandler<IValue> ChangedMODEvent;
        public event EventHandler<IValue> ChangedMAXEvent;
        #endregion
        #region Properties
        public List<IValue> UsedValues { get => aktValues.Keys.ToList(); }
        #endregion
        #region Variables
        private Dictionary<IValue, int> aktValues;
        private Dictionary<IValue, int> modValue;
        #endregion
        public CharakterValues(List<IValue> values)
        {
            aktValues   = new Dictionary<IValue, int>();
            modValue    = new Dictionary<IValue, int>();

            foreach(var item in values)
            {
                aktValues.Add(item, item.Value);
                modValue.Add(item, 0);
                item.ValueChanged += (sender, args) =>
                {
                    aktValues[item] = item.Value;
                    ChangedAKTEvent?.Invoke(this, item);
                };
            }
        }
        #region Getter
        public int GetAKTValue(IValue value, out Error error)
        {
            error   = null;
            var ret = -1;

            if (aktValues.TryGetValue(value, out int currentValue) == false)
            {
                error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
            }
            else
            {
                ret = aktValues[value];
            }

            return ret;
        }
        public int GetMODValue(IValue value, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var regularValue = modValue.TryGetValue(value, out int currentValue);

                if (regularValue == false)
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                } 
                else
                {
                    ret = modValue[value];
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
                throw ex;
                //Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterValues), nameof(GetAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        #endregion
        #region Setter
        /// <summary>
        /// Internal da es Von dem Charakter gesteuert wird
        /// Die Steuerung erfolgt durch den Charakter damit der Trait nicht als Klasse übergeben werden muss und keine 
        /// weitere Abhängigkeit entsteht
        /// </summary>
        /// <param name="attribut"></param>
        /// <param name="value"></param>
        internal void SetModValue(IValue item, int value)
        {
            if (!UsedValues.Contains(item))
            {
                throw new ArgumentException("Der Parameter wird nicht verwendet", nameof(item));
            }
            else
            {
                modValue.Remove(item);
                modValue.Add(item, value);

                ChangedMODEvent?.Invoke(this, item);
                ChangedMAXEvent?.Invoke(this, item);
            }
        }
        #endregion
    }
}
