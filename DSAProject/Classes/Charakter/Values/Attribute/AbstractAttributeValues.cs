using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected ICharakterAttribut Attribute { get; private set; }
        public abstract string Name { get; }
        #endregion
        public AbstractAttributeValues(ICharakterAttribut attribute)
        {
            Attribute   = attribute;
            Value       = (int) Math.Ceiling(Calculate());
            Attribute.ChangedAttributMAXEvent += (object sender, CharakterAttribut args) =>
            {
                var oldValue    = Value;
                var calculateV  = Calculate();
                Value           = (int)Math.Ceiling(calculateV); ;
                if (Value != oldValue)
                {
                    ValueChanged(this, null);
                }
            };
        }
        #region Methoden
        protected abstract double Calculate();
        #endregion
    }
}
