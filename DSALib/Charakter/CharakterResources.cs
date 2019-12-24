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
        public event EventHandler<IResource> ChangedMOD;
        public event EventHandler<IResource> ChangedMAX;
        #endregion
        #region Properties
        public List<IResource> UsedValues { get => aktValues.Keys.ToList(); }
        #endregion
        #region Variables
        private Dictionary<IResource, int> aktValues;
        private Dictionary<IResource, int> modValue;
        #endregion

        public CharakterResources(List<IResource> values)
        {
            aktValues   = new Dictionary<IResource, int>();
            modValue    = new Dictionary<IResource, int>();

            foreach (var item in values)
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
        public int GetAKTValue(IResource value, out Error error)
        {
            error = null;
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
        public int GetMODValue(IResource value, out Error error)
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
        internal void SetModValue(IResource item, int value)
        {
            if (!UsedValues.Contains(item))
            {
                throw new ArgumentException("Der Parameter wird nicht verwendet", nameof(item));
            }
            else
            {
                modValue.Remove(item);
                modValue.Add(item, value);

                ChangedMOD?.Invoke(this, item);
                ChangedMAX?.Invoke(this, item);
            }
        }
        #endregion
    }
}
