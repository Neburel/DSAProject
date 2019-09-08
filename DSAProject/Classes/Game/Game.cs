﻿using DSALib;
using DSALib.Classes.JSON;
using DSALib.Utils;

using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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

            try
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: talent,
                    gameType: GameType.DSA);

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
                error = new Error
                {
                    ErrorCode = ErrorCode.InvalidValue,
                    Message = ex.Message
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
                    TalentHelper.LoadTalent(jSON_talentLocal.Talente_DSA, TalenteDSA);
                    TalentHelper.LoadTalent(jSON_talentLocal.Talente_PNP, TalentePNP);
                }

            }

            jString = FileManagment.LoadTextAssestFile(talentSaveFile, out error);
            if (error == null)
            {
                var jSON_talentAssests = JSON_TalentSaveFile.DeSerializeJson(jString, out string serror);

                if (serror == null)
                {
                    TalentHelper.LoadTalent(jSON_talentAssests.Talente_DSA, TalenteDSA);
                    TalentHelper.LoadTalent(jSON_talentAssests.Talente_PNP, TalentePNP);
                }
            }

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
