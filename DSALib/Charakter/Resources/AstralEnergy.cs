
using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    public class AstralEnergy : AbstractAttributeResources
    {
        public override string Name => "Astralenergie";
        public AstralEnergy(ICharakterAttribut attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Intuition,
            CharakterAttribut.Charisma
        };
        internal override int CalculateValue => 2;
    }
}
