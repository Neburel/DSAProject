using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Resources
{
    public abstract class AbstractAttributeResources : IResource
    {
        public int Value { get => Calculate(); }
        public abstract string Name { get; }
        public abstract string ShortName { get; }
        protected IAttributeRepository Attribute { get; private set; }
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
        public AbstractAttributeResources(IAttributeRepository attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            Attribute = attribute;
        }
        #region Methoden
        protected int Calculate()
        {
            double value = 0;
            foreach (var item in attributeList)
            {
                value = value + Attribute.GetMAX(item);
            }
            if(CalculateValue != 0)
            {
                value = (value) / CalculateValue;
            }
            return (int)Math.Ceiling(value);
        }
        #endregion
    }
}
