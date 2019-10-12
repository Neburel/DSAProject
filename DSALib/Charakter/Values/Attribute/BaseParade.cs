using DSALib;
using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseParade : AbstractAttributeValues
    {
        public BaseParade(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => "Padrade-Basis";
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Körperkraft,
        };
        internal override int CalculateValue => 5;
    }
}
