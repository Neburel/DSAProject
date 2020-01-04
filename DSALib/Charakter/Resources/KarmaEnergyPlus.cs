using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergyPlus : AbstractAttributeResources
    {
        public override string Name => "Karma Positiv";
        public KarmaEnergyPlus(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
        };
        internal override int CalculateValue => 0;
    }
}
