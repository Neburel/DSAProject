using DSALib;
using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseRange : AbstractAttributeValues
    {
        public BaseRange(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Fernkampf-Basis";

        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Fingerfertigkeit,
            CharakterAttribut.Körperkraft,
        };
        internal override int CalculateValue => 5;
    }
}
