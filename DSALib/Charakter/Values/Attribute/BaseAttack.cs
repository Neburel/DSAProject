using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseAttack : AbstractAttributeValues
    {
        public BaseAttack(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Attacke-Basis";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Mut, out Error error);
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Gewandheit, out error);
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Körperkraft, out error);
            
            return (var1 + var2 + var3) / 5.0;
        }
    }
}