﻿using DSALib;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseBlock : AbstractAttributeValues
    {
        public BaseBlock(CharakterAttribute attribute) : base(attribute) { }
        public override string Name => DSALib.Resources.BlockBasis;
        public override string ShortName { get => "BL"; }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Konstitution,
            CharakterAttribut.Körperkraft,
            CharakterAttribut.Intuition,
        };
        internal override int CalculateValue => 5;
    }
}
