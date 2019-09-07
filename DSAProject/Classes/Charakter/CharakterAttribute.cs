using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class CharakterAttribute : ICharakterAttribut
    {
        #region Events
        public event EventHandler<CharakterAttribut> ChangedAttributAKTEvent;
        public event EventHandler<CharakterAttribut> ChangedAttributMODEvent;
        public event EventHandler<CharakterAttribut> ChangedAttributMAXEvent;
        #endregion
        #region Properties
        public List<CharakterAttribut> UsedAttributs { get => aktValues.Keys.ToList(); }
        public int GetSumValueAttributeAKT
        {
            get => aktValues.Sum(x => x.Value);
        }
        public int GetSumValueAttributMod { get => modValue.Sum(x => x.Value); }
        #endregion
        #region Variables
        private Dictionary<CharakterAttribut, int> aktValues;
        private Dictionary<CharakterAttribut, int> modValue;
        private Dictionary<CharakterAttribut, int> limitValues;               //Dicionary für Maximale Werte, also das Maximum welches der Charakter erreichen kann
        #endregion
        public CharakterAttribute(List<CharakterAttribut> attributs)
        {
            aktValues   = new Dictionary<CharakterAttribut, int>();
            modValue    = new Dictionary<CharakterAttribut, int>();
            limitValues = new Dictionary<CharakterAttribut, int>();

            foreach(var item in attributs)
            {
                aktValues.Add(item, 0);
                modValue.Add(item, 0);
            }
        }
        #region Setter
        public void SetAttributAKTValue(CharakterAttribut attribut, int value, out Error error)
        {
            error = null; 
            try
            {
                error = null;
                var regularValue        = aktValues.TryGetValue(attribut, out int currentValue);
                var mattributMaxValues  = limitValues.TryGetValue(attribut, out int maxValue);

                if(regularValue == false)
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                } 
                else
                {
                    if (mattributMaxValues)
                    {
                        if(value > maxValue)
                        {
                            currentValue = maxValue;
                            error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Der Maximum wert des Charakters wurde überschritten" };
                        }
                    }
                    aktValues[attribut] = value;
                    ChangedAttributAKTEvent?.Invoke(this, attribut);
                    ChangedAttributMAXEvent?.Invoke(this, attribut);
                }
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterAttribute), nameof(SetAttributAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
        }
        #endregion
        #region Getter
        public int GetAttributAKTValue(CharakterAttribut attribut, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var regularValue = aktValues.TryGetValue(attribut, out int currentValue);

                if (regularValue == false)
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                }
                else
                {
                    ret = aktValues[attribut];
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterAttribute), nameof(SetAttributAKTValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        public int GetAttributMODValue(CharakterAttribut attribut, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                if (UsedAttributs.Contains(attribut))
                {
                    ret = modValue[attribut];
                } 
                else
                {
                    error = new Error { ErrorCode = ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ErrorLog, ex.Message, nameof(CharakterAttribute), nameof(GetAttributMODValue));
                error = new Error { ErrorCode = ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }
        public int GetAttributMAXValue(CharakterAttribut attribut, out Error error)
        {
            error   = null;
            var ret = -1;
            var akt = GetAttributAKTValue(attribut, out error);
            if(error == null)
            {
                var mod = GetAttributMODValue(attribut, out error);
                if(error == null)
                {
                    ret = akt + mod;
                }
            }
            return ret;
        }
        #endregion
    }
}
