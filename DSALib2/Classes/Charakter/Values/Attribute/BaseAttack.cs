using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Values.Attribute
{
    public class BaseAttack : AbstractAttributeValues
    {
        public BaseAttack(IAttributeRepository attribute) : base(attribute) { }
        public override string Name => "Attacke-Basis";
        public override string ShortName { get => "AT"; }
        internal override List<CharakterAttribut> AttributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Körperkraft
        };
        internal override int CalculateValue => 5;
    }
}