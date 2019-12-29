using DSALib;
using DSALib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class CharakterAttribute
    {
        #region Events
        public event EventHandler<CharakterAttribut> ChangedAKT;
        public event EventHandler<CharakterAttribut> ChangedMOD;
        public event EventHandler<CharakterAttribut> ChangedMAX;
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
        private List<CharakterAttribut> usedAttributs;
        private Dictionary<CharakterAttribut, int> aktValues;
        private Dictionary<CharakterAttribut, int> modValue;
        private Dictionary<CharakterAttribut, int> limitValues;               //Dicionary für Maximale Werte, also das Maximum welches der Charakter erreichen kann
        #endregion
        public CharakterAttribute(List<CharakterAttribut> attributs)
        {
            usedAttributs = new List<CharakterAttribut>(attributs);
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
        public void SetAKTValue(CharakterAttribut attribut, int value, out Error error)
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
                    ChangedAKT?.Invoke(this, attribut);
                    ChangedMAX?.Invoke(this, attribut);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Internal da es Von dem Charakter gesteuert wird
        /// Die Steuerung erfolgt durch den Charakter damit der Trait nicht als Klasse übergeben werden muss und keine 
        /// weitere Abhängigkeit entsteht
        /// </summary>
        /// <param name="attribut"></param>
        /// <param name="value"></param>
        internal void SetModValue(CharakterAttribut attribut, int value)
        {
            if (!usedAttributs.Contains(attribut))
            {
                throw new ArgumentException("Der Parameter wird nicht verwendet", nameof(attribut));
            }
            else
            {
                modValue.Remove(attribut);
                modValue.Add(attribut, value);

                ChangedMOD?.Invoke(this, attribut);
                ChangedMAX?.Invoke(this, attribut);
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
                throw ex;
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
                throw ex;
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
