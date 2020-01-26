﻿using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using System.Linq;

namespace DSAProject.Layout.Wrapper
{
    public class TalentWrapper : AbstractPropertyChanged
    {
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
        public bool ATPAVisibility
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
        public TalentWrapper()
        {
            PropertyChanged += (sender, args) =>
            {
                var talent = Talent;

                if (args.PropertyName == nameof(Talent))
                {
                    var talenttype = talent.GetType();
                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent, 0, 0);
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
                        ATPAVisibility = false;
                    }
                    else if (typeof(AbstractTalentFighting).IsAssignableFrom(talenttype))
                    {
                        var atf = (AbstractTalentFighting)talent;
                        AT = Game.Charakter.Talente.GetMaxAT(atf);
                        PA = Game.Charakter.Talente.GetMaxPA(atf);

                        ATPAVisibility = true;
                    }
                }
                else if (args.PropertyName == nameof(TaW))
                {
                    //Der Setter wird auch verwendet damit änderungen durch +/- und beidseitige Bindung ermöglicht werden
                    //daher ist ein  recht Komplexer mechanismus von nöten der erkennen kann wann nur der Max wert gesetz worden ist
                    var value = TaW;
                    var maxValue = Game.Charakter.Talente.GetMaxTaw(talent);

                    if (maxValue != value)
                    {
                        var aktValue = Game.Charakter.Talente.GetTAW(talent);
                        var modValue = Game.Charakter.Talente.GetModTaW(talent);
                        var changeValue = value - maxValue;
                        var newValue = aktValue + changeValue;

                        Game.Charakter.Talente.SetTAW(talent, newValue);

                        if (Game.Charakter.Talente.GetMaxTaw(talent) != value)
                        {
                            TaW = maxValue;
                        }
                        TaWToolTipText = newValue + "(" + modValue + ")";
                    }
                    else
                    {
                        var aktValue = Game.Charakter.Talente.GetTAW(talent);
                        var modValue = Game.Charakter.Talente.GetModTaW(talent);

                        TaWToolTipText = aktValue + "(" + modValue + ")";
                    }

                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent, 0, 0);
                }
                else if (args.PropertyName == nameof(PA))
                {
                    if (!typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                    {
                        PA = 0;
                        return;
                    }

                    var atf = (AbstractTalentFighting)talent;
                    var value = PA;
                    var maxValue = Game.Charakter.Talente.GetMaxPA(atf);

                    if (maxValue != value)
                    {
                        var aktValue = Game.Charakter.Talente.GetPA(atf);
                        var modValue = Game.Charakter.Talente.GetModPA(atf);
                        var changeValue = value - maxValue;
                        var newValue = aktValue + changeValue;

                        Game.Charakter.Talente.SetPA(atf, newValue);

                        if (Game.Charakter.Talente.GetMaxPA(atf) != value)
                        {
                            //Wert kann nicht gesetzen werden da z.B. der TaW zu klein ist
                            PA = maxValue;
                        }
                        PAToolTipText = newValue + "(" + modValue + ")";
                    }
                    else
                    {
                        var aktValue = Game.Charakter.Talente.GetPA(atf);
                        var modValue = Game.Charakter.Talente.GetModTaW(atf);

                        PAToolTipText = aktValue + "(" + modValue + ")";
                    }

                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent, 0, 0);
                }
                else if (args.PropertyName == nameof(AT))
                {
                    if (!typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                    {
                        AT = 0;
                        return;
                    }

                    var atf = (AbstractTalentFighting)talent;
                    var value = AT;
                    var maxValue = Game.Charakter.Talente.GetMaxAT(atf);

                    if (maxValue != value)
                    {
                        var aktValue = Game.Charakter.Talente.GetAT(atf);
                        var modValue = Game.Charakter.Talente.GetModAT(atf);
                        var changeValue = value - maxValue;
                        var newValue = aktValue + changeValue;

                        Game.Charakter.Talente.SetAT(atf, newValue);

                        if (Game.Charakter.Talente.GetMaxAT(atf) != value)
                        {
                            //Wert kann nicht gesetzen werden da z.B. der TaW zu klein ist
                            AT = maxValue;
                        }
                        ATToolTipText = newValue + "(" + modValue + ")";
                    }
                    else
                    {
                        var aktValue = Game.Charakter.Talente.GetAT(atf);
                        var modValue = Game.Charakter.Talente.GetModAT(atf);

                        ATToolTipText = aktValue + "(" + modValue + ")";
                    }

                    ProbeValue = Game.Charakter.Talente.GetProbeString(talent, 0, 0);
                }
            };
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
    }
}