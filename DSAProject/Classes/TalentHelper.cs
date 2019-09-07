using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DSALib;
using DSALib.Classes.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;

namespace DSAProject.Classes
{
    public static class TalentHelper
    {
        #region Variables
        private static List<Guid> talentGuids = new List<Guid>();
        #endregion
        public static bool IsTalentTypeAvaivible(string contentType)
        {
            try
            {
                CreateTalent(contentType);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static ITalent EditTalent(ITalent talent, List<ITalentDeduction> deductions = null, List<ITalentRequirement> requirements = null, AbstractTalentGeneral fatherTalent = null)
        {
            talent.Deductions.Clear();
            foreach (var deduction in deductions)
            {
                talent.Deductions.Add(deduction);
            }
            if(requirements != null || fatherTalent != null)
            {
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                {
                    var abstractTalentGeneral = (AbstractTalentGeneral)talent;
                    abstractTalentGeneral.Requirements = requirements;
                    abstractTalentGeneral.FatherTalent = fatherTalent;
                }
                else
                {
                    throw new Exception("Es wurde Versucht Variablen in einem Talent Typen zu editieren in dem diese nicht vorhanden sind");
                }
            }

            return talent;
        }
        public static ITalent CreateTalent(string contentType, Guid guid, List<CharakterAttribut> probe, string be, string name, string nameExtension)
        {
            ITalent talent = CreateTalent(
                contentType: contentType,
                guid: guid,
                probe: probe);
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Der Name darf nicht null sein");
            }
            else if (talent != null)
            {
                #region AllgemeineTalentWerte
                talent.BE               = be;
                talent.Name             = name;
                talent.NameExtension    = nameExtension;
                #endregion
            }

            return talent;
        }
        private static ITalent CreateTalent(string contentType, Guid guid = new Guid(), List<CharakterAttribut> probe = null)
        {
            ITalent talent = null;

            if (guid == new Guid() || guid == null)
            {
                guid = guid.GenerateNextGuid(talentGuids);
                talentGuids.Add(guid);
            }

            if (contentType == nameof(TalentWeaponless))
            {
                talent = new TalentWeaponless(guid);
            }
            else if (contentType == nameof(TalentClose))
            {
                talent = new TalentClose(guid);
            }
            else if (contentType == nameof(TalentRange))
            {
                talent = new TalentRange(guid);
            }
            else if (contentType == nameof(TalentCrafting))
            {
                talent = new TalentCrafting(guid, probe);
            }
            else if (contentType == nameof(TalentKnowldage))
            {
                talent = new TalentKnowldage(guid, probe);
            }
            else if (contentType == nameof(TalentNature))
            {
                talent = new TalentNature(guid, probe);
            }
            else if (contentType == nameof(TalentPhysical))
            {
                talent = new TalentPhysical(guid, probe);
            }
            else if (contentType == nameof(TalentSocial))
            {
                talent = new TalentSocial(guid, probe);
            }
            else if (contentType == nameof(TalentLanguageBase))
            {
                talent = new TalentLanguageBase(guid);
            }
            else if (contentType == nameof(TalentLanguageFamily))
            {
                talent = new TalentLanguageFamily(guid);
            }
            else
            {
                throw new Exception("Der Angegebene Talent Typ ist unbekannt");
            }
            return talent;
        }
        public static ObservableCollection<ITalent> LoadTalent(List<JSON_Talent> talents, ObservableCollection<ITalent> list)
        {
            Dictionary<ITalent, JSON_Talent> talentwithDedcut = new Dictionary<ITalent, JSON_Talent>();
            Dictionary<AbstractTalentGeneral, JSON_Talent> talentWithRequirement = new Dictionary<AbstractTalentGeneral, JSON_Talent>();
            Dictionary<AbstractTalentGeneral, JSON_Talent> talentWithFather = new Dictionary<AbstractTalentGeneral, JSON_Talent>();

            if (talents != null)
            {
                try
                {
                    #region Talente Erstellen, Dedurctions vorbereiten
                    foreach (var item in talents)
                    {
                        var talent = CreateTalent(item.ContentType, item.ID, item.Probe, item.BE, item.Name, item.NameExtension);

                        if (talent != null)
                        {
                            if ((item.DeductionTalents != null && item.DeductionTalents.Count > 0) || (item.DeductionStrings != null && item.DeductionTalents.Count > 0))
                            {
                                if (item.DeductionTalents.Keys.Count > 0)
                                {
                                    talentwithDedcut.Add(talent, item);
                                }
                            }
                            #region AbstractTalentGeneral
                            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                            {
                                var abstractTalentGeneral = (AbstractTalentGeneral)talent;
                                talentWithRequirement.Add(abstractTalentGeneral, item);

                                if (item.FatherTalent != Guid.Empty)
                                {
                                    talentWithFather.Add(abstractTalentGeneral, item);
                                }
                            }
                            #endregion
                            list.Add(talent);
                            talentGuids.Add(talent.ID);
                        }


                    }
                    #endregion
                    #region Deductions
                    foreach (var item in talentwithDedcut)
                    {
                        if (item.Value.DeductionTalents != null)
                        {
                            foreach (var deduction in item.Value.DeductionTalents)
                            {
                                var items = list.Where(x => x.ID == deduction.Key).ToList();
                                if (items.Count != 1)
                                {
                                    Logger.Log(LogLevel.ErrorLog, "Deuction not found: " + deduction.Key, nameof(Game), nameof(LoadTalent));
                                }
                                else
                                {
                                    item.Key.Deductions.Add(new TalentDeductionTalent(items[0], deduction.Value, item.Key.BaseDeduction));
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
                                    Logger.Log(LogLevel.ErrorLog, "Requriement not found: " + reqItem.Key.ToString(), nameof(Game), nameof(LoadTalent));
                                }
                                else
                                {
                                    talent.Requirements.Add(new TalentRequirementTalent(reqTalent, reqNeed, reqOff));
                                }
                            }
                        }
                        else
                        {
                            //Es exestiert keine Requirement, oder sie ist unbekannt
                        }
                    }
                    #endregion
                    #region Father Talent
                    foreach (var item in talentWithFather)
                    {
                        var talent = item.Key;
                        var jobject = item.Value;
                        var fatherTalent = list.Where(x => x.ID == jobject.FatherTalent).ToList().First();
                        if (fatherTalent == null || !(typeof(AbstractTalentGeneral).IsAssignableFrom(fatherTalent.GetType())))
                        {
                            Logger.Log(LogLevel.ErrorLog, "Das Vater Talent mit der folgenden Id ist fehlerhaft: " + jobject.FatherTalent.ToString(), nameof(Game), nameof(LoadTalent));
                        }
                        else
                        {
                            talent.FatherTalent = (AbstractTalentGeneral)fatherTalent;
                        }

                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.ErrorLog, nameof(Game), nameof(LoadTalent), ex.Message);
                }
            }

            return new ObservableCollection<ITalent>(list.OrderBy(x => x.Name).ToList());
        }
        public static JSON_Talent CreateJSON(ITalent talent, GameType gameType)
        {
            JSON_Talent jsonTalent = null;

            #region TalentType
            var talenttype  = talent.GetType().ToString();
            var lastIndex   = talenttype.LastIndexOf(".");
            talenttype      = talenttype.Substring(lastIndex + 1);
            #endregion

            if (talent.Name != null && talent.Name != string.Empty)
            {
                jsonTalent = new JSON_Talent
                {
                    ID              = talent.ID,
                    BE              = talent.BE,
                    Name            = talent.Name,
                    NameExtension   = talent.NameExtension,
                    ContentType     = talenttype,
                    SaveTime        = DateTime.Now
                };
                foreach (var item in talent.Deductions)
                {
                    if (typeof(TalentDeductionTalent).IsAssignableFrom(item.GetType()))
                    {
                        var deduction = (TalentDeductionTalent)item;
                        if (jsonTalent.DeductionTalents.ContainsKey(deduction.Talent.ID))
                        {
                            jsonTalent.DeductionTalents.Remove(deduction.Talent.ID);
                        }

                        jsonTalent.DeductionTalents.Add(deduction.Talent.ID, deduction.Value);
                    }
                    else if (typeof(TalentDeductionFreeText).IsAssignableFrom(item.GetType()))
                    {
                        var deduction = (TalentDeductionFreeText)item;
                        jsonTalent.DeductionStrings.Add(deduction.Text);
                    }
                }
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                {
                    var abstractTalentGeneral   = (AbstractTalentGeneral)talent;
                    jsonTalent.Probe            = abstractTalentGeneral.Attributs;

                    foreach (var requirement in abstractTalentGeneral.Requirements)
                    {
                        if (requirement.GetType() == typeof(TalentRequirementTalent))
                        {
                            var req = (TalentRequirementTalent)requirement;
                            if (jsonTalent.RequirementNeed == null)
                            {
                                jsonTalent.RequirementNeed = new Dictionary<Guid, int>();
                                jsonTalent.RequirementOff = new Dictionary<Guid, int>();
                            }
                            if (jsonTalent.RequirementOff.ContainsKey(req.Talent.ID))
                            {
                                throw new Exception("Versuch als Anforderung zweimal das gleiche Talent einzufügen");
                            }
                            else
                            {
                                jsonTalent.RequirementNeed.Add(req.Talent.ID, req.ReqNeed);
                                jsonTalent.RequirementOff.Add(req.Talent.ID, req.ReqOff);
                            }
                        }
                        else if (requirement.GetType() == typeof(TalentRequirementAttribut))
                        {
                            var req = (TalentRequirementAttribut)requirement;
                            if (jsonTalent.RequirementAttributs == null) jsonTalent.RequirementAttributs = new Dictionary<CharakterAttribut, int>();
                            jsonTalent.RequirementAttributs.Add(req.Attribut, req.AttributValue);
                        }
                        else if (requirement.GetType() == typeof(TalentRequirementFreeText))
                        {
                            var req = (TalentRequirementFreeText)requirement;
                            if (jsonTalent.RequirementStrings == null) jsonTalent.RequirementStrings = new List<string>();
                            jsonTalent.RequirementStrings.Add(req.FreeText);
                        }
                        else
                        {
                            throw new Exception("Unbekannter Requirement Typ");
                        }
                    }
                    if (abstractTalentGeneral.FatherTalent != null)
                    {
                        jsonTalent.FatherTalent = abstractTalentGeneral.FatherTalent.ID;
                    }
                }
            }
            else
            {
                throw new Exception("Es sind nicht alle nötigen Variablen für das erstellen eines Talent gefüllt");
            }
            return jsonTalent;
        }
    }
}
