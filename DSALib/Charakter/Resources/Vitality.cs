using DSALib.Utils;
using DSAProject.Classes.Interfaces;

namespace DSALib.Charakter.Resources
{
    class Vitality : AbstractAttributeResources
    {
        public override string Name => "Ausdauer";
        private Vitality(ICharakterAttribut attribute) : base(attribute) { }
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Konstitution, out Error error);
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Konstitution, out error);
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Körperkraft, out error);

            if (error != null)
            {
                throw new System.Exception(error.Message);
            }

            return (var1 + var2 + var3) / 2.0;
        }
    }
}

