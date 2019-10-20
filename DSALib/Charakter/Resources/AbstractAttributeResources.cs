using DSALib.Interfaces;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    public abstract class AbstractAttributeResources : IResource
    {
        #region Event
        public event EventHandler ValueChanged;
        #endregion
        public int Value { get; private set; }
        public abstract string Name { get; }
        protected CharakterAttribute Attribute { get; private set; }
        internal abstract int CalculateValue { get; }
        internal abstract List<CharakterAttribut> attributeList { get; }
        public string InfoText
        {
            get
            {
                string result = string.Empty;
                foreach (var item in attributeList)
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
                if(CalculateValue != 0)
                {
                    result = result + ")" + "/" + CalculateValue;
                }
                return result;
            }
        }
        public AbstractAttributeResources(CharakterAttribute attribute)
        {
            Attribute = attribute;
            Value = (int)Math.Ceiling(Calculate());
            Attribute.ChangedMAX += (object sender, CharakterAttribut args) =>
            {
                var oldValue = Value;
                var calculateV = Calculate();
                Value = (int)Math.Ceiling(calculateV); ;
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
            foreach (var item in attributeList)
            {
                value = value + Attribute.GetAttributMAXValue(item, out Error error);
            }
            if(CalculateValue != 0)
            {
                value = (value) / CalculateValue;
            }

            return value;
        }
        #endregion
    }
}
