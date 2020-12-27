using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Resources
{
    public class Vitality : AbstractAttributeResources
    {
        public override string Name => DSALib2.Resources.Vitality;
        public override string ShortName => DSALib2.Resources.VitalityShort;
        public Vitality(IAttributeRepository attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
            CharakterAttribut.Konstitution,
            CharakterAttribut.Körperkraft
        };
        internal override int CalculateValue => 2;
    }
}

