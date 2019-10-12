using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseInitiative : AbstractAttributeValues
    {
        public BaseInitiative(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => "Initiative-Basis";
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Mut,
            CharakterAttribut.Intuition,
            CharakterAttribut.Gewandheit
        };
        internal override int CalculateValue => 5;
    }
}
