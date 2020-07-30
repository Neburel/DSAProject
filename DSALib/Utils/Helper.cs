using System.Globalization;

namespace DSALib.Utils
{
    public static class Helper
    {
        public static CultureInfo CultureInfo { get; set; } = new CultureInfo("de-DE");
        public static string GetShort(CharakterAttribut attribut)
        {
            var ret = string.Empty;

            if (attribut == CharakterAttribut.Charisma)
            {
                ret = "CH";
            }
            else if (attribut == CharakterAttribut.Fingerfertigkeit)
            {
                ret = "FF";
            }
            else if (attribut == CharakterAttribut.Gewandheit)
            {
                ret = "GE";
            }
            else if (attribut == CharakterAttribut.Intuition)
            {
                ret = "IN";
            }
            else if (attribut == CharakterAttribut.Klugheit)
            {
                ret = "KL";
            }
            else if (attribut == CharakterAttribut.Konstitution)
            {
                ret = "KO";
            }
            else if (attribut == CharakterAttribut.Körperkraft)
            {
                ret = "KK";
            }
            else if (attribut == CharakterAttribut.Mut)
            {
                ret = "MU";
            }
            else if (attribut == CharakterAttribut.Sozialstatus)
            {
                ret = "SO";
            }
            else
            {
                ret = attribut.ToString();
            }
            return ret;
        }
    }
}
