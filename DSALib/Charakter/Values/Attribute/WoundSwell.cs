using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class WoundSwell : AbstractAttributeValues
    {
        public WoundSwell(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => "Wundschwelle";
        public override string ShortName { get => "WS"; }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
        };
        internal override int CalculateValue => 2;
    }
}
