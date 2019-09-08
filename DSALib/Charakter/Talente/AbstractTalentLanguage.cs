using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente
{
    public class AbstractTalentLanguage : AbstractTalent
    {
        public override int BaseDeduction => 0;

        public AbstractTalentLanguage(Guid id) : base(id) { }
    }
}
