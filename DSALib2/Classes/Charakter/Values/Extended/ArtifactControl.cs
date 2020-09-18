using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Values.Extended
{
    public class ArtifactControl : IValue
    {
        private int value;

        //protected CharakterResources Res { get; private set; }
        //protected CharakterAttribute Attribute { get; private set; }

        public int Value { get => value; }
        public string Name => DSALib2.Resources.ArtifactControl;
        public string ShortName { get => Name; }
        public string InfoText => "MR + IN";

        public ArtifactControl()
        {
            //CharakterResources res, CharakterAttribute attribut
            //if (res == null) throw new ArgumentNullException(nameof(res));
            //else if (attribut == null) throw new ArgumentNullException(nameof(attribut));

            //Res         = res;
            //Attribute   = attribut;

        
            //value = (int)Math.Ceiling(Calculate());
        }
        private void ChangedValue()
        {
            var oldValue = this.Value;
            var calculateV = this.Calculate();
            value = (int)global::System.Math.Ceiling(calculateV); ;
        }
   


        protected double Calculate()
        {
            //double value    = 0;
            //var initative   = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition);
            //var mr          = Res.UsedValues.Where(x => x.GetType() == typeof(MagicResistance)).FirstOrDefault();
            //var mrValue     = 0;

            //if (mr != null)
            //{
            //    mrValue = Res.GetMAXValue(mr, out DSAError error);
            //}
            //value = initative + mrValue;
            //return value;

            return 0;
        }
    }
}
