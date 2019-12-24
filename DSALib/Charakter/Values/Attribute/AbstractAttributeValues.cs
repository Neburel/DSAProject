using DSALib;
using DSALib.Charakter.Values;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    /*
     *  Values die Aufgrundlage von Attributen berechnet werden
     */
    public abstract class AbstractAttributeValues : AbstractChangeHandlerValue
    {
        #region Event
        public override event EventHandler ValueChanged;
        #endregion
        #region Variables
        private int value;
        #endregion
        #region Properties
        public override int Value { get => value; }
        internal abstract int CalculateValue { get; }
        protected CharakterAttribute Attribute { get; private set; }
        public override abstract string Name { get; }
        public override string InfoText
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
            value       = (int) Math.Ceiling(Calculate());
            Attribute.ChangedMAX += (object sender, CharakterAttribut args) =>
            {
                var oldValue    = Value;
                var calculateV  = Calculate();
                value           = (int)Math.Ceiling(calculateV); ;
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
