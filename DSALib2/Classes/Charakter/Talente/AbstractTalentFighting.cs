using System;

namespace DSALib2.Classes.Charakter.Talente
{
    public class AbstractTalentFighting : AbstractTalent
    {
        public override int BaseDeduction => 5;

        public AbstractTalentFighting(Guid id) : base(id) { }


    }
}
