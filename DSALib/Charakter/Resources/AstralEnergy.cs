
using DSALib.Utils;
using DSAProject.Classes.Interfaces;

namespace DSALib.Charakter.Resources
{
    public class AstralEnergy : AbstractAttributeResources
    {
        public override string Name => "Astralenergie";
        private AstralEnergy(ICharakterAttribut attribute) : base(attribute) { }
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Mut, out Error error);
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition, out error);
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Charisma, out error);

            if(error != null)
            {
                throw new System.Exception(error.Message);
            }

            return (var1 + var2 + var3) / 2.0;
        }
    }
}
