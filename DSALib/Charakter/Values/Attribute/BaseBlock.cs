using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseBlock : AbstractAttributeValues
    {
        public BaseBlock(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => DSALib.Resources.BlockBasis;
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
            CharakterAttribut.Fingerfertigkeit,
            CharakterAttribut.Intuition,
        };
        internal override int CalculateValue => 5;
    }
}
