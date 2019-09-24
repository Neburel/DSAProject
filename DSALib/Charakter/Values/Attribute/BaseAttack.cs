using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseAttack : AbstractAttributeValues
    {
        public BaseAttack(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Attacke-Basis";

        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Körperkraft
        };
        internal override int CalculateValue => 5;
    }
}