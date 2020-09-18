using System.Collections.Generic;

namespace DSALib2.Utils
{
    public static class DSAUtil
    {
        public static List<CharakterAttribut> getDSAAttributList()
        {
            return new List<CharakterAttribut>
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
        }
    }
}
