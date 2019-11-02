using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public class CharakterTalente
    {
        public event EventHandler<ITalent> TaWChanged;

        private ICharakter charakter;

        internal Dictionary<ITalent, int> TAWDictionary = new Dictionary<ITalent, int>();
        internal Dictionary<AbstractTalentFighting, int> ATDictionary  = new Dictionary<AbstractTalentFighting, int>();
        internal Dictionary<AbstractTalentFighting, int> PADictionary  = new Dictionary<AbstractTalentFighting, int>();

        public CharakterTalente(ICharakter charakter)
        {
            this.charakter = charakter;
        }

        public void SetTAW(ITalent talent, int taw)
        {
            if(TAWDictionary.TryGetValue(talent, out int innerTAW))
            {
                TAWDictionary.Remove(talent);
            }
            TAWDictionary.Add(talent, taw);

            TaWChanged?.Invoke(this, talent);
        }
        public void SetAT(AbstractTalentFighting talent, int AT)
        {
            var taw = GetTAW(talent);
            var pa  = GetPA(talent);
            var maxAT = taw - pa;
            var newAT = 0;

            if(AT <= maxAT)
            {
                newAT = AT;
            } 
            else
            {
                newAT = maxAT;
            }
            if(ATDictionary.TryGetValue(talent, out int innerAT))
            {
                ATDictionary.Remove(talent);
            }
            ATDictionary.Add(talent, newAT);

        }
        public void SetPA(AbstractTalentFighting talent, int PA)
        {
            var taw     = GetTAW(talent);
            var at      = GetAT(talent);
            var maxPA   = taw - at;
            var newPA   = 0;

            if (PA <= maxPA)
            {
                newPA = PA;
            } 
            else
            {
                newPA = maxPA;
            }
            if (PADictionary.TryGetValue(talent, out int innnerPA))
            {
                PADictionary.Remove(talent);
            }
            PADictionary.Add(talent, newPA);
        }

        public int GetTAW(ITalent talent)
        {
            var innerTAW = 0;
            TAWDictionary.TryGetValue(talent, out innerTAW);
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
            {
                var x = (AbstractTalentGeneral)talent;
                if(x.FatherTalent != null)
                {
                    innerTAW = innerTAW + GetTAW(x.FatherTalent);
                }
            }

            return innerTAW;
        }
        public int GetAT(AbstractTalentFighting talent)
        {
            if (ATDictionary.TryGetValue(talent, out int innerTAW))
            {
                return innerTAW;
            }
            return 0;
        }
        public int GetPA(AbstractTalentFighting talent)
        {
            if (PADictionary.TryGetValue(talent, out int innerTAW))
            {
                return innerTAW;
            }
            return 0;
        }

        public string GetProbeString(ITalent talent, int bonusTaW = 0, int bonusAT = 0, int bonusPA = 0)
        {
            var talentType  = talent.GetType();
            Error error     = null;
            string probe    = string.Empty;


            if (typeof(TalentClose).IsAssignableFrom(talentType) || typeof(TalentWeaponless).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var baseAttack  = charakter.Values.UsedValues.Where(x => x.GetType() == typeof(BaseAttack)).ToList()[0];
                var baseParade  = charakter.Values.UsedValues.Where(x => x.GetType() == typeof(BaseParade)).ToList()[0];

                var at = charakter.Values.GetMAXValue(baseAttack, out error) + GetAT(innertalent) + bonusAT;
                var pa = charakter.Values.GetMAXValue(baseParade, out error) + GetPA(innertalent) + bonusPA;

                probe = (at + bonusAT).ToString() + "/" + (pa + bonusPA).ToString();
            } 
            else if (typeof(TalentRange).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var baseAttack  = charakter.Values.UsedValues.Where(x => x.GetType() == typeof(BaseRange)).ToList()[0];
                var at          = charakter.Values.GetMAXValue(baseAttack, out error) + GetAT(innertalent) + bonusAT;

                probe           = (at + bonusAT).ToString();
            }
            else if (typeof(AbstractTalentGeneral).IsAssignableFrom(talentType))
            {
                var innerTalent = (AbstractTalentGeneral)talent;
                var value = 0;
                foreach(var item in innerTalent.Attributs)
                {
                    value = value +charakter.Attribute.GetAttributMAXValue(item, out error);
                }

                probe = (value + bonusTaW).ToString();
            }
            else
            {
                throw new Exception();
            }

            return probe;
        }
        private string GetString(string ret, string newValue)
        {
            var retu = string.Empty;
            if (ret == string.Empty)
            {
                retu = newValue;
            }
            else
            {
                retu = ret + ", " + newValue;
            }


            return retu;
        }
    }
}
