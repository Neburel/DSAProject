using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class BaseParade : AbstractAttributeValues
    {
        public BaseParade(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => DSALib2.Resources.ParadeBase;
        public override string ShortName { get => DSALib2.Resources.ParadeBaseShort; }
        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Fingerfertigkeit,
        };
        internal override int CalculateValue => 5;
    }
}
