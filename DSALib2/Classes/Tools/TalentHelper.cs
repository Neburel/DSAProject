using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentDeductions;
using DSALib2.Classes.Charakter.Talente.TalentRequirement;
using DSALib2.Classes.JSONSave;
using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Tools
{
    public static class TalentHelper
    {
        public static ITalent SearchTalent(Guid talentGuid, List<ITalent> talentList)
        {
            return talentList.Where(x => x.ID == talentGuid).FirstOrDefault();
        }
        public static ITalent SearchTalent(string name, List<ITalent> talentList, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var innerTalent = talentList.Where(x => x.Name == name).FirstOrDefault();
            if (innerTalent != null && type.IsAssignableFrom(innerTalent.GetType()))
            {
                return innerTalent;
            }
            return null;
        }
        public static T SearchTalentGeneric<T>(Guid talentGuid, List<ITalent> talentList)
        {
            var talent = SearchTalent(talentGuid, talentList);
            if (talent != null && typeof(T).IsAssignableFrom(talent.GetType()))
            {
                return (T)talent;
            }
            if (talent == null)
            {
                throw new ArgumentNullException("Das Talent mit der GUID " + talentGuid + " konnte nicht gefunden werden. Erwarteter Talent Typ: " + typeof(T));
            }
            else
            {
                throw new ArgumentNullException("Das Talent mit der GUID " + talentGuid + " konnte nicht mit dem Angegebenen Typen gefunden werden. Erwarteter Talent Typ: " + typeof(T) + " eigentlicher Typ: " + talent.GetType());
            }
        }


        public static JSONTalent CreateJSON(ITalent talent)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));

            JSONTalent jsonTalent;
            #region TalentType
            var talenttype = talent.GetType().ToString();
            var lastIndex = talenttype.LastIndexOf(".", StringComparison.CurrentCulture);
            talenttype = talenttype.Substring(lastIndex + 1);
            #endregion

            if (!string.IsNullOrEmpty(talent.Name))
            {
                jsonTalent = new JSONTalent
                {
                    ID = talent.ID,
                    BE = talent.BE,
                    Name = talent.Name,
                    NameExtension = talent.NameExtension,
                    ContentType = talenttype,
                    SaveTime = DateTime.Now,
                    OrginalPos = talent.OrginalPosition
                };
                foreach (var item in talent.Deductions)
                {
                    if (typeof(TalentDeductionTalent).IsAssignableFrom(item.GetType()))
                    {
                        if (jsonTalent.DeductionTalentList == null) jsonTalent.DeductionTalentList = new List<JSONTalentDeduction>();
                        var deduction = (TalentDeductionTalent)item;
                        var existingItem = jsonTalent.DeductionTalentList.Where(x => x.ID == deduction.Talent.ID).FirstOrDefault();
                        if (existingItem != null)
                        {
                            jsonTalent.DeductionTalentList.Remove(existingItem);
                        }

                        jsonTalent.DeductionTalentList.Add(new JSONTalentDeduction
                        {
                            ID = deduction.Talent.ID,
                            Value = deduction.Value,
                            Description = deduction.Description
                        });
                    }
                    else if (typeof(TalentDeductionFreeText).IsAssignableFrom(item.GetType()))
                    {
                        if (jsonTalent.DeductionStrings == null) jsonTalent.DeductionStrings = new List<string>();

                        var deduction = (TalentDeductionFreeText)item;
                        jsonTalent.DeductionStrings.Add(deduction.Text);
                    }
                }
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                {
                    var abstractTalentGeneral = (AbstractTalentGeneral)talent;
                    jsonTalent.Probe = abstractTalentGeneral.Attributs;

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
                                throw new Exception(Resources.ErrorTalentDobbleRequirement);
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
                            throw new Exception(Resources.ErrorTalentUnknwonRequirement);
                        }
                    }
                }
            }
            else
            {
                throw new Exception(Resources.ErrorTalentMissingVariables);
            }
            return jsonTalent;
        }
    }
}
