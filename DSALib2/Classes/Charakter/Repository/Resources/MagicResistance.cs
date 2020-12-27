using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Resources
{
    public class MagicResistance : AbstractAttributeResources
    {
        public override string Name => DSALib2.Resources.MagicResistance;
        public override string ShortName => DSALib2.Resources.MagicResistanceShort;
        public MagicResistance(IAttributeRepository attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Klugheit,
            CharakterAttribut.Konstitution
        };
        internal override int CalculateValue => 5;
    }
}
