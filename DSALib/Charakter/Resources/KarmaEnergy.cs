using DSAProject.Classes.Interfaces;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergy : AbstractAttributeResources
    {
        public override string Name => "Ausdauer";
        private KarmaEnergy(ICharakterAttribut attribute) : base(attribute) { }
        protected override double Calculate()
        {
            return 0;
        }
    }
}
