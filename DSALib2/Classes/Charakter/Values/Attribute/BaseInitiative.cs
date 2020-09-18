using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class BaseInitiative : AbstractAttributeValues
    {
        public BaseInitiative(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => DSALib2.Resources.InitiativeBasis;
        public override string ShortName { get => DSALib2.Resources.InitiativeBasisShort; }
        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Mut,
            CharakterAttribut.Intuition,
            CharakterAttribut.Gewandheit
        };
        internal override int CalculateValue => 5;
    }
}
