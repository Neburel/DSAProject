using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Talente
{
    public class AbstractTalentLanguage : AbstractTalent
    {
        public override int BaseDeduction => 0;
        public List<CharakterAttribut> AttributList { get; internal set; }

        public AbstractTalentLanguage(Guid id) : base(id) { }
    }
}
