using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter
{
    public class Charakter_DSA : ICharakter
    {
        public ICharakterAttribut Attribute { get; private set; }

        public Charakter_DSA()
        {
            CharakterCreateAttribute();
        }
        private void CharakterCreateAttribute()
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
                CharakterAttribut.Körperkraft,
                CharakterAttribut.Sozialstatus
            };
            Attribute = new CharakterAttribute(list);
        }
    }
}
