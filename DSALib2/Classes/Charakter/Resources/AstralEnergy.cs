using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Resources
{
    public class AstralEnergy : AbstractAttributeResources
    {
        public override string Name => DSALib2.Resources.AstralEnergy;
        public override string ShortName => DSALib2.Resources.AstralEnergyShort;
        public AstralEnergy(IAttributeRepository attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Intuition,
            CharakterAttribut.Charisma
        };
        internal override int CalculateValue => 2;
    }
}
