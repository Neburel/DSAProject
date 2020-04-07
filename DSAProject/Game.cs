using DSALib;
using DSALib.Charakter.Talente;
using DSALib.Classes.JSON;
using DSALib.Exceptions;
using DSALib.Utils;

using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

    public enum NavEnum
    {
        StartPage = 1,
        CreateTraitPage = 2
    };

    public static class Game
    {
        public static event EventHandler<EventNavRequest> NavRequested;
        public static event EventHandler CharakterChanged;
        #region Variables
        private static string talentSaveFile = "Talente.json";
        private static ICharakter charakter;
        private static JSONTalentSaveFile jSON_talentLocal = new JSONTalentSaveFile();
        #endregion
        #region Properties
        public static ICharakter Charakter
        {
            get
            {
                if (charakter != null)
                {
                    return charakter;
                }
                else
                {
                    Charakter = new CharakterDSA(GenerateNextCharakterGUID(), TalentList);
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
        public static string CurrentYearDSA { get; } = "? nach Bosporos Fall";
        public static List<ITalent> TalentList { get; private set; } = new List<ITalent>();
        public static List<LanguageFamily> LanguageFamilyList = new List<LanguageFamily>();
        #endregion
        #region Funktion
        public static void RequestNav(EventNavRequest nav)
        {
            NavRequested?.Invoke(null, nav);
        }
        public static void SaveTalent(ITalent talent, GameType gameType, out DSAError error)
        {
            error = null;
            #region Talenttype
            List<JSONTalent> jTalentList = null;
            List<ITalent> talentList = null;
            var talenttype = talent.GetType().ToString();
            var lastIndex = talenttype.LastIndexOf(".");
            talenttype = talenttype.Substring(lastIndex + 1);
            #endregion
            #region GameType
            if (gameType == GameType.DSA)
            {
                if (jSON_talentLocal.Talente == null) jSON_talentLocal.Talente = new List<JSONTalent>();
                jTalentList = jSON_talentLocal.Talente;
                talentList = TalentList;
            }
            else
            {
                throw new Exception();
            }
            #endregion

            try
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: talent);

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
                FileManagment.WriteToFile(jSON_talentLocal.JSONContent, talentSaveFile, Windows.Storage.CreationCollisionOption.ReplaceExisting, out error);
                #endregion
            }
            catch (Exception ex)
            {
                error = new DSAError
                {
                    ErrorCode = ErrorCode.InvalidValue,
                    Message = ex.Message
                };
            }
        }
        public static void LoadTalente()
        {
            var jstringAssests = FileManagment.LoadTextAssestFile(talentSaveFile, out DSAError errorAssest);
            var jSON_talentAssests = JSONTalentSaveFile.DeSerializeJson(jstringAssests, out string serrorAssest);

            TalentList = TalentHelper.LoadTalent(jSON_talentAssests.Talente);
            LanguageFamilyList = TalentHelper.LoadLanguageFamily(jSON_talentAssests.Families, TalentList);
        }
        public static Guid GenerateNextCharakterGUID()
        {
            var guid = Guid.NewGuid();
            var files = FileManagment.GetFilesDictionary(CharakterSaveFolder, out DSAError error);
            var list = new List<Guid>();

            foreach (var file in files)
            {
                var stringguid = System.IO.Path.ChangeExtension(file, null);

                if (Guid.TryParse(stringguid, out Guid result))
                {
                    list.Add(result);
                }
            }

            while (list.Contains(guid))
            {
                guid = new Guid();
            }

            return guid;
        }
        public static List<ITalent> GetTalentForCurrentCharakter()
        {
            List<ITalent> talents = null;

            if (typeof(CharakterDSA) == charakter.GetType())
            {
                talents = TalentList;
            }
            else
            {
                Logger.Log(LogLevel.ErrorLog, "Talent nicht verfügbar", nameof(Game), nameof(GetTalentForCurrentCharakter));
            }

            return talents;
        }
        public static void CharakterSave(out DSAError error)
        {
            error = null;
            try
            {
                var saveFile = Charakter.CreateSave();
                var filePath = Path.Combine(CharakterSaveFolder, Charakter.ID.ToString() + ".save");
                FileManagment.WriteToFile(saveFile.JSONContent, filePath, Windows.Storage.CreationCollisionOption.ReplaceExisting, out error);

                #region Sicherungskopie
                var task = new Task(async () =>
                {
                    try
                    {
                        var folder = await StorageFolder.GetFolderFromPathAsync("D:\\Dropbox\\07_DSA_PNP_D&D\\DSA_Save");
                        var sfile = await folder.CreateFileAsync(Charakter.ID.ToString() + ".save", CreationCollisionOption.ReplaceExisting);
                        await FileIO.AppendTextAsync(sfile, saveFile.JSONContent);
                    }
                    catch (Exception)
                    {
                        Logger.Log(LogLevel.ErrorLog, "Sicherung konnte nicht erstellt werden");
                        //"https://support.microsoft.com/de-de/help/4468237/windows-10-file-system-access-and-privacy-microsoft-privacy"
                    }
                });
                task.Start();
                #endregion
            }
            catch (Exception ex)
            {
                error = new DSAError
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
        public static void LoadCharakter(JSONCharakter json_charakter, out DSAError error)
        {
            error = null;
            charakter.Load(json_charakter, TalentList.ToList());
        }

        #endregion
    }
}
