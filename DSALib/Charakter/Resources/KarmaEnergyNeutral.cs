﻿using DSAProject.Classes.Charakter;
using System.Collections.Generic;

namespace DSALib.Charakter.Resources
{
    class KarmaEnergyNeutral : AbstractAttributeResources
    {
        public override string Name => "Karma Neutral";
        public override string ShortName => "KEN";
        public KarmaEnergyNeutral(CharakterAttribute attribute) : base(attribute) { }
        internal override List<CharakterAttribut> attributeList => new List<CharakterAttribut>()
        {
        };
        internal override int CalculateValue => 0;
    }
}
