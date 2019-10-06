using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    public class MagicResistance : AbstractAttributeResources
    {
        public override string Name => "Magieresistenz";
        public MagicResistance(ICharakterAttribut attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Klugheit,
            CharakterAttribut.Konstitution
        };
        internal override int CalculateValue => 5;
    }
}
