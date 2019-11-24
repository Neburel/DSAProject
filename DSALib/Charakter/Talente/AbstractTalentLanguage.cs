using System;

namespace DSAProject.Classes.Charakter.Talente
{
    public class AbstractTalentLanguage : AbstractTalent
    {
        public override int BaseDeduction => 0;

        public AbstractTalentLanguage(Guid id) : base(id) { }
    }
}
