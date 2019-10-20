using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    /*
     *  Values die Aufgrundlage von Attributen berechnet werden
     */
    public abstract class AbstractAttributeValues : IValue
    {
        #region Event
        public event EventHandler ValueChanged;
        #endregion
        #region Properties
        public int Value { get; private set; }
        internal abstract int CalculateValue { get; }
        protected CharakterAttribute Attribute { get; private set; }
        public abstract string Name { get; }
        public string InfoText
        {
            get
            {
                string result = string.Empty;
                foreach(var item in attributeList)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "(" + item.ToString();
                    }
                    else
                    {
                        result = result + "+" + item.ToString();
                    }
                }
                result = result + ")" + "/" + CalculateValue;
                return result;
            }
        }
        internal abstract List<CharakterAttribut> attributeList { get; }
        #endregion
        public AbstractAttributeValues(CharakterAttribute attribute)
        {
            Attribute   = attribute;
            Value       = (int) Math.Ceiling(Calculate());
            Attribute.ChangedMAX += (object sender, CharakterAttribut args) =>
            {
                var oldValue    = Value;
                var calculateV  = Calculate();
                Value           = (int)Math.Ceiling(calculateV); ;
                if (Value != oldValue)
                {
                    ValueChanged?.Invoke(this, null);
                }
            };
        }
        #region Methoden
        protected double Calculate()
        {
            double value = 0;
            foreach(var item in attributeList)
            {
               value = value + Attribute.GetAttributMAXValue(item, out Error error);
            }
            return (value) / CalculateValue;
        }
        #endregion
    }
}
