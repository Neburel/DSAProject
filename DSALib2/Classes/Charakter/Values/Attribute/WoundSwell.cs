using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class WoundSwell : AbstractAttributeValues
    {
        public WoundSwell(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => DSALib2.Resources.WoundSwell;
        public override string ShortName { get => DSALib2.Resources.WoundSwellShort; }
        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
        };
        internal override int CalculateValue => 2;
    }
}
