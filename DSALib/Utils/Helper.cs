using DSALib.Charakter.Resources;
using DSALib.Interfaces;
using DSAProject.Classes.Charakter.Values;
using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Utils
{
    public static class Helper
    {
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
        public static string GetShort(IValue value)
        {
            var ret = string.Empty;
            if (value == null) throw new ArgumentNullException(nameof(value));
            if(typeof(BaseAttack) == value.GetType())
            {
                ret = "AT";
            }
            else if(typeof(BaseInitiative) == value.GetType())
            {
                ret = "INI";
            }
            else if (typeof(BaseParade) == value.GetType())
            {
                ret = "PA";
            }
            else if (typeof(BaseRange) == value.GetType())
            {
                ret = "FK";
            }
            else if (typeof(WoundSwell) == value.GetType())
            {
                ret = "WS";
            }
            else if (typeof(ControllValue) == value.GetType())
            {
                ret = "BW";
            }
            else if (typeof(Rapture) == value.GetType())
            {
                ret = "ER";
            }
            else if (typeof(SpeedAir) == value.GetType())
            {
                ret = nameof(SpeedAir);
            }
            else if (typeof(SpeedLand) == value.GetType())
            {
                ret = nameof(SpeedLand);
            }
            else if (typeof(SpeedWater) == value.GetType())
            {
                ret = nameof(SpeedWater);
            }
            else
            {
                ret = nameof(value);
            }



            return ret;
        }
        public static string GetShort(IResource value)
        {
            var ret = string.Empty;
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (typeof(AstralEnergy) == value.GetType())
            {
                ret = "AP";
            }
            else if (typeof(Endurance) == value.GetType())
            {
                ret = "AU";
            }
            else if (typeof(KarmaEnergy) == value.GetType())
            {
                ret = "KE";
            }
            else if (typeof(MagicResistance) == value.GetType())
            {
                ret = "MR";
            }
            else if (typeof(Vitality) == value.GetType())
            {
                ret = "LE";
            }
            else
            {
                ret = nameof(value);
            }
            return ret;
        }
    }
}
