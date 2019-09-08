using DSALib;
using DSALib.Utils;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class WoundSwell : AbstractAttributeValues
    {
        public WoundSwell(Interfaces.ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Wundschwelle";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Konstitution, out Error error);
            return (var1) / 2.0;
        }
    }
}
