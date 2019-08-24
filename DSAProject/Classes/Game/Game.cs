using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Interfaces;
using DSAProject.Classes.JSON;
using DSAProject.util;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DSAProject.Classes.Game
{
    /*
        Heldenbrief ->Helm(Spartahelm)
        Waffentalente(Kampftalente) ->Gekreuzte Schwerter, Bogen
        Allgemeinte Talente ->Hammer und Sichel
        Sprachen(Schriftrolle)
        Gaben(Göttersymbolik) (Altar), Gefaltete Hände

        Körperliche Talente(Arm)
        Natur(Baum)
        Wissstalente(Buch)
        */
    public enum GameType
    {
        DSA,
        PNP
    }

    public static class Game 
    {
        public static event EventHandler StartPage;
        public static event EventHandler CharakterChanged;
        #region TalentContent
        public static string Talente_BaseFolder { get => "Talente"; }
        public static string TalentContent_Weaponless { get => "Waffenlose Kampftechnik"; }
        public static string TalentContent_Close { get => "Bewaffnete Kampftechnik"; }
        public static string TalentContent_Range { get => "Fernkampftechnik"; }
        public static string TalentContent_Physical { get => "Körperliches Talent"; }
        public static string TalentContent_Social { get => "Gesellschaftliches Talent"; }
        public static string TalentContent_Nature { get => "Natur Talent"; }
        public static string TalentContent_Knwoldage { get => "Wissens Talent"; }
        public static string TalentContent_Crafting { get => "Handwerkstalent"; }
        public static string TalentContent_LanguageFamily { get => "Sprachfamilie"; }
        public static string TalentContent_Language { get => "Sprache"; }
        #endregion
        #region Variables
        private static string talentSaveFile                = "Talente.json";
        private static ICharakter charakter;
        private static JSON_TalentSaveFile jSON_talentLocal = new JSON_TalentSaveFile();
        private static List<Guid> talentGuids               = new List<Guid>();
        #endregion
        #region Properties
        public static ICharakter Charakter
        {
            get
            {
                if(charakter != null)
                {
                    return charakter;
                } 
                else
                {
                    charakter = new CharakterDSA();
                    return charakter;
                }
            }
            set
            {
                charakter = value;
                CharakterChanged?.Invoke(null, null);
            }
        } 
        public static string CharakterSaveFolder { get; } = Path.Combine("Save", "Data");
        public static string CharakterMetaFolder{ get; } = Path.Combine("Save", "Meta");
        public static string CurrentYearDSA { get; } = "? nach Bosporos Fall";
        public static string CurrentYearPNP { get; } = "3135";
        public static ObservableCollection<ITalent> TalentePNP { get; private set; }     = new ObservableCollection<ITalent>();
        public static ObservableCollection<ITalent> TalenteDSA { get; private set; }     = new ObservableCollection<ITalent>();
        #endregion
        #region Funktion
        public static void GoStartPage()
        {
            StartPage?.Invoke(null, null);
        }
        public static void SaveTalent(ITalent talent, GameType gameType, out Error error)
        {   
            error                           = null;
            #region Talenttype
            List<JSON_Talent> jTalentList               = null;
            ObservableCollection<ITalent> talentList    = null;
            var talenttype                              = talent.GetType().ToString();
            var lastIndex                               = talenttype.LastIndexOf(".");
            talenttype                                  = talenttype.Substring(lastIndex + 1);
            #endregion
            #region GameType
            if (gameType == GameType.DSA)
            {
                if (jSON_talentLocal.Talente_DSA == null) jSON_talentLocal.Talente_DSA = new List<JSON_Talent>();
                jTalentList = jSON_talentLocal.Talente_DSA;
                talentList = TalenteDSA;
            }
            else if (gameType == GameType.PNP)
            {
                if (jSON_talentLocal.Talente_PNP == null) jSON_talentLocal.Talente_PNP = new List<JSON_Talent>();
                jTalentList = jSON_talentLocal.Talente_PNP;
                talentList = TalentePNP;
            }
            else
            {
                throw new Exception();
            }
            #endregion
            //Alle nötigen Allegemeinen Variablen Gefüllt
            if (talent.Name != null && talent.Name != string.Empty)
            {
                var jsonTalent = new JSON_Talent
                {
                    ID              = talent.ID,
                    BE              = talent.BE,
                    Name            = talent.Name,
                    NameExtension   = talent.NameExtension,
                    ContentType     = talenttype,
                    SaveTime        = DateTime.Now
                };
                foreach(var item in talent.Deductions)
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
                #region AbstractTalentGeneral
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
                {
                    var abstractTalentGeneral   = (AbstractTalentGeneral)talent;
                    jsonTalent.Probe            = abstractTalentGeneral.Attributs;

                    foreach (var requirement in abstractTalentGeneral.Requirements)
                    {
                        if(requirement.GetType() == typeof(TalentRequirementTalent))
                        {
                            var req = (TalentRequirementTalent)requirement;
                            if(jsonTalent.RequirementNeed == null)
                            {
                                jsonTalent.RequirementNeed = new Dictionary<Guid, int>();
                                jsonTalent.RequirementOff = new Dictionary<Guid, int>();
                            }
                            if (jsonTalent.RequirementOff.ContainsKey(req.Talent.ID))
                            {
                                error = new Error
                                {
                                    ErrorCode = ErrorCode.InvalidValue,
                                    Message = "Versuch als Anforderung zweimal das gleiche Talent einzufügen"
                                };

                                break;
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
                            if(jsonTalent.RequirementAttributs == null) jsonTalent.RequirementAttributs = new Dictionary<CharakterAttribut, int>();
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
                            error = new Error
                            {
                                ErrorCode = ErrorCode.InvalidValue,
                                Message = "Unbekannter Requirement Typ"
                            };
                            break;
                        }
                    }
                    if(abstractTalentGeneral.FatherTalent != null)
                    {
                        jsonTalent.FatherTalent = abstractTalentGeneral.FatherTalent.ID;
                    }
                }
                #endregion
                if (error == null)
                {
                    #region Doppelte Elemente Entfernen
                    var jdoppledID = jTalentList.Where(x => x.ID == talent.ID).ToList();
                    if (jdoppledID.Count > 0)
                    {
                        jTalentList.Remove(jdoppledID[0]);
                    }
                    var tdoppledID = talentList.Where(x => x.ID == talent.ID).ToList();
                    if (tdoppledID.Count > 0)
                    {
                        tdoppledID.Remove(tdoppledID[0]);
                    }
                    #endregion
                    #region Speichern
                    jTalentList.Add(jsonTalent);

                    if (!talentList.Contains(talent))
                    {
                        talentList.Add(talent);
                    }
                    if (talentGuids.Contains(talent.ID))
                    {
                        talentGuids.Remove(talent.ID);
                    }
                    talentGuids.Add(talent.ID);


                    FileManagment.WriteToFile(jSON_talentLocal.JSONContent, talentSaveFile, Windows.Storage.CreationCollisionOption.ReplaceExisting, out error);
                    #endregion
                }
            } 
            else
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.InvalidValue,
                    Message = "Dem Talent muss ein Name gegeben werden"
                };
            }
        }
        public static void LoadTalente()
        {
            var jString = string.Empty;
            //Locale Talente
            jString = FileManagment.LoadTextFile(talentSaveFile, out Error error);
            if(error == null)
            {
                jSON_talentLocal = JSON_TalentSaveFile.DeSerializeJson(jString, out string serror);
                
                if(serror == null)
                {
                    LoadTalent(jSON_talentLocal.Talente_DSA, TalenteDSA);
                    LoadTalent(jSON_talentLocal.Talente_PNP, TalentePNP);
                }

            }

            jString = FileManagment.LoadTextAssestFile(talentSaveFile, out error);
            if (error == null)
            {
                var jSON_talentAssests = JSON_TalentSaveFile.DeSerializeJson(jString, out string serror);

                if (serror == null)
                {
                    LoadTalent(jSON_talentAssests.Talente_DSA, TalenteDSA);
                    LoadTalent(jSON_talentAssests.Talente_PNP, TalentePNP);
                }
            }
        }
        private static ObservableCollection<ITalent> LoadTalent(List<JSON_Talent> talents, ObservableCollection<ITalent> list)
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
                        ITalent talent = null;
                        if (item.ContentType == nameof(TalentWeaponless))
                        {
                            talent = new TalentWeaponless(item.ID);
                        } else if (item.ContentType == nameof(TalentClose))
                        {
                            talent = new TalentClose(item.ID);
                        } else if (item.ContentType == nameof(TalentRange))
                        {
                            talent = new TalentRange(item.ID);
                        } else if (item.ContentType == nameof(TalentCrafting))
                        {
                            talent = new TalentCrafting(item.ID, item.Probe);
                        } else if (item.ContentType == nameof(TalentKnowldage))
                        {
                            talent = new TalentKnowldage(item.ID, item.Probe);
                        } else if (item.ContentType == nameof(TalentNature))
                        {
                            talent = new TalentNature(item.ID, item.Probe);
                        } else if (item.ContentType == nameof(TalentPhysical))
                        {
                            talent = new TalentPhysical(item.ID, item.Probe);
                        } else if (item.ContentType == nameof(TalentSocial))
                        {
                            talent = new TalentSocial(item.ID, item.Probe);
                        } else if (item.ContentType == nameof(TalentLanguageBase))
                        {
                            talent = new TalentLanguageBase(item.ID);
                        } else if (item.ContentType == nameof(TalentLanguageFamily))
                        {
                            talent = new TalentLanguageFamily(item.ID);
                        } else
                        {
                            Logger.Log(LogLevel.ErrorLog, nameof(Game), nameof(LoadTalent), "Der Angegebene Talent Typ ist unbekannt");
                        }
                        if (talent != null)
                        {
                            #region AllgemeineTalentWerte
                            talent.BE = item.BE;
                            talent.Name = item.Name;
                            talent.NameExtension = item.NameExtension;
                            #endregion

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

                                if(item.FatherTalent != Guid.Empty)
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
                            foreach(var deduction in item.Value.DeductionTalents)
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
                            foreach(var deduction in item.Value.DeductionStrings)
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
                                var reqTalent   = list.Where(x => x.ID == reqItem.Key).First();
                                var reqNeed     = reqItem.Value;
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
                    foreach(var item in talentWithFather)
                    {
                        var talent = item.Key;
                        var jobject = item.Value;
                        var fatherTalent = list.Where(x => x.ID == jobject.FatherTalent).ToList().First();
                        if(fatherTalent == null || !(typeof(AbstractTalentGeneral).IsAssignableFrom(fatherTalent.GetType())))
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
 
        public static Guid GeneretNextTalentGUID()
        {
            var guid = Guid.NewGuid();

            while (talentGuids.Contains(guid))
            {
                guid = Guid.NewGuid();
            }
            return guid;
        }
        public static Guid GenerateNextCharakterGUID()
        {
            var guid            = Guid.NewGuid();
            var files           = FileManagment.GetFilesDictionary(CharakterMetaFolder, out Error error);
            var list            = new List<Guid>();

            foreach (var file in files)
            {
                var stringguid = System.IO.Path.ChangeExtension(file, null);

                if(Guid.TryParse(stringguid, out Guid result))
                {
                    list.Add(Guid.Parse(stringguid));
                }
            }

            while (list.Contains(guid))
            {
                guid = new Guid();
            }

            return guid;
        }
        
        public static ObservableCollection<ITalent> GetTalentForCurrentCharakter()
        {
            ObservableCollection<ITalent> talents = null;

            if(typeof(CharakterDSA) == charakter.GetType())
            {
                talents = TalenteDSA;
            }
            else if (typeof(CharakterDSA) == charakter.GetType())
            {

                talents = TalentePNP;
            } 
            else
            {
                Logger.Log(LogLevel.ErrorLog, "Talent nicht verfügbar", nameof(Game), nameof(GetTalentForCurrentCharakter));
            }

            return talents;
        }
        #endregion
        public static string GetProbeShort(CharakterAttribut attribut)
        {
            var ret = string.Empty;

            if(attribut == CharakterAttribut.Charisma)
            {
                ret = "CH";
            }
            else if(attribut == CharakterAttribut.Fingerfertigkeit)
            {
                ret = "FF";
            }
            else if(attribut == CharakterAttribut.Gewandheit)
            {
                ret = "GE";
            }
            else if(attribut == CharakterAttribut.Intuition)
            {
                ret = "IN";
            }
            else if(attribut == CharakterAttribut.Klugheit)
            {
                ret = "KL";
            }
            else if(attribut == CharakterAttribut.Konstitution)
            {
                ret = "KO";
            }
            else if(attribut == CharakterAttribut.Körperkraft)
            {
                ret = "KK";
            }
            else if(attribut == CharakterAttribut.Mut)
            {
                ret = "MU";
            }
            else if(attribut == CharakterAttribut.Sozialstatus)
            {
                ret = "SO";
            } 
            else
            {
                ret = attribut.ToString();
            }
            return ret;
        }
    }
}
