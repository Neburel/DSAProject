using DSALib.Interfaces;
using DSAProject.Classes.Interfaces;
using System;

namespace DSALib.Charakter.Resources
{
    public abstract class AbstractAttributeResources : IResources
    {
        #region Event
        public event EventHandler ValueChanged;
        #endregion
        public int Value { get; private set; }
        public abstract string Name { get; }
        protected ICharakterAttribut Attribute { get; private set; }
        public AbstractAttributeResources(ICharakterAttribut attribute)
        {
            Attribute = attribute;
            Value = (int)Math.Ceiling(Calculate());
            Attribute.ChangedAttributMAXEvent += (object sender, CharakterAttribut args) =>
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
        protected abstract double Calculate();
        #endregion
    }
}
