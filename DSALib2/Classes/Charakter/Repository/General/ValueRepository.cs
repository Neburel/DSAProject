using DSALib;
using DSALib.Charakter.Values;
using DSALib.Charakter.Values.Settable;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;
using System;

using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class ValueRepository
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
        public ValueRepository(List<IValue> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            aktValues   = new Dictionary<IValue, int>();
            modValue    = new Dictionary<IValue, int>();

            foreach(var item in values)
            {
                aktValues.Add(item, item.Value);
                modValue.Add(item, 0);

                if (typeof(AbstractChangeHandlerValue).IsAssignableFrom(item.GetType()))
                {
                    var innerItem = (AbstractChangeHandlerValue)item;
                    innerItem.ValueChanged += (sender, args) =>
                    {
                        aktValues[item] = item.Value;
                        ChangedAKTEvent?.Invoke(this, item);
                    };
                }
            }
        }
        #region Getter
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben", Justification = "<Ausstehend>")]
        public int GetAKTValue(IValue value, out DSAError error)
        {
            error   = null;
            var ret = -1;

            if (aktValues.ContainsKey(value))
            {
                ret = aktValues[value];
            }
            else
            {
                error = new DSAError { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
            }
            return ret;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben", Justification = "<Ausstehend>")]
        public int GetMODValue(IValue value, out DSAError error)
        {
            error = null;
            var ret = -1;

            try
            {
                var regularValue = modValue.TryGetValue(value, out int currentValue);

                if (regularValue == false)
                {
                    error = new DSAError { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                } 
                else
                {
                    ret = modValue[value];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
            return ret;
        }
        public int GetMAXValue(IValue value, out DSAError error)
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
                throw new Exception("", ex);
            }
            return ret;
        }
        #endregion
        #region Setter
        public void SetAKTValue(AbstractSettableValue item, int value)
        {
            if (aktValues.ContainsKey(item))
            {
                aktValues[item] = value;
            }
            else
            {
                aktValues.Add(item, value);
            }
        }
        /// <summary>
        /// Internal da es Von dem Charakter gesteuert wird
        /// Die Steuerung erfolgt durch den Charakter damit der Trait nicht als Klasse übergeben werden muss und keine 
        /// weitere Abhängigkeit entsteht
        /// </summary>
        /// <param name="attribut"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Literale nicht als lokalisierte Parameter übergeben", Justification = "<Ausstehend>")]
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
