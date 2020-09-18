using System;
using System.Collections.Generic;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    /// <summary>
    /// Values die Aufgrundlage von Attributen berechnet werden
    /// </summary>
    public abstract class AbstractAttributeValues : IValue
    {
        #region Properties
        public int Value { get => Calculate(); }
        internal abstract int CalculateValue { get; }
        /// <summary>
        /// Berechnungslogik für den Text
        /// </summary>
        public string InfoText
        {
            get
            {
                string result = string.Empty;
                foreach (var item in AttributeList)
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
        public abstract string Name { get; }
        public abstract string ShortName { get; }
        protected IAttributeRepository Attribute { get; private set; }
        internal abstract List<CharakterAttribut> AttributeList { get; }
        #endregion
        public AbstractAttributeValues(IAttributeRepository attribute)
        {
            Attribute   = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }
        #region Methoden      
        protected int Calculate()
        {
            double value = 0;
            foreach(var item in AttributeList)
            {
                value += Attribute.GetMAX(item);
            }
            value = (value) / CalculateValue;
            return (int)Math.Ceiling(value);
        }
        #endregion
    }
}
