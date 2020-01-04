using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergyMinus : AbstractAttributeResources
    {
        public override string Name => "Karma Negativ";
        public KarmaEnergyMinus(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
        };
        internal override int CalculateValue => 0;
    }
}
