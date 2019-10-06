﻿using DSAProject.Classes.Interfaces;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    public class Endurance : AbstractAttributeResources
    {
        public override string Name => "Ausdauer";
        public Endurance(ICharakterAttribut attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
            CharakterAttribut.Mut,
            CharakterAttribut.Gewandheit,
            CharakterAttribut.Konstitution
        };
        internal override int CalculateValue => 2;
    }
}
