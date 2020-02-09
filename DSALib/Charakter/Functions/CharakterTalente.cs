using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
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
        #region Events#
        public event EventHandler<ITalent> TaWChanged;
        public event EventHandler<ITalent> ATChanged;
        public event EventHandler<ITalent> PAChanged;
        #endregion
        #region Variables
        private readonly ICharakter charakter;
        #endregion
        #region Properties
        internal Dictionary<ITalent, int> TAWDictionary { get; set; } = new Dictionary<ITalent, int>();
        internal Dictionary<AbstractTalentFighting, int> ATDictionary { get; set; } = new Dictionary<AbstractTalentFighting, int>();
        internal Dictionary<AbstractTalentFighting, int> PADictionary { get; set; } = new Dictionary<AbstractTalentFighting, int>();
        internal Dictionary<TalentSpeaking, bool> MotherDicionary { get; set; } = new Dictionary<TalentSpeaking, bool>();
        #endregion
        #region Variables
        private List<TalentDeductionTalent> aktivDeductionList  = new List<TalentDeductionTalent>();
        private Dictionary<ITalent, int> deductionDictionary    = new Dictionary<ITalent, int>();
        #endregion
        /// <summary>
        /// Die TalentListe wird für die Ableitungen benötigt
        /// Ansonsten kann sie auch mit allen Talenten umgehen die anderweitig gesetzt werden
        /// </summary>
        /// <param name="charakter"></param>
        /// <param name="talentList"></param>
        public CharakterTalente(ICharakter charakter, List<ITalent> talentList)
        {
            Dictionary<ITalent, List<ITalent>> deductionListDictionary  = new Dictionary<ITalent, List<ITalent>>();

            this.charakter              = charakter;
            var talentWithDeductionList = talentList.Where(x => x.Deductions != null && x.Deductions.Where(y => y.GetType() == typeof(TalentDeductionTalent)).Any()).ToList();

            #region Erstelle ein zuordnungs Dicionary von einem Talent zu einer Liste von Talenten die einen wert bekommen
            foreach (var talent in talentWithDeductionList)
            {
                var talentDeductionList = talent.Deductions.Where(x => x.GetType() == typeof(TalentDeductionTalent)).ToList();

                foreach (TalentDeductionTalent talentDeduction in talentDeductionList)
                {
                    var innertalent = talentDeduction.Talent;
                    if (deductionListDictionary.TryGetValue(innertalent, out List<ITalent> deductioninnerList))
                    {
                        deductioninnerList.Add(talent);
                    }
                    else
                    {
                        var newList = new List<ITalent>
                        {
                            talent
                        };
                        deductionListDictionary.Add(innertalent, newList);
                    }
                }
            }
            #endregion
            TaWChanged += (sender, args) =>
            {
                var maxTaw = GetMaxTaw(args);
                if (!deductionListDictionary.TryGetValue(args, out List<ITalent> list)) return;

                foreach (var itemTalent in list)
                {
                    var talentDeductionList = itemTalent.Deductions.Where(x => x.GetType() == typeof(TalentDeductionTalent) && ((TalentDeductionTalent)x).Talent == args).ToList();
                    foreach(TalentDeductionTalent deduction in talentDeductionList)
                    {
                        var modValue = 0;

                        if(deduction.Value == maxTaw && !aktivDeductionList.Contains(deduction))
                        {
                            modValue = 1;
                            aktivDeductionList.Add(deduction);
                        }
                        else if(deduction.Value > maxTaw && aktivDeductionList.Contains(deduction))
                        {
                            modValue = -1;
                            aktivDeductionList.Remove(deduction);
                        }
                        if(modValue != 0)
                        {
                            var currentValue = GetDeductionValue(itemTalent);
                            SetDeductionValue(itemTalent, currentValue + modValue);
                            TaWChanged(this, itemTalent);
                        }
                    }
                }
            };
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
        private void SetDeductionValue(ITalent talent, int value)
        {
            if (deductionDictionary.ContainsKey(talent))
            {
                deductionDictionary.Remove(talent);
            }
            deductionDictionary.Add(talent, value);
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
            var traitValue = charakter.Traits.GetTawBonus(talent);
            deductionDictionary.TryGetValue(talent, out int deductionValue);

            return traitValue + deductionValue;
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
        private int GetDeductionValue(ITalent talent)
        {
            deductionDictionary.TryGetValue(talent, out int value);
            return value;
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
        public int GetProbeValue(AbstractTalentLanguage talent)
        {
            if (talent == null) return 0;
            var value = 0;
            foreach (var item in talent.AttributList)
            {
                value += charakter.Attribute.GetAttributMAXValue(item);
            }
            return value;
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
                probe = (GetProbeValue(innerTalent).ToString(Helper.CultureInfo));
            }
            else
            {
                throw new Exception();
            }

            return probe;
        }
    }
}
