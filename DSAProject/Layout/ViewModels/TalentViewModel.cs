﻿using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Layout.ViewModels
{
    public class TalentViewModel : AbstractPropertyChanged
    {
        private TalentDeductionTalent deselectDeductionTalent = new TalentDeductionTalent(null, 0, 0, "Abwählen");
        public int TaW
        {
            get => Get<int>();
            set => Set(value);
        }
        public int AT
        {
            get => Get<int>();
            set => Set(value);
        }
        public int PA
        {
            get => Get<int>();
            set => Set(value);
        }
        public int BL
        {
            get => Get<int>();
            set => Set(value);
        }

        public bool ATVisibility
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool PABLVisibility
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool ProbeTextVisibility
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool DeductionChooserVisibility
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string ProbeText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ProbeValue
        {
            get => Get<string>();
            set => Set(value);
        }
        public string DiceResult
        {
            get => Get<string>();
            set => Set(value);
        }
        public string TaWToolTipText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ATToolTipText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string PAToolTipText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string BLToolTipText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string DeductionStringTalent
        {
            get => Get<string>();
            set => Set(value);
        }
        public string DeductionStringFreeText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string RequirementStringFreeText
        {
            get => Get<string>();
            set => Set(value);
        }
        public string RequirementStringRest
        {
            get => Get<string>();
            set => Set(value);
        }
        public ITalent Talent
        {
            get => Get<ITalent>();
            set => Set(value);
        }
        public TalentDeductionTalent DeductionSelectedItem
        {
            get => Get<TalentDeductionTalent>();
            set => Set(value);
        }
        public TalentDeductionTalent DeductionSelectedValue
        {
            get => Get<TalentDeductionTalent>();
            set => Set(value);
        }
        public int SelectedIndex
        {
            get => Get<int>();
            set => Set(value);
        }

        public List<TalentDeductionTalent> TalentDeductionList
        {
            get => Get<List<TalentDeductionTalent>>();
            set => Set(value);
        }
        public TalentViewModel(DiceChanger helper)
        {
            SelectedIndex = -1;
            helper.PropertyChanged += (sender, args) =>
            {
                DiceChanged(helper.DiceResult);
            };

            PropertyChanged += (sender, args) =>
            {
                var talent = Talent;

                if (args.PropertyName == nameof(Talent))
                {
                    TalentChanged(Talent);
                }
                else if (args.PropertyName == nameof(TaW))
                {
                    TaWChanged(talent, TaW);
                }
                else if (args.PropertyName == nameof(PA))
                {
                    var atf = (AbstractTalentFighting)talent;
                    PA = CalculateFightingValue(DSALib.FightingValue.Parade, atf, PA);
                    PAToolTipText = GetTooltip(DSALib.FightingValue.Parade,  atf);
                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent);
                }
                else if (args.PropertyName == nameof(AT))
                {
                    var atf = (AbstractTalentFighting)talent;
                    AT = CalculateFightingValue(DSALib.FightingValue.Attacke, atf, AT);
                    PAToolTipText = GetTooltip(DSALib.FightingValue.Attacke, atf);
                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent);
                }
                else if (args.PropertyName == nameof(BL))
                {
                    var atf = (AbstractTalentFighting)talent;
                    BL = CalculateFightingValue(DSALib.FightingValue.Blocken, atf, BL) ;
                    BLToolTipText = GetTooltip(DSALib.FightingValue.Blocken, atf);
                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent);
                }
                else if (args.PropertyName == nameof(DeductionSelectedItem))
                {
                    if(DeductionSelectedItem != null && DeductionSelectedItem != DeductionSelectedValue)
                    {
                        if(deselectDeductionTalent == DeductionSelectedItem)
                        {
                            DeductionSelectedItem = null;
                            SelectedIndex = -1;
                        }
                        Game.Charakter.Talente.SetDeduction(talent, DeductionSelectedItem);
                        DeductionSelectedValue = DeductionSelectedItem;
                    }
                }
            };
            Game.Charakter.Talente.TaWChanged += (sender, args) =>
            {
                if (args == Talent)
                {
                    var maxValue = Game.Charakter.Talente.GetMaxTaw(args);
                    if(maxValue != TaW)
                    {
                        TaW = maxValue;
                        SetTaWToolTipText();
                    }
                }
            };           
        }
        private void TalentChanged(ITalent talent)
        {
            var talenttype = talent.GetType();
            ProbeValue = Game.Charakter.Talente.GetProbeString(talent);
            TaW = Game.Charakter.Talente.GetMaxTaw(talent); //TAW wird beidseitig gesetzt, aufgrund dessen wird die Propertie genutzt um eine änderung festzusstellen und weiterzugeben

            var freeText = string.Empty;
            var restString = string.Empty;

            #region Deductions
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
            DeductionStringFreeText = freeText;
            DeductionStringTalent = restString;

            TalentDeductionList = talent.Deductions.Where(x => x.GetType().IsAssignableFrom(typeof(TalentDeductionTalent))).Cast<TalentDeductionTalent>().ToList();
            if (!TalentDeductionList.Any())
            {
                DeductionSelectedValue = null;
                DeductionChooserVisibility = false;
            }
            else
            {
                TalentDeductionList.Add(deselectDeductionTalent);
                DeductionSelectedValue = Game.Charakter.Talente.GetDeduction(talent);
                DeductionChooserVisibility = true;
            }
            #endregion

            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
            {
                var atg = (AbstractTalentGeneral)talent;
                ProbeText = atg.GetProbeText();

                #region Requirements
                freeText = string.Empty;
                restString = string.Empty;

                foreach (var requirement in atg.Requirements)
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
                RequirementStringFreeText = freeText;
                RequirementStringRest = restString;
                #endregion
                ATVisibility = false;
                ProbeTextVisibility = true;
            }
            else if (typeof(AbstractTalentFighting).IsAssignableFrom(talenttype))
            {
                var atf = (AbstractTalentFighting)talent;
                AT = Game.Charakter.Talente.GetMaxFightingValue(DSALib.FightingValue.Attacke, atf);
                PA = Game.Charakter.Talente.GetMaxFightingValue(DSALib.FightingValue.Parade, atf);
                BL = Game.Charakter.Talente.GetMaxFightingValue(DSALib.FightingValue.Blocken, atf);

                ATVisibility = true;
                PABLVisibility = true;
                ProbeTextVisibility = false;

                if (typeof(TalentRange).IsAssignableFrom(talenttype))
                {
                    PABLVisibility = false;
                }

            }
        }
        private void DiceChanged(int diceResult)
        {
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(Talent.GetType()))
            {
                var atg = (AbstractTalentGeneral)Talent;
                var probeValue = Game.Charakter.Talente.GetProbeValue(atg);
                DiceResult = (probeValue - diceResult).ToString();
            }
            if (typeof(AbstractTalentFighting).IsAssignableFrom(Talent.GetType()))
            {
                var atf = (AbstractTalentFighting)Talent;
                var probeValue = (Game.Charakter.Talente.GetATValue(atf) - diceResult).ToString();
                var probeValue2 = (Game.Charakter.Talente.GetPAValue(atf) - diceResult).ToString();

                if (typeof(TalentRange).IsAssignableFrom(Talent.GetType()))
                {
                    DiceResult = probeValue;
                }
                else
                {
                    DiceResult = probeValue + "/" + probeValue2;
                }
            }
        }
        private void TaWChanged(ITalent talent, int tawValue)
        {
            //Der Setter wird auch verwendet damit änderungen durch +/- und beidseitige Bindung ermöglicht werden
            //daher ist ein  recht Komplexer mechanismus von nöten der erkennen kann wann nur der Max wert gesetz worden ist
            var maxValue = Game.Charakter.Talente.GetMaxTaw(talent);

            if (maxValue != tawValue)
            {
                #region Funktion beim Ändern von TaW über die Seite
                var aktValue = Game.Charakter.Talente.GetTAW(talent);
                var modValue = Game.Charakter.Talente.GetModTaW(talent);
                var changeValue = tawValue - maxValue;
                var newValue = aktValue + changeValue;

                Game.Charakter.Talente.SetTAW(talent, newValue);

                if (Game.Charakter.Talente.GetMaxTaw(talent) != tawValue)
                {
                    TaW = maxValue;
                }
                SetTaWToolTipText(newValue, modValue);
                #endregion
            }
            else
            {
                SetTaWToolTipText();
            }

            ProbeValue = Game.Charakter.Talente.GetProbeString(talent);
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
        private void SetTaWToolTipText()
        {
            int aktValue = Game.Charakter.Talente.GetTAW(Talent);
            int modValue = Game.Charakter.Talente.GetModTaW(Talent);

            SetTaWToolTipText(aktValue, modValue);
        }
        private void SetTaWToolTipText(int aktValue, int modValue)
        {
            TaWToolTipText = aktValue + "(" + modValue + ")";
        }
        private int CalculateFightingValue(DSALib.FightingValue fightingvalue, AbstractTalentFighting talent, int value)
        {
            if (typeof(TalentRange).IsAssignableFrom(talent.GetType()) && fightingvalue != DSALib.FightingValue.Attacke)
            {
                return value;
            }
                       
            var maxValue = Game.Charakter.Talente.GetMaxFightingValue(fightingvalue, talent);

            if (maxValue != value)
            {
                var aktValue = Game.Charakter.Talente.GetFightingValue(fightingvalue, talent);
                var changeValue = value - maxValue;
                var newValue = aktValue + changeValue;

                Game.Charakter.Talente.SetFightingValue(fightingvalue, talent, newValue);

                if (Game.Charakter.Talente.GetMaxFightingValue(fightingvalue, talent) != value)
                {
                    //Wert kann nicht gesetzen werden da z.B. der TaW zu klein ist
                    return maxValue;
                }
            }

            return value;
        }
        private string GetTooltip(DSALib.FightingValue fightingvalue, AbstractTalentFighting talent)
        {
            var aktValue = Game.Charakter.Talente.GetFightingValue(fightingvalue, talent);
            var modValue = Game.Charakter.Talente.GetModFightingValue(fightingvalue, talent);

            return aktValue + "(" + modValue + ")";
        }
    }
    public class DiceChanger : AbstractPropertyChanged
    {
        public int DiceResult
        {
            get => Get<int>();
            set => Set(value);
        }
    }
}