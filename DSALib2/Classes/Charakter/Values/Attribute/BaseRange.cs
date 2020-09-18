using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class BaseRange : AbstractAttributeValues
    {
        public BaseRange(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => DSALib2.Resources.RangeBase;
        public override string ShortName { get => DSALib2.Resources.RangeBaseShort; }

        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Fingerfertigkeit,
            CharakterAttribut.Körperkraft,
        };
        internal override int CalculateValue => 5;
    }
}
