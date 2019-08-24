using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente.TalentGeneral
{
    public class TalentPhysical : AbstractTalentGeneral
    {
        public TalentPhysical(Guid id, List<CharakterAttribut> attributs) : base(id, attributs) { }
    }
}
