using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class WoundSwell : AbstractAttributeValues
    {
        public WoundSwell(Interfaces.ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Wundschwelle";
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
        };
        internal override int CalculateValue => 2;

    }
}
