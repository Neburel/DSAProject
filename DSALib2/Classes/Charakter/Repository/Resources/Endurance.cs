using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Resources
{
    public class Endurance : AbstractAttributeResources
    {
        public override string Name => DSALib2.Resources.Endurance;
        public override string ShortName => DSALib2.Resources.EnduranceShort;
        public Endurance(IAttributeRepository attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Konstitution
        };
        internal override int CalculateValue => 2;
    }
}
