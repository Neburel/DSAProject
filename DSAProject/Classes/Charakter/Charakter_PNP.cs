using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSAProject.Classes.Interfaces;

namespace DSAProject.Classes.Charakter
{
    public class Charakter_PNP : AbstractCharakter
    {
        protected override ICharakterAttribut CharakterCreateAttribute()
        {
            var list = new List<CharakterAttribut>
            {
                CharakterAttribut.Mut,
                CharakterAttribut.Klugheit,
                CharakterAttribut.Intuition,
                CharakterAttribut.Charisma,
                CharakterAttribut.Fingerfertigkeit,
                CharakterAttribut.Gewandheit,
                CharakterAttribut.Konstitution,
                CharakterAttribut.Körperkraft
            };
            return new CharakterAttribute(list);
        }
    }
}
