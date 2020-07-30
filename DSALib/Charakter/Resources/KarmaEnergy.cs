using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergy : AbstractAttributeResources
    {
        public override string Name => "Karma";
        public override string ShortName => "KE";
        public KarmaEnergy(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
        };
        internal override int CalculateValue => 0;
    }
}
