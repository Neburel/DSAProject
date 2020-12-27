using DSALib2.Charakter.Talente;
using DSALib2.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentDeductions;
using DSALib2.Classes.Charakter.Talente.TalentFighting;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente.TalentRequirement;
using DSALib2.Classes.Charakter.Values.Attribute;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralTalentRepository : ITalentRepository
    {
        private List<ITalent> talentList;
        private List<LanguageFamily> familieList;
        private AbstractCharakter charakter;

        public GeneralTalentRepository(AbstractCharakter charakter, List<ITalent> list, List<LanguageFamily> familieList)
        {
            this.talentList = list;
            this.familieList = familieList;
            this.charakter = charakter;
        }
        abstract protected int GetTAW(ITalent talent);
        abstract protected bool? GetMother(TalentSpeaking talent);
        private int GetModTaW(ITalent talent)
        {            
            return GetDeduction(talent);
        }
        private int GetMaxTaw(ITalent talent)
        {
            if (talent == null) return 0;

            var taw = GetTAW(talent);
            var bonusTaw = GetModTaW(talent);

            return taw + bonusTaw;
        }
        private int GetProbeValue(AbstractTalentGeneral talent)
        {
            if (talent == null) return 0;

            var value = 0;
            foreach (var item in talent.Attributs)
            {
                value += this.charakter.Attribute.GetMAX(item);
            }

            var maxTaw = GetMaxTaw(talent);
            var x = maxTaw / 5;

            value += GetMaxTaw(talent) / 5;
            return value;
        }
        private int GetProbeValue(AbstractTalentLanguage talent)
        {
            if (talent == null) return 0;
            var value = 0;
            foreach (var item in talent.AttributList)
            {
                value += charakter.Attribute.GetMAX(item);
            }
            value += GetMaxTaw(talent);
            return value;
        }
        private int GetProbeATValue(AbstractTalentFighting talent)
        {
            var talentType = talent.GetType();
            var valueRepo = charakter.Values;
            if (typeof(TalentWeaponless).IsAssignableFrom(talentType))
            {
                var value = valueRepo.GetItemByType(typeof(BaseAttack));
                return this.charakter.Values.GetMAX(value) + this.GetATMax(talent);
            }
            else if (typeof(TalentClose).IsAssignableFrom(talentType))
            {
                var value = valueRepo.GetItemByType(typeof(BaseAttack));
                return this.charakter.Values.GetMAX(value) + this.GetATMax(talent);
            }
            else if (typeof(TalentRange).IsAssignableFrom(talentType))
            {
                var value = valueRepo.GetItemByType(typeof(BaseRange));
                return this.charakter.Values.GetMAX(value) + this.GetATMax(talent);
            }
            throw new NotImplementedException();
        }
        private int GetProbePAValue(AbstractTalentFighting talent)
        {
            var value = charakter.Values.GetItemByType(typeof(BaseParade));
            return this.charakter.Values.GetMAX(value) + this.GetPAMax(talent);
        }
        private int GetProbeBLValue(AbstractTalentFighting talent)
        {
            var value = charakter.Values.GetItemByType(typeof(BaseBlock));
            return this.charakter.Values.GetMAX(value) + this.GetBLMax(talent);
        }
        abstract protected int GetAT(AbstractTalentFighting talent);
        abstract protected int GetPA(AbstractTalentFighting talent);
        abstract protected int GetBL(AbstractTalentFighting talent);
        private protected int GetModAT(AbstractTalentFighting talent)
        {
            return 0;
        }
        private protected int GetModPA(AbstractTalentFighting talent)
        {
            return 0;
        }
        private protected int GetModBL(AbstractTalentFighting talent)
        {
            return 0;
        }
        private int GetATMax(AbstractTalentFighting talent)
        {
            return GetAT(talent) + GetModAT(talent);
        }
        private int GetPAMax(AbstractTalentFighting talent)
        {
            return GetPA(talent) + GetModPA(talent);
        }
        private int GetBLMax(AbstractTalentFighting talent)
        {
            return GetBL(talent) + GetModBL(talent);
        }
      
        abstract protected int GetDeduction(ITalent talent);
        private string GetProbeString(ITalent talent)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));

            var talentType = talent.GetType();
            string probe;
            if (typeof(TalentClose).IsAssignableFrom(talentType) || typeof(TalentWeaponless).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var paValue = GetProbePAValue(innertalent);
                var atValue = GetProbeATValue(innertalent);
                var blValue = GetProbeBLValue(innertalent);

                probe = (atValue).ToString(Helper.CultureInfo) + "/" + (paValue).ToString(Helper.CultureInfo) + "/" + (blValue).ToString(Helper.CultureInfo);
            }
            else if (typeof(TalentRange).IsAssignableFrom(talentType))
            {
                var innertalent = (AbstractTalentFighting)talent;
                var atValue = GetProbeATValue(innertalent);
                probe = (atValue).ToString(Helper.CultureInfo);
            }
            else if (typeof(AbstractTalentGeneral).IsAssignableFrom(talentType))
            {
                var innerTalent = (AbstractTalentGeneral)talent;
                var probeValue = GetProbeValue(innerTalent);
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
        abstract protected Guid? GetDeductionID(ITalent talent);
        private TextView GetDeductionText(ITalent talent)
        {
            var freeText = "";
            var restString = "";
            foreach (var deduction in talent.Deductions)
            {
                if (typeof(TalentDeductionFreeText).IsAssignableFrom(deduction.GetType()))
                {
                    freeText = GetString(deduction.GetDeductionString(), freeText);
                }
                else if (typeof(TalentDeductionTalent).IsAssignableFrom(deduction.GetType()))
                {
                    restString = GetString(deduction.GetDeductionString(), restString, string.Empty);
                }
            }
            if (!string.IsNullOrEmpty(restString))
            {
                freeText = GetString(string.Empty, freeText, restString);
            }
            return new TextView()
            {
                Text = restString,
                FreeText = freeText
            };
        }
        private TextView GetRequirementText(AbstractTalentGeneral talent)
        {
            var freeText = "";
            var restString = "";
            foreach (var requirement in talent.Requirements)
            {
                if (typeof(TalentRequirementFreeText).IsAssignableFrom(requirement.GetType()))
                {
                    freeText = GetString(requirement.RequirementString(), freeText);
                }
                else if (typeof(TalentRequirementTalent).IsAssignableFrom(requirement.GetType()))
                {
                    restString = GetString(requirement.RequirementString(), restString, string.Empty);
                }
                else if (typeof(TalentRequirementFreeText).IsAssignableFrom(requirement.GetType()))
                {
                    restString = GetString(requirement.RequirementString(), restString, string.Empty);
                }
            }
            return new TextView
            {
                FreeText = freeText,
                Text = restString
            };
        }
        private List<DeductionView> GetDeductionViewList(ITalent talent)
        {
            var deductionList = new List<DeductionView>();
            foreach (var deduction in talent.Deductions)
            {
                if (typeof(TalentDeductionTalent).IsAssignableFrom(deduction.GetType()))
                {
                    var innerDeduction = (TalentDeductionTalent)deduction;
                    deductionList.Add(new DeductionView()
                    {
                        ID = innerDeduction.Talent.ID,
                        Name = innerDeduction.Talent.Name
                    });
                }
            }
            return deductionList;
        }
        private DeductionView GetDeductionView(ITalent talent)
        {
            var deductionID = this.GetDeductionID(talent);
            if(deductionID != null)
            {
                return new DeductionView()
                {
                    ID = (Guid)deductionID
                };
            }
            else
            {
                return null;
            }
        }
        private string GetString(string newValue, string currentText, string secondControll = null)
        {
            if ((currentText == string.Empty || currentText == null) && (secondControll == string.Empty || secondControll == null))
            {
                return newValue;
            }
            else if (string.IsNullOrEmpty(currentText) && string.IsNullOrEmpty(newValue))
            {
                return newValue;
            }
            else
            {
                return currentText.Trim() + ", " + newValue.Trim();
            }
        }

        public TalentView GetView(Guid guid)
        {
            var item = talentList.Where(x => x.ID == guid).FirstOrDefault();
            if(item != null)
            {
                return CreateTalentView(item);
            }
            return null;
        }
        public LanguageView GetLanguageView(Guid guidLanguage, Guid guidWriting)
        {
            //CS: Entprechende Mechanismen zur Sicherung müssen noch Implementiert werden
            var x = talentList.Where(x => x.ID == guidLanguage).FirstOrDefault();

            var itemlanguage = (TalentSpeaking) talentList.Where(x => x.ID == guidLanguage).FirstOrDefault();
            var itemWriting = (TalentWriting) talentList.Where(x => x.ID == guidWriting).FirstOrDefault();

            return CreateTalentView(itemlanguage, itemWriting);
        }
        public List<TalentView> GetViewList<T>() where T : ITalent
        {
            var ret = new List<TalentView>();
            var list = talentList.Where(x => x.GetType() == typeof(T)).ToList();
            foreach (var item in list.OrderBy(x => x.OrginalPosition))
            {
                var newItem = CreateTalentView(item);
                ret.Add(newItem);
            }
            return ret;
        }
        public List<LanguageView> GetViewList()
        {
            List<LanguageView> list = new List<LanguageView>();
            foreach (var item in this.familieList)
            {
                var HeaderItem = new LanguageView()
                {
                    Sprache = item.Name,
                    IsTitle = true
                };
                list.Add(HeaderItem);
                var count = item.GetHighestPosition();

                for(int i = 0; i< count; i++)
                {
                    item.Languages.TryGetValue(i, out TalentSpeaking currentLanguage);
                    item.Writings.TryGetValue(i, out TalentWriting currentWriting);

                    list.Add(CreateTalentView(currentLanguage, currentWriting));
                }
            }

            return list;
        }


        private TalentView CreateTalentView(ITalent item)
        {
            var newItem = new TalentView()
            {
                ID = item.ID,
                Name = item.Name,
                Probe = GetProbeString(item),
                BE = item.BE,
                TAW = GetMaxTaw(item),
                DeductionText = this.GetDeductionText(item),
                DeductionList = this.GetDeductionViewList(item),
                DeductionSelected = this.GetDeductionView(item)
            };
            var talentType = item.GetType();
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talentType))
            {
                var innerItem = (AbstractTalentGeneral)item;
                newItem.ProbeString = innerItem.GetProbeText();
                newItem.RequirementText = GetRequirementText(innerItem);
            }
            if (typeof(AbstractTalentFighting).IsAssignableFrom(talentType))
            {
                var innerItem = (AbstractTalentFighting)item;
                newItem.AT = GetATMax(innerItem);
                newItem.PA = GetPAMax(innerItem);
                newItem.BL = GetBLMax(innerItem);
            }

            return newItem;
        }
        private LanguageView CreateTalentView(TalentSpeaking speaking, TalentWriting writing)
        {
            var newItem = new LanguageView()
            {
                IsTitle = false
            };
            if (speaking != null)
            {
                newItem.IDSprache = speaking.ID;
                newItem.Sprache = speaking.Name;
                newItem.KomplexSprache = speaking.BE;
                newItem.Mother = GetMother(speaking);
                newItem.TawSprache = this.GetMaxTaw(speaking);
                newItem.ProbeSprache = this.GetProbeString(speaking);
            }
            if (writing != null)
            {
                newItem.IDSchrift = writing.ID;
                newItem.Schrift = writing.Name;
                newItem.KomplexSchrift = writing.BE;
                newItem.TawSchrift = this.GetMaxTaw(writing);
                newItem.ProbeSchrift = this.GetProbeString(writing);
            }
            return newItem;
        }

        public abstract void SetTalentbyView(TalentView view);
        public abstract void SetTalentbyView(LanguageView view);
    }
}
