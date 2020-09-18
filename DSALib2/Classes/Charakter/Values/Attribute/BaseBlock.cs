using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class BaseBlock : AbstractAttributeValues
    {
        public BaseBlock(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => DSALib2.Resources.BlockBasis;
        public override string ShortName { get => DSALib2.Resources.BlockBasisShort; }
        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
            CharakterAttribut.Körperkraft,
            CharakterAttribut.Intuition,
        };
        internal override int CalculateValue => 5;
    }
}
