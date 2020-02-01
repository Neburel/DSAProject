using DSALib;
using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Talente
{
    public class AbstractTalentLanguage : AbstractTalent
    {
        public override int BaseDeduction => 0;
        public List<CharakterAttribut> AttributList { get; internal set; }

        public AbstractTalentLanguage(Guid id) : base(id) { }
    }
}
