using DSALib;
using DSALib.Exceptions;
using DSALib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (attributs == null) throw new ArgumentNullException(nameof(attributs));

            usedAttributs = new List<CharakterAttribut>(attributs);
            aktValues = new Dictionary<CharakterAttribut, int>();
            modValue = new Dictionary<CharakterAttribut, int>();
            limitValues = new Dictionary<CharakterAttribut, int>();

            foreach (var item in attributs)
            {
                aktValues.Add(item, 0);
                modValue.Add(item, 0);
            }
        }
        #region Setter
        public void SetAKTValue(CharakterAttribut attribut, int value)
        {
            var regularValue = aktValues.TryGetValue(attribut, out int currentValue);
            var mattributMaxValues = limitValues.TryGetValue(attribut, out int maxValue);

            if (regularValue == false)
            {
                throw new AttributException(error: ErrorCode.InvalidValue, message: DSALib.Resources.ErrorAttributNotExist);
            }
            else
            {
                if (mattributMaxValues)
                {
                    if (value > maxValue)
                    {
                        throw new AttributException(error: ErrorCode.InvalidValue, message: DSALib.Resources.ErrorAttrbutOverMax);
                    }
                }
                aktValues[attribut] = value;
                ChangedAKT?.Invoke(this, attribut);
                ChangedMAX?.Invoke(this, attribut);
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
                throw new AttributException(error: ErrorCode.InvalidValue, message: DSALib.Resources.ErrorAttributNotExist);
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
        public int GetAttributAKTValue(CharakterAttribut attribut)
        {
            var ret = -1;

            var regularValue = aktValues.TryGetValue(attribut, out int currentValue);

            if (regularValue == false)
            {
                throw new AttributException(error: ErrorCode.InvalidValue, message: DSALib.Resources.ErrorAttributNotExist);
            }
            else
            {
                ret = aktValues[attribut];
            }
            return ret;
        }
        public int GetAttributMODValue(CharakterAttribut attribut)
        {
            int ret;
            if (UsedAttributs.Contains(attribut))
            {
                ret = modValue[attribut];
            }
            else
            {
                throw new AttributException(error: ErrorCode.InvalidValue, message: DSALib.Resources.ErrorAttributNotExist);
            }
            return ret;
        }
        public int GetAttributMAXValue(CharakterAttribut attribut)
        {
            var akt = GetAttributAKTValue(attribut);
            var mod = GetAttributMODValue(attribut);

            return akt + mod;
        }
        #endregion
    }
}
