using DSALib.Charakter.Resources;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using System;
using System.Linq;

namespace DSALib.Charakter.Values
{
    public class ArtifactControl : IValue
    {
        protected CharakterResources Res { get; private set; }
        protected CharakterAttribute Attribute { get; private set; }


        public ArtifactControl(CharakterResources res, CharakterAttribute attribut)
        {
            Res         = res;
            Attribute   = attribut;

            Attribute.ChangedMAX += (object sender, global::DSALib.CharakterAttribut args) =>
            {
                ChangedValue();
            };
            Res.ChangedMAX += (object sender, Interfaces.IResource resource) =>
            {
                ChangedValue();
            };
            Value = (int)Math.Ceiling(Calculate());
        }
        public string Name => DSALib.Resources.ArtifactControl;

        private void ChangedValue()
        {
            var oldValue = this.Value;
            var calculateV = this.Calculate();
            Value = (int)global::System.Math.Ceiling(calculateV); ;
            if (this.Value != oldValue)
            {
                ValueChanged?.Invoke(this, null);
            }
        }

        //public event EventHandler ValueChanged;
        public int Value { get; private set; }
        public string InfoText => "IN + MR";

        public event EventHandler ValueChanged;

        protected double Calculate()
        {
            double value    = 0;
            var initative   = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition);
            var mr          = Res.UsedValues.Where(x => x.GetType() == typeof(MagicResistance)).FirstOrDefault();
            var mrValue     = 0;

            if (mr != null)
            {
                mrValue = Res.GetMAXValue(mr, out Error error);
            }
            value = initative + mrValue;
            return value;
        }
    }
}
