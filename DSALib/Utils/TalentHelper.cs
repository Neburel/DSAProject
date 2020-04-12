using System;
using System.Collections.Generic;
using System.Linq;
using DSALib;
using DSALib.Charakter.Talente;
using DSALib.Charakter.Talente.TalentLanguage;
using DSALib.Classes.JSON;
using DSALib.Exceptions;
using DSALib.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Interfaces;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;

namespace DSAProject.Classes
{

    public static class TalentHelper
    {
        #region Variables
        private static List<Guid> talentGuids = new List<Guid>();
        #endregion
        #region Const
        private const string EXCELTITLE = "//Title//";
        #endregion
        public static bool IsTalentTypeAvaivible(string contentType)
        {
            try
            {
                CreateTalent(contentType);
                return true;
            }
            catch(TalentException)
            {
                return false;
            }
        }
        public static Type GetTypeFromString(string contentType)
        {
            return CreateTalent(contentType).GetType();
        }
        #region Creator
        public static ITalent EditTalent(ITalent talent, List<ITalentDeduction> deductions = null, List<ITalentRequirement> requirements = null)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));
            else if (deductions == null) throw new ArgumentNullException(nameof(deductions));

            talent.Deductions.Clear();
            foreach (var deduction in deductions)
            {
                talent.Deductions.Add(deduction);
            }
            if (requirements != null)
            {
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                {
                    var abstractTalentGeneral = (AbstractTalentGeneral)talent;
                    abstractTalentGeneral.Requirements = requirements;
                }
                else
                {
                    throw new TalentException(
                        error: ErrorCode.Error,
                        message: Resources.ErrorTalentUnknownTalentTypeEdit
                        ); 
                }
            }

            return talent;
        }
        public static ITalent CreateTalent(string contentType, List<CharakterAttribut> probe, string be, string name, string nameExtension, Guid talentGuid = new Guid(), int orginalPos = -1)
        {
            ITalent talent = CreateTalent(
                contentType: contentType,
                guid: talentGuid,
                probe: probe,
                orginalPos: orginalPos);
            if (string.IsNullOrEmpty(name))
            {
                throw new TalentException(
                    error: ErrorCode.Error,
                    message: Resources.ErrorNullName);
            }
            else if (talent != null)
            {
                talent.BE = be;
                talent.Name = name;
                talent.NameExtension = nameExtension;
            }

            return talent;
        }
        private static ITalent CreateTalent(string contentType, Guid guid = new Guid(), List<CharakterAttribut> probe = null, int orginalPos = -1)
        {
            ITalent talent = null;
            contentType = contentType.Trim();

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
            else if (contentType == nameof(TalentSpeaking) || contentType == "TalentLanguage")
            {
                //TalentLanguage ist ein Relikt das aber vorhanden sein muss damit man alte Save dateien Laden kann
                talent = new TalentSpeaking(guid);
            }
            else if (contentType == nameof(TalentWriting))
            {
                talent = new TalentWriting(guid);
            }
            else
            {
                throw new TalentException(
                    error: ErrorCode.Error,
                    message: Resources.ErrorUnknownTalentType);
            }
            talent.OrginalPosition = orginalPos;
            return talent;
        }
        public static JSONTalent CreateJSON(ITalent talent)
        {
            if (talent == null) throw new ArgumentNullException(nameof(talent));
            
            JSONTalent jsonTalent;
            #region TalentType
            var talenttype  = talent.GetType().ToString();
            var lastIndex   = talenttype.LastIndexOf(".", StringComparison.CurrentCulture);
            talenttype      = talenttype.Substring(lastIndex + 1);
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
                    OrginalPos = talent.OrginalPosition,
                };
                foreach (var item in talent.Deductions)
                {
                    if (typeof(TalentDeductionTalent).IsAssignableFrom(item.GetType()))
                    {
                        var deduction = (TalentDeductionTalent)item;
                        var existingItem = jsonTalent.DeductionTalentList.Where(x => x.ID == deduction.Talent.ID).FirstOrDefault();
                        if(existingItem != null)
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
        #endregion
        #region Loader
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
                        var talent = CreateTalent(
                            contentType: item.ContentType,
                            talentGuid: item.ID,
                            probe: item.Probe,
                            be: item.BE,
                            name: item.Name,
                            nameExtension: item.NameExtension,
                            orginalPos: item.OrginalPos);

                        if (talent != null)
                        {
                            if (item.DeductionTalentList.Any() || item.DeductionStrings.Any())
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
                            talentGuids.Add(talent.ID);
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
                    var talent = SearchTalentGeneric<TalentWriting>(item.Value, talentList);
                    family.Writings.Add(item.Key, talent);
                }
                foreach (var item in json_Family.Languages)
                {
                    var talent = SearchTalentGeneric<TalentSpeaking>(item.Value, talentList);
                    family.Languages.Add(item.Key, talent);
                }
                ret.Add(family);
            }

            return ret;
        }
        #endregion
        #region Excel
        private enum ExcleRowType
        {
            NoValidTalent = 0,
            ValidTalent = 1,
            Title = 2
        }
        public static List<ITalent> ExcelImport(string importFile, out List<LanguageFamily> familieList)
        {
            familieList = new List<LanguageFamily>();
            var ret = new List<ITalent>();
            var excelTalentDic = new Dictionary<string, List<ExcelTalent>>();
            var talentsWithDeduction = new Dictionary<ITalent, ExcelTalent>();
            var talentWithRequirements = new Dictionary<ITalent, ExcelTalent>();

            #region Import der Exel Datei
            SpreadsheetDocument document = SpreadsheetDocument.Open(importFile, false);
            WorkbookPart wbPart = document.WorkbookPart;
            List<Sheet> sheets = wbPart.Workbook.Descendants<Sheet>().ToList();
        
            foreach (var sheet in sheets)
            {
                var orginalPosition     = 0;
                var contentType         = sheet.Name;
                var currentTitle        = string.Empty;
                var excelTalents        = new List<ExcelTalent>();
                WorksheetPart wsPart    = (WorksheetPart)(wbPart.GetPartById(sheet.Id));
                var rowList             = wsPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().ToList();
                var titleRowHeaders     = new List<string>();
                var titleRow            = rowList[0];
                rowList.RemoveAt(0);    //Titel Leiste Entfernen

                foreach (var cell in titleRow.Descendants<Cell>().ToList())
                {
                    var cellValue = ExcelImportGetCellValue(wbPart, cell);
                    titleRowHeaders.Add(cellValue);
                }
                foreach (var row in rowList)
                {
                    var excelTalent = new ExcelTalent();
                    var celllist    = row.Descendants<Cell>().ToList();
                    var counter     = 0;
                    excelTalent.OrginalPosition = orginalPosition++;

                    foreach (var cell in celllist)
                    {
                        var cellValue = ExcelImportGetCellValue(wbPart, cell);
                        excelTalent.AddValue(titleRowHeaders[counter], cellValue);
                        counter++;
                    }
                    var excelRowType = excelTalent.ExcelRowType();
                    if (excelRowType == ExcleRowType.Title)
                    {
                        var title = excelTalent.Talent.Replace(EXCELTITLE, "");
                        currentTitle = title;
                    }
                    else if (excelRowType == ExcleRowType.ValidTalent)
                    {
                        excelTalent.Title = currentTitle;
                        excelTalents.Add(excelTalent);
                    }
                }
                excelTalentDic.Add(contentType, excelTalents);
            }
            #endregion
            #region Talente Erstellen
            foreach (var talentGroup in excelTalentDic)
            {
                LanguageFamily currentLanguageFamily = null;
                var talentList = talentGroup.Value;
                var pos = 0;
                foreach (var excelTalent in talentList)
                {
                    var name = string.Empty;
                    var nameExtension = string.Empty;
                    if (excelTalent.Talent.Contains("("))
                    {
                        var items = excelTalent.Talent.Split('(');
                        name = items[0];
                        nameExtension = items[1].Split(')').First();
                    }
                    else
                    {
                        name = excelTalent.Talent;
                    }
                    
                    ITalent newTalent = null;
                    if (!string.IsNullOrEmpty(name))
                    {
                        newTalent = SearchTalent(name, ret, GetTypeFromString(talentGroup.Key));
                        if(newTalent == null)
                        {
                            newTalent = CreateTalent(
                                contentType: talentGroup.Key,
                                probe: excelTalent.GetConvertAttribute(),
                                be: excelTalent.BE,
                                name: name,
                                nameExtension: nameExtension,
                                orginalPos: excelTalent.OrginalPosition);
                        }
                    }
                    #region Sprache
                    if (talentGroup.Key == nameof(TalentSpeaking))
                    {
                        TalentSpeaking talentLanguage = null;
                        TalentWriting talentWriting = null;
                        if (newTalent != null) talentLanguage = (TalentSpeaking)newTalent;

                        if (!string.IsNullOrEmpty(excelTalent.Schrift))
                        {
                            talentWriting = (TalentWriting)SearchTalent(excelTalent.Schrift, ret, typeof(TalentWriting));

                            if(talentWriting == null)
                            {
                                talentWriting = (TalentWriting)CreateTalent(
                                    contentType: nameof(TalentWriting),
                                    probe: excelTalent.GetConvertAttribute(),
                                    be: excelTalent.Komplex2,
                                    name: excelTalent.Schrift,
                                    nameExtension: nameExtension);
                            }
                        }

                        if(currentLanguageFamily == null || currentLanguageFamily.Name != excelTalent.Title)
                        {
                            currentLanguageFamily = new LanguageFamily(excelTalent.Title);
                            familieList.Add(currentLanguageFamily);
                            pos = 0;
                        }
                        if(talentLanguage != null)
                        {
                            currentLanguageFamily.Languages.Add(pos, talentLanguage);
                        }
                        if(talentWriting != null)
                        {
                            currentLanguageFamily.Writings.Add(pos, talentWriting);
                        }

                        if(talentWriting != null && !ret.Contains(talentWriting))
                        {
                            ret.Add(talentWriting);
                        }
                    }
                    #endregion
                    if (excelTalent.IsValidVerwanteFertigkeit()) { talentsWithDeduction.Add(newTalent, excelTalent); }
                    if (excelTalent.IsValidAnforderung()) { talentWithRequirements.Add(newTalent, excelTalent); }

                    if(newTalent != null && !ret.Contains(newTalent))
                    {
                        ret.Add(newTalent);
                    }
                    pos++;
                }
            }
            ret = new List<ITalent>(ret.OrderBy(x => x.Name));
            foreach (var talentwithDeduction in talentsWithDeduction)
            {
                if (talentwithDeduction.Key.Name == "Alchimie")
                {

                }

                var deductionTalentStrings = talentwithDeduction.Value.GetSplitDeduction();
                foreach (var deductionString in deductionTalentStrings)
                {
                    var value               = deductionString;
                    var valueint            = -1;
                    var mainReqg            = new Regex("[(][+][0-9]?[0-9][)]");
                    var innerReqg           = new Regex("[0-9]?[0-9]");
                    var stringTalentReqg    = new Regex("[(][A-Za-zäü]{1,}[ ]?[A-Za-zäü]{1,}[)]");

                    if (mainReqg.IsMatch(deductionString))
                    {
                        value = mainReqg.Split(deductionString)[0].Trim();
                        if(!int.TryParse(innerReqg.Match(deductionString).ToString(),  out valueint))
                        {
                            valueint = -1;
                        }
                    }
                    if (valueint == -1) { valueint = talentwithDeduction.Key.BaseDeduction; }

                    ITalentDeduction deduction = null;
                    var deductionTalent = ret.Where(x => x.Name.StartsWith(value, StringComparison.CurrentCulture));
                    if (deductionTalent.Any())
                    {
                        deduction = new TalentDeductionTalent(deductionTalent.First(), valueint, talentwithDeduction.Key.BaseDeduction);
                    }
                    else
                    {
                        var stringTalent    = stringTalentReqg.Split(deductionString)[0].Trim();
                        var innerTalent     = ret.Where(x => x.Name == stringTalent).FirstOrDefault();
                        
                        if(innerTalent != null)
                        {
                            value = mainReqg.Split(deductionString)[0].Trim();

                            if (stringTalentReqg.IsMatch(value))
                            {
                                var description = stringTalentReqg.Match(value).Value;
                                description = description.Replace("(", "").Replace(")", "");
                                deduction = new TalentDeductionTalent(innerTalent, valueint, talentwithDeduction.Key.BaseDeduction, description);
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
                    talentwithDeduction.Key.Deductions.Add(deduction);

                }
            }
            foreach (var talentWithRequirement in talentWithRequirements)
            {
                var splitRequirement = talentWithRequirement.Value.GetSplitRequirement();
                foreach (var requirementString in splitRequirement)
                {
                    ITalentRequirement requirement;
                    var talent = (AbstractTalentGeneral)talentWithRequirement.Key;
                    var value = requirementString;
                    var valueStart = -1;
                    var valueEnd = -1;
                    var reqTalent = ret.Where(x => x.Name.StartsWith(value, StringComparison.CurrentCulture));

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
                        requirement = new TalentRequirementTalent(reqTalent.First(), valueEnd, valueStart);
                    }
                    else if (reqTalent.Any() && valueEnd != -1)
                    {
                        var trueReqTalent = reqTalent.First();
                        requirement = new TalentRequirementTalent(reqTalent.First(), valueEnd);
                    }
                    else
                    {
                        requirement = new TalentRequirementFreeText(requirementString);
                    }

                    talent.Requirements.Add(requirement);
                }
            }
            #endregion
            return ret;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Keine allgemeinen Ausnahmetypen abfangen", Justification = "<Ausstehend>")]
        private static string ExcelImportGetCellValue(WorkbookPart wbPart, Cell cell)
        {
            var strintTableValue = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            var cellValue = cell.InnerText;

            if (!string.IsNullOrEmpty(cellValue))
            {
                //Kontrolle nötig, Beabsichtigter Effekt?
                try
                {
                    cellValue = strintTableValue.SharedStringTable.ElementAt(int.Parse(cellValue, Helper.CultureInfo)).InnerText;
                }
                catch (Exception)
                {

                }
            }
            return cellValue;
        }

        private class ExcelTalent
        {
            private bool komplex1Found = false;
            
            public int OrginalPosition { get; set; }
            public string Talent { get; set; }
            public string Title { get; set; }
            public string Probe { get; set; }
            public string TaW { get; set; }
            public string BE { get; set; }
            public string Billiger { get; set; }
            public string Spezialisierung { get; set; }
            public string Waffenmeister { get; set; }
            public string Ableiten { get; set; }
            public string Anforderungen { get; set; }
            public string VerwandteFertigkeiten { get; set; }

            public string Sprache { get; set; }
            public string Schrift { get; set; }
            public string Komplex1
            {
                get => BE;
                set => BE = value;
            }
            public string Komplex2 { get; set; }


            public List<CharakterAttribut> GetConvertAttribute()
            {
                List<CharakterAttribut> ret = null;
                if (!string.IsNullOrEmpty(Probe))
                {
                    ret = new List<CharakterAttribut>();
                    var probes = Probe.Split('/');
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
            public ExcleRowType ExcelRowType()
            {
                if (string.IsNullOrEmpty(Talent) && string.IsNullOrEmpty(Schrift)) return ExcleRowType.NoValidTalent;
                else if (Talent.StartsWith(EXCELTITLE, StringComparison.CurrentCulture)) return ExcleRowType.Title;

                return ExcleRowType.ValidTalent;
            }
            public void AddValue(string titleValue, string value)
            {
                titleValue = titleValue.Replace(" ", "");

                if (titleValue == nameof(Talent)) Talent = value;
                else if (titleValue == nameof(BE)) BE       = value;
                else if (titleValue == nameof(Probe)) Probe = value;
                else if (titleValue == nameof(Billiger)) Billiger = value;
                else if (titleValue == nameof(Spezialisierung)) Spezialisierung = value;
                else if (titleValue == nameof(Waffenmeister)) Waffenmeister = value;
                else if (titleValue == nameof(TaW)) TaW = value;
                else if (titleValue == nameof(Anforderungen)) Anforderungen = value;
                else if (titleValue.StartsWith(nameof(VerwandteFertigkeiten), StringComparison.CurrentCulture)) VerwandteFertigkeiten = value;
                else if (titleValue.StartsWith(nameof(Ableiten), StringComparison.CurrentCulture)) Ableiten = value;
                else if (titleValue == nameof(Sprache))
                {
                    value = RemoveLanguageSymbols(value);
                    Talent = value;
                    Sprache = value;
                }
                else if (titleValue == nameof(Schrift))
                {
                    Schrift = RemoveLanguageSymbols(value).Trim();
                }
                else if (titleValue == "Komplex.")
                {
                    if (!komplex1Found) Komplex1 = value;
                    else Komplex2 = value;
                    komplex1Found = true;
                }
                //Sprache M.	Komplex.TaW Schrift Komplex.TaW

            }
            private static bool IsValidString(string value)
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

            public bool IsValidVerwanteFertigkeit()
            {
                if (IsValidString(VerwandteFertigkeiten) || IsValidString(Ableiten))
                {
                    return true;
                }
                return false;
            }
            public bool IsValidAnforderung()
            {
                return (IsValidString(Anforderungen));
            }
            private static List<string> SplitString(string valueString, List<string> retList)
            {
                if (retList == null) retList = new List<string>();
                if (!string.IsNullOrEmpty(valueString))
                {
                    retList.AddRange(valueString.Split(','));
                }
                return retList;
            }
            public List<string> GetSplitDeduction()
            {
                var list = new List<string>();
                var retList = new List<string>();

                list = SplitString(VerwandteFertigkeiten, list);
                list = SplitString(Ableiten, list);

                foreach (var item in list)
                {
                    retList.Add(item.Trim());
                }
                return retList;
            }
            public List<string> GetSplitRequirement()
            {
                return SplitString(Anforderungen, new List<string>());
            }
            private static string RemoveLanguageSymbols(string value)
            {
                value = value.Split('*')[0];
                return value;
            }

            public override string ToString()
            {
                return Talent;
            }
        }
        #endregion
        #region Hilfsmethoden
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
                LogStrings.LogString(LogLevel.ErrorLog, "Das Talent mit der GUID " + talentGuid + " konnte nicht gefunden werden. Erwarteter Talent Typ: " + typeof(T));
            }
            else
            {
                LogStrings.LogString(LogLevel.ErrorLog, "Das Talent mit der GUID " + talentGuid + " konnte nicht mit dem Angegebenen Typen gefunden werden. Erwarteter Talent Typ: " + typeof(T) + " eigentlicher Typ: " + talent.GetType());

            }

            return default(T);
        }
        #endregion
    }
}
