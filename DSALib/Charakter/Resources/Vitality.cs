using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    public class Vitality : AbstractAttributeResources
    {
        public override string Name => DSALib.Resources.Vitality;
        public Vitality(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
            CharakterAttribut.Konstitution,
            CharakterAttribut.Körperkraft
        };
        internal override int CalculateValue => 2;
    }
}

