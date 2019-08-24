using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente
{
    public class AbstractTalentFighting : AbstractTalent
    {
        public override int BaseDeduction => 5;

        public AbstractTalentFighting(Guid id) : base(id) { }


    }
}
