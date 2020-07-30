using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseParade : AbstractAttributeValues
    {
        public BaseParade(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => "Padrade-Basis";
        public override string ShortName { get => "PA"; }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Intuition,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Fingerfertigkeit,
        };
        internal override int CalculateValue => 5;
    }
}
