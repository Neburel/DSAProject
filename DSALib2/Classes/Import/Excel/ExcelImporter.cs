using DSALib2.Charakter.Talente;
using DSALib2.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentDeductions;
using DSALib2.Classes.Charakter.Talente.TalentFighting;
using DSALib2.Classes.Charakter.Talente.TalentGeneral;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente.TalentRequirement;
using DSALib2.Classes.Tools;
using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace DSALib2.Classes.Import.Excel
{
    public class ExcelImporter
    {
        #region Const
        private const string EXCELTITLE = "//Title//";
        #endregion
        private enum ExcleRowType
        {
            NoValidTalent = 0,
            ValidTalent = 1,
            ValidTalentSprache = 3,
            ValidTalentSchrift = 4,
            Title = 2
        }

        public ExcelImportResult ExcelImport(List<TalentExcelImport> close, List<TalentExcelImport> range, List<TalentExcelImport> weaponless,
             List<TalentExcelImport> crafting, List<TalentExcelImport> knowldage, List<TalentExcelImport> nature, List<TalentExcelImport> physical, List<TalentExcelImport> social,
              List<TalentExcelImport> language)
        {
            var result = new ExcelImportResult();
            List<TalentExcelImport> writing = new List<TalentExcelImport>();
            List<TalentExcelImport> speaking = new List<TalentExcelImport>();

            var familyImporter = GetLanguageItems(language, ref writing, ref speaking);

            var talentDic = ExcelImport<TalentClose>(close);
            talentDic = AddDicRange(talentDic, ExcelImport<TalentRange>(range));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentWeaponless>(weaponless));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentCrafting>(crafting));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentKnowldage>(knowldage));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentNature>(nature));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentPhysical>(physical));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentSocial>(social));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentWriting>(writing));
            talentDic = AddDicRange(talentDic, ExcelImport<TalentSpeaking>(speaking));

            talentDic = ExcelImportAddDeductions(talentDic);
            talentDic = ExelImportAddRequirements(talentDic);

            var talentList = new List<ITalent>();
            foreach(var item in talentDic)
            {
                talentList.Add(item.Value);               
            }
            result.TalentList = talentList;
            result.LanguageFamilyList = ExelImportFamily(familyImporter, talentDic);
            result.OldNameDictionary = GetDictionaryTalentWithNewNames(talentDic);
            return result;
        }

        private List<FamilyImporter> GetLanguageItems(List<TalentExcelImport> importList, 
            ref List<TalentExcelImport> writingList, ref List<TalentExcelImport> speakingList)
        {
            var result = new List<FamilyImporter>();
            writingList = new List<TalentExcelImport>();
            speakingList = new List<TalentExcelImport>();
            FamilyImporter currentFamilyImport = null;
            var pos = 0;
            foreach (var item in importList)
            {
                var rowType = TalentExcelImportType(item);
                if (rowType == ExcleRowType.Title)
                {
                    currentFamilyImport = new FamilyImporter();
                    currentFamilyImport.Family = new LanguageFamily(RemoveTitel(item.Sprache));
                    pos = 0;
                    result.Add(currentFamilyImport);
                }
                else if (rowType == ExcleRowType.ValidTalent)
                {
                    if(!string.IsNullOrEmpty(item.Sprache))
                    {
                        item.OrginalPosition = pos;
                        item.Talent = item.Sprache;

                        speakingList.Add(item);
                        currentFamilyImport.Speaking.Add(item);
                    }
                    if (!string.IsNullOrEmpty(item.Schrift))
                    {
                        var itemWriting = new TalentExcelImport();
                        itemWriting.BE = item.Komplex2;
                        itemWriting.Talent = item.Schrift;
                        itemWriting.OrginalPosition = pos;
 ;
                        writingList.Add(itemWriting);
                        currentFamilyImport.Writing.Add(itemWriting);
                    }
                    pos++;
                }
            }
            return result;
        }
        private Dictionary<TalentExcelImport, ITalent> ExcelImport<T>(List<TalentExcelImport> importList) where T : ITalent
        {
            var talentDic = new Dictionary<TalentExcelImport, ITalent>();

            foreach (var item in importList)
            {
                var rowType = TalentExcelImportType(item);
                if (rowType == ExcleRowType.ValidTalent)
                {
                    string name;
                    string nameExtension;
                    List<CharakterAttribut> probe = GetAttributProbe(item.Probe);

                    GetName(item.Talent, out name, out nameExtension);
                    //Bereits ein Talent mit diesem namen vorhanden?
                    var existingItem = talentDic.Where(x => x.Value.Name == name).FirstOrDefault().Value;
                    if (existingItem == null)
                    {
                        var talent = TalentCreator.CreateTalent<T>(
                            name: name,
                            nameExtension: nameExtension,
                            orginalPos: item.OrginalPosition,
                            be: item.BE,
                            probe: probe);
                        talentDic.Add(item, talent);
                    }
                    else
                    {
                        Debug.WriteLine(existingItem);
                    }
                }
            }

            return talentDic;
        }
        private Dictionary<TalentExcelImport, ITalent> ExcelImportAddDeductions(Dictionary<TalentExcelImport, ITalent> talentDic)
        {
            foreach (var talent in talentDic)
            {
                if (IsValidDeduction(talent.Key.VerwandteFertigkeiten, talent.Key.Ableiten))
                {
                    var deductionTalentStrings = GetSplitDeduction(talent.Key.VerwandteFertigkeiten, talent.Key.Ableiten);
                    foreach (var deductionString in deductionTalentStrings)
                    {
                        var value = deductionString;
                        var valueint = -1;
                        var mainReqg = new Regex("[(][+][0-9]?[0-9][)]");
                        var innerReqg = new Regex("[0-9]?[0-9]");
                        var stringTalentReqg = new Regex("[(][A-Za-zäüß]{1,}[ ]?[A-Za-zäüß]{1,}[)]");

                        if (mainReqg.IsMatch(deductionString))
                        {
                            value = mainReqg.Split(deductionString)[0].Trim();
                            if (!int.TryParse(innerReqg.Match(deductionString).ToString(), out valueint))
                            {
                                valueint = -1;
                            }
                        }
                        if (valueint == -1) { valueint = talent.Value.BaseDeduction; }

                        ITalentDeduction deduction = null;
                        var deductionTalent = talentDic.Where(x => x.Value.Name.StartsWith(value, StringComparison.CurrentCulture));
                        if (deductionTalent.Any())
                        {
                            deduction = new TalentDeductionTalent(deductionTalent.First().Value, valueint, talent.Value.BaseDeduction);
                        }
                        else
                        {
                            var stringTalent = stringTalentReqg.Split(deductionString)[0].Trim();
                            var innerTalent = talentDic.Where(x => x.Value.Name.StartsWith(value, StringComparison.CurrentCulture));

                            if (innerTalent.Count() > 0)
                            {
                                value = mainReqg.Split(deductionString)[0].Trim();

                                if (stringTalentReqg.IsMatch(value))
                                {
                                    var description = stringTalentReqg.Match(value).Value;
                                    description = description.Replace("(", "").Replace(")", "");
                                    deduction = new TalentDeductionTalent(innerTalent.First().Value, valueint, talent.Value.BaseDeduction, description);
                                }
                                else
                                {
                                    deduction = new TalentDeductionFreeText(deductionString);
                                }
                            }
                            else
                            {
                                deduction = new TalentDeductionFreeText(deductionString);
                            }
                        }
                        talent.Value.Deductions.Add(deduction);
                    }
                }
            }

            return talentDic;
        }
        private Dictionary<TalentExcelImport, ITalent> ExelImportAddRequirements(Dictionary<TalentExcelImport, ITalent> talentDic)
        {
            foreach (var talentItem in talentDic)
            {
                if (IsValidRequirement(talentItem.Key.Anforderungen))
                {
                    var splitRequirement = GetSplitRequirement(talentItem.Key.Anforderungen);
                    foreach (var requirementString in splitRequirement)
                    {
                        ITalentRequirement requirement;
                        var talent = (AbstractTalentGeneral)talentItem.Value;
                        var value = requirementString;
                        var valueStart = -1;
                        var valueEnd = -1;
                        var reqTalent = talentDic.Where(x => x.Value.Name.StartsWith(value, StringComparison.CurrentCulture));

                        var startReqg = new Regex("[0-9]?[0-9][+][:]");
                        var endReqg = new Regex("[ ][0-9]?[0-9]");

                        if (endReqg.IsMatch(value))
                        {
                            if (startReqg.IsMatch(value))
                            {
                                var innerStartReq = new Regex("[0-9]?[0-9]");
                                var startvalue = startReqg.Match(value).ToString();
                                var truestartValue = innerStartReq.Match(startvalue).ToString();
                                valueStart = Int32.Parse(truestartValue, Helper.CultureInfo);
                                value = startReqg.Split(value)[1];
                            }

                            var startSplit = startReqg.Split(value);
                            valueEnd = Int32.Parse(endReqg.Match(value).ToString(), Helper.CultureInfo);
                            value = endReqg.Split(value)[0].Trim();
                        }

                        requirement = new TalentRequirementFreeText(requirementString);

                        if (reqTalent.Any() && valueEnd != -1 && valueStart != -1)
                        {
                            requirement = new TalentRequirementTalent(reqTalent.First().Value, valueEnd, valueStart);
                        }
                        else if (reqTalent.Any() && valueEnd != -1)
                        {
                            var trueReqTalent = reqTalent.First();
                            requirement = new TalentRequirementTalent(reqTalent.First().Value, valueEnd);
                        }
                        else
                        {
                            requirement = new TalentRequirementFreeText(requirementString);
                        }

                        talent.Requirements.Add(requirement);
                    }
                }
            }
            return talentDic;
        }
        private List<LanguageFamily> ExelImportFamily(List<FamilyImporter> helpList, Dictionary<TalentExcelImport, ITalent> importlist)
        {
            var languageList = new List<LanguageFamily>();

            foreach(var item in helpList)
            {
                var family = item.Family;
                foreach(var writingItem in item.Writing)
                {
                    var talent = (TalentWriting)importlist.Where(x => x.Value.Name == writingItem.Talent && typeof(TalentWriting).IsAssignableFrom(x.Value.GetType())).FirstOrDefault().Value;
                    family.Writings.Add(writingItem.OrginalPosition, talent);
                }
                foreach(var speakingItem in item.Speaking)
                {
                    var talent = (TalentSpeaking)importlist.Where(x => x.Value.Name == speakingItem.Talent && typeof(TalentSpeaking).IsAssignableFrom(x.Value.GetType())).FirstOrDefault().Value;
                    family.Languages.Add(speakingItem.OrginalPosition, talent);
                }
                languageList.Add(family);
            }
            return languageList;
        }
        private Dictionary<ITalent, string> GetDictionaryTalentWithNewNames(Dictionary<TalentExcelImport, ITalent> dictionary)
        {
            Dictionary<ITalent, string> ret = new Dictionary<ITalent, string>();

            foreach(var keyValue in dictionary)
            {
                if(!string.IsNullOrEmpty(keyValue.Key.AlterName) && keyValue.Key.AlterName != keyValue.Key.Talent)
                {
                    ret.Add(keyValue.Value, keyValue.Key.AlterName);
                }
            }
            return ret;
        }

        #region Hilsmethoden
        private void GetName(string valueString, out string name, out string nameExtension)
        {
            name            = string.Empty;
            nameExtension   = string.Empty;
            if (!string.IsNullOrEmpty(valueString) && valueString.Contains("("))
            {
                var items = valueString.Split('(');
                name = items[0];
                nameExtension = items[1].Split(')').First();
            }
            else
            {
                name = valueString;
            }
            name = name.Trim();
            nameExtension = nameExtension.Trim();
        }
        private bool IsValidString(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value != "-")
                {
                    return true;
                };
            }
            return false;
        }
        private bool IsValidDeduction(string valueString, string valueString2 = null)
        {
            if (IsValidString(valueString) ||IsValidString(valueString2))
            {
                return true;
            }
            return false;
        }
        private bool IsValidRequirement(string valueString)
        {
            if(IsValidString(valueString) && valueString != "-")
            {
                return true;
            }
            return false;
        }
        private string RemoveTitel(string valueString)
        {
            return valueString.Replace(EXCELTITLE, "");
        }
        private List<string> SplitString(string valueString, List<string> retList)
        {
            if (retList == null) retList = new List<string>();
            if (!string.IsNullOrEmpty(valueString))
            {
                retList.AddRange(valueString.Split(','));
            }
            return retList;
        }
        private List<string> GetSplitDeduction(string valueString, string valueString2 = null)
        {
            var list = new List<string>();
            var retList = new List<string>();

            list = SplitString(valueString, list);
            list = SplitString(valueString2, list);

            foreach (var item in list)
            {
                retList.Add(item.Trim());
            }
            return retList;
        }
        private List<string> GetSplitRequirement(string valueString)
        {
            return SplitString(valueString, new List<string>());
        }
        private List<CharakterAttribut> GetAttributProbe(string valueString)
        {
            List<CharakterAttribut> ret = null;
            if (!string.IsNullOrEmpty(valueString))
            {
                ret = new List<CharakterAttribut>();
                var probes = valueString.Split('/');
                foreach (var item in probes)
                {
                    foreach (var attribute in Enum.GetValues(typeof(CharakterAttribut)))
                    {
                        if (item == Helper.GetShort((CharakterAttribut)attribute))
                        {
                            ret.Add((CharakterAttribut)attribute);
                        }
                    }
                }
            }
            return ret;
        }
        private ExcleRowType TalentExcelImportType(TalentExcelImport talent)
        {
            if (talent.Talent != null && talent.Talent.StartsWith(EXCELTITLE, StringComparison.CurrentCulture)) return ExcleRowType.Title;
            else if (talent.Sprache != null && talent.Sprache.StartsWith(EXCELTITLE, StringComparison.CurrentCulture)) return ExcleRowType.Title;
            else if (string.IsNullOrEmpty(talent.Talent) && string.IsNullOrEmpty(talent.Schrift) && string.IsNullOrEmpty(talent.Sprache)) return ExcleRowType.NoValidTalent;
           
            return ExcleRowType.ValidTalent;
        }

        private Dictionary<TalentExcelImport, ITalent> AddDicRange(Dictionary<TalentExcelImport, ITalent> orginal, Dictionary<TalentExcelImport, ITalent> newDic)
        {
            foreach(var item in newDic)
            {
                orginal.Add(item.Key, item.Value);
            }
            return orginal;
        }
        #endregion
        
    }
}
