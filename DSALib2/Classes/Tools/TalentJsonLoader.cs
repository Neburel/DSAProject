using DSALib2.Charakter.Talente;
using DSALib2.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentDeductions;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente.TalentRequirement;
using DSALib2.Classes.JSONSave;
using DSALib2.Interfaces.Charakter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Tools
{
    public static class TalentJsonLoader
    {
        public static List<ITalent> LoadTalent(List<JSONTalent> talents)
        {
            var list = new List<ITalent>();
            Dictionary<ITalent, JSONTalent> talentwithDedcut = new Dictionary<ITalent, JSONTalent>();
            Dictionary<AbstractTalentGeneral, JSONTalent> talentWithRequirement = new Dictionary<AbstractTalentGeneral, JSONTalent>();

            if (talents != null)
            {
                try
                {
                    #region Talente Erstellen, Dedurctions vorbereiten
                    foreach (var item in talents)
                    {
                        var talent = TalentCreator.CreateTalent(
                            contentType: item.ContentType,
                            talentGuid: item.ID,
                            probe: item.Probe,
                            be: item.BE,
                            name: item.Name,
                            nameExtension: item.NameExtension,
                            orginalPos: item.OrginalPos);

                        if (talent != null)
                        {   
                            if ((item.DeductionTalentList != null && item.DeductionTalentList.Any()) || (item.DeductionStrings != null && item.DeductionStrings.Any()))
                            {
                                talentwithDedcut.Add(talent, item);
                            }
                            #region AbstractTalentGeneral
                            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                            {
                                var abstractTalentGeneral = (AbstractTalentGeneral)talent;
                                talentWithRequirement.Add(abstractTalentGeneral, item);
                            }
                            #endregion
                            list.Add(talent);
                            //talentGuids.Add(talent.ID);
                        }
                    }
                    #endregion
                    #region Deductions
                    foreach (var item in talentwithDedcut)
                    {
                        if (item.Value.DeductionTalentList != null)
                        {
                            foreach (var deduction in item.Value.DeductionTalentList)
                            {
                                var items = list.Where(x => x.ID == deduction.ID).ToList();
                                if (items.Count != 1)
                                {
                                    throw new Exception();
                                    //Logger.Log(LogLevel.ErrorLog, "Deuction not found: " + deduction.Key, nameof(Game), nameof(LoadTalent));
                                }
                                else
                                {
                                    item.Key.Deductions.Add(new TalentDeductionTalent(items[0], deduction.Value, item.Key.BaseDeduction, deduction.Description));
                                }

                            }
                        }
                        if (item.Value.DeductionStrings != null)
                        {
                            foreach (var deduction in item.Value.DeductionStrings)
                            {
                                item.Key.Deductions.Add(new TalentDeductionFreeText(deduction));
                            }
                        }
                    }
                    #endregion
                    #region Requirements
                    foreach (var item in talentWithRequirement)
                    {
                        var talent = item.Key;
                        var jObject = item.Value;

                        if (jObject.RequirementStrings != null)
                        {
                            foreach (var reqItem in jObject.RequirementStrings)
                            {
                                talent.Requirements.Add(new TalentRequirementFreeText(reqItem));
                            }
                        }
                        if (jObject.RequirementAttributs != null)
                        {
                            foreach (var reqItem in jObject.RequirementAttributs)
                            {
                                talent.Requirements.Add(new TalentRequirementAttribut(reqItem.Key, reqItem.Value));
                            }
                        }
                        if (jObject.RequirementNeed != null)
                        {
                            foreach (var reqItem in jObject.RequirementNeed)
                            {
                                var reqTalent = list.Where(x => x.ID == reqItem.Key).First();
                                var reqNeed = reqItem.Value;
                                jObject.RequirementOff.TryGetValue(reqItem.Key, out int reqOff);

                                if (reqTalent == null)
                                {
                                    throw new Exception();
                                }
                                else
                                {
                                    talent.Requirements.Add(new TalentRequirementTalent(reqTalent, reqNeed, reqOff));
                                }
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }

            return list.OrderBy(x => x.Name).ToList();
        }
        public static List<LanguageFamily> LoadLanguageFamily(List<JSONTalentLanguageFamily> jsonFamilyList, List<ITalent> talentList)
        {
            if (jsonFamilyList == null) jsonFamilyList = new List<JSONTalentLanguageFamily>();
            if (talentList == null) talentList = new List<ITalent>();

            var ret = new List<LanguageFamily>();

            foreach (var json_Family in jsonFamilyList)
            {
                var family = new LanguageFamily(json_Family.Name);

                foreach (var item in json_Family.Writings)
                {
                    var talent = TalentHelper.SearchTalentGeneric<TalentWriting>(item.Value, talentList);
                    family.Writings.Add(item.Key, talent);
                }
                foreach (var item in json_Family.Languages)
                {
                    var talent = TalentHelper.SearchTalentGeneric<TalentSpeaking>(item.Value, talentList);
                    family.Languages.Add(item.Key, talent);
                }
                ret.Add(family);
            }

            return ret;
        }

    }
}
