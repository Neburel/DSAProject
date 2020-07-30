using DSALib;
using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseRange : AbstractAttributeValues
    {
        public BaseRange(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => "Fernkampf-Basis";
        public override string ShortName { get => "FK"; }

        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Fingerfertigkeit,
            CharakterAttribut.Körperkraft,
        };
        internal override int CalculateValue => 5;
    }
}
