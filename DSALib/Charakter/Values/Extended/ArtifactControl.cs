using DSALib.Charakter.Resources;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using System;
using System.Linq;

namespace DSALib.Charakter.Values
{
    public class ArtifactControl : AbstractChangeHandlerValue
    {
        public override event EventHandler ValueChanged;
        protected CharakterResources Res { get; private set; }
        protected CharakterAttribute Attribute { get; private set; }

        private int value;
        public override int Value { get => value; }

        public ArtifactControl(CharakterResources res, CharakterAttribute attribut)
        {
            if (res == null) throw new ArgumentNullException(nameof(res));
            else if (attribut == null) throw new ArgumentNullException(nameof(attribut));

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
            value = (int)Math.Ceiling(Calculate());
        }
        private void ChangedValue()
        {
            var oldValue = this.Value;
            var calculateV = this.Calculate();
            value = (int)global::System.Math.Ceiling(calculateV); ;
            if (this.Value != oldValue)
            {
                ValueChanged?.Invoke(this, null);
            }
        }
        public override string Name => DSALib.Resources.ArtifactControl;
        public override string InfoText => "MR + IN";


        protected double Calculate()
        {
            double value    = 0;
            var initative   = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition);
            var mr          = Res.UsedValues.Where(x => x.GetType() == typeof(MagicResistance)).FirstOrDefault();
            var mrValue     = 0;

            if (mr != null)
            {
                mrValue = Res.GetMAXValue(mr, out DSAError error);
            }
            value = initative + mrValue;
            return value;
        }
    }
}
