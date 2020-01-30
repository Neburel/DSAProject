﻿using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
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
        public event EventHandler<ITalent> ATChanged;
        public event EventHandler<ITalent> PAChanged;

        private ICharakter charakter;

        internal Dictionary<ITalent, int> TAWDictionary = new Dictionary<ITalent, int>();
        internal Dictionary<AbstractTalentFighting, int> ATDictionary   = new Dictionary<AbstractTalentFighting, int>();
        internal Dictionary<AbstractTalentFighting, int> PADictionary   = new Dictionary<AbstractTalentFighting, int>();
        internal Dictionary<TalentSpeaking, bool> MotherDicionary       = new Dictionary<TalentSpeaking, bool>();
        public CharakterTalente(ICharakter charakter)
        {
            this.charakter = charakter;
        }

        public void SetTAW(ITalent talent, int taw)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));

            if(TAWDictionary.TryGetValue(talent, out int innerTAW))
            {
                TAWDictionary.Remove(talent);
            }
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
            {
                var x = (AbstractTalentGeneral)talent;
                if (x.FatherTalent != null)
                {
                    var fatherTAW = GetTAW(x.FatherTalent);
                    taw = taw - fatherTAW;
                }
            }
            else if (typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
            {
                var fightTalent = (AbstractTalentFighting)talent;
                var minTaw = GetPA(fightTalent) + GetAT(fightTalent);


                if(taw < minTaw)
                {
                    taw = minTaw;
                }
            }

            TAWDictionary.Add(talent, taw);

            if(innerTAW != taw)
            {
                TaWChanged?.Invoke(this, talent);
            }
        }
        public void SetAT(AbstractTalentFighting talent, int AT)
        {
            var taw = GetMaxTaw(talent);
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

            if(innerAT != AT)
            {
                ATChanged?.Invoke(this, talent);
            }
        }
        public void SetPA(AbstractTalentFighting talent, int PA)
        {
            var taw     = GetMaxTaw(talent);
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

            if (innnerPA != PA)
            {
                PAChanged?.Invoke(this, talent);
            }
        }
        public void SetMother(TalentSpeaking talent, bool value)
        {
            MotherDicionary.Remove(talent);

            if (value)
            {
                MotherDicionary.Add(talent, value);
            }
        }

        /// <summary>
        /// Ruft die manuell gesetzen TaW ab
        /// </summary>
        /// <param name="talent"></param>
        /// <returns></returns>
        public int GetTAW(ITalent talent)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));

            int innerTAW;
            TAWDictionary.TryGetValue(talent, out innerTAW);
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
            {
                var x = (AbstractTalentGeneral)talent;
                if(x.FatherTalent != null)
                {
                    var fatherTAW = GetTAW(x.FatherTalent);
                    innerTAW += fatherTAW;
                }
            }
            return innerTAW;
        }
        /// <summary>
        /// Ruft die Bonus TaW ab die übder die Traits gesetzt werden
        /// </summary>
        /// <param name="talent"></param>
        /// <returns></returns>
        public int GetModTaW(ITalent talent)
        {
            return charakter.Traits.GetTawBonus(talent);
        }
        /// <summary>
        /// Ruft die Summe von Manuellen und Bonus TaW ab
        /// </summary>
        /// <param name="talent"></param>
        /// <returns></returns>
        public int GetMaxTaw(ITalent talent)
        {
            if (talent == null) return 0;

            var taw = GetTAW(talent);
            var bonusTaw = GetModTaW(talent);

            return taw + bonusTaw;
        }
        public int GetAT(AbstractTalentFighting talent)
        {
            if (talent == null) return 0;

            if (ATDictionary.TryGetValue(talent, out int innerTAW))
            {
                return innerTAW;
            }
            return 0;
        }
        public int GetModAT(AbstractTalentFighting talent)
        {
            if (talent == null) return 0;

            return charakter.Traits.GetATBonus(talent);
        }
        public int GetMaxAT(AbstractTalentFighting talent)
        {
            var taw         = GetAT(talent);
            var bonusTaw    = GetModAT(talent);

            return taw + bonusTaw;
        }
        public int GetPA(AbstractTalentFighting talent)
        {
            if (PADictionary.TryGetValue(talent, out int innerTAW))
            {
                return innerTAW;
            }
            return 0;
        }
        public int GetModPA(AbstractTalentFighting talent)
        {
            return charakter.Traits.GetPABonus(talent);
        }
        public int GetMaxPA(AbstractTalentFighting talent)
        {
            var taw = GetPA(talent);
            var bonusTaw = GetModPA(talent);

            return taw + bonusTaw;
        }
        public bool GetMother(TalentSpeaking talent)
        {
            return MotherDicionary.TryGetValue(talent, out bool value);
        }

        public int GetProbeValue(AbstractTalentGeneral talent)
        {
            if (talent == null) return 0;

            var value = 0;
            foreach (var item in talent.Attributs)
            {
                value += charakter.Attribute.GetAttributMAXValue(item);
            }
            return value;
        }
        public int GetATValue(AbstractTalentFighting talent)
        {
            if (talent == null) return 0;

            var type = typeof(BaseAttack);
            if (typeof(TalentRange).IsAssignableFrom(talent.GetType()))
            {
                type = typeof(BaseRange);
            }
            var baseAttack = charakter.Values.UsedValues.Where(x => x.GetType() == type).ToList()[0];
            var at = charakter.Values.GetMAXValue(baseAttack, out DSAError error) + GetMaxAT(talent);

            return at;
        }
        public int GetPAValue(AbstractTalentFighting talent)
        {
            if (talent == null) return 0;
            var baseParade  = charakter.Values.UsedValues.Where(x => x.GetType() == typeof(BaseParade)).ToList()[0];
            var pa          = charakter.Values.GetMAXValue(baseParade, out DSAError error) + GetMaxPA(talent);

            return pa;
        }
        /// <summary>
        /// Sollte auf dauer Überarbeitet und entfernt werden
        /// </summary>
        /// <param name="talent"></param>
        /// <param name="bonusTaW"></param>
        /// <param name="bonusAT"></param>
        /// <param name="bonusPA"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Stil", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "<Ausstehend>")]
        public string GetProbeString(ITalent talent, int bonusAT = 0, int bonusPA = 0)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));

            var talentType  = talent.GetType();
            string probe    = string.Empty;

            if (typeof(TalentClose).IsAssignableFrom(talentType) || typeof(TalentWeaponless).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var paValue     = GetPAValue(innertalent);
                var atValue     = GetATValue(innertalent);

                probe = (atValue).ToString(Helper.CultureInfo) + "/" + (paValue).ToString(Helper.CultureInfo);
            } 
            else if (typeof(TalentRange).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var atValue     = GetATValue(innertalent);
                probe           = (atValue).ToString(Helper.CultureInfo);
            }
            else if (typeof(AbstractTalentGeneral).IsAssignableFrom(talentType))
            {
                var innerTalent = (AbstractTalentGeneral)talent;
                var probeValue  = GetProbeValue(innerTalent);
                probe = (probeValue).ToString(Helper.CultureInfo);
            }
            else if (typeof(AbstractTalentLanguage).IsAssignableFrom(talentType))
            {
                var innerTalent = (AbstractTalentLanguage)talent;
                var value = 0;
                foreach (var item in innerTalent.Attributs)
                {
                    value = value + charakter.Attribute.GetAttributMAXValue(item);
                }

                //probe = (value + bonusTaW).ToString();
                probe = (value).ToString(Helper.CultureInfo);
            }
            else
            {
                throw new Exception();
            }

            return probe;
        }
        private string GetString(string ret, string newValue)
        {
            string retu;
            if (string.IsNullOrEmpty(ret))
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
