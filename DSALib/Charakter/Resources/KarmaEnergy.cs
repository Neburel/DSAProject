using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergy : AbstractAttributeResources
    {
        public override string Name => "KarmaEnergie";
        public KarmaEnergy(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
        };
        internal override int CalculateValue => 0;
    }
}
