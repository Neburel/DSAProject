using DSALib.Charakter.Talente;
using DSALib.Classes.JSON;
using DSALib.JSON;
using DSAProject.Classes;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDic      = Directory.GetCurrentDirectory();
            var importDirectory = Path.Combine(currentDic, "Imports");
            var importFile      = Path.Combine(importDirectory, "TalentImport.xlsx");
            var talentFile      = Path.Combine(currentDic, "OldTalente.json");
            var saveFile        = Path.Combine(currentDic, "Talente.json");

            var exelTalente     = TalentHelper.ExcelImport(importFile, out List<LanguageFamily> families);
            var jsonString      = string.Empty;
            var talenteCurrent  = new ObservableCollection<ITalent>();
            var counter = 0;

            if (File.Exists(talentFile))
            {
                jsonString = File.ReadAllText(talentFile);
                var json = JSON_TalentSaveFile.DeSerializeJson(jsonString, out string serrorAssest);
                talenteCurrent = TalentHelper.LoadTalent(json.Talente_DSA);
            }

            var jSON_talentLocal = new JSON_TalentSaveFile
            {
                Talente_DSA = new List<JSON_Talent>()
            };

            #region Alte Talente
            Console.WriteLine("Alte Talente:");
            counter = 0;
            foreach (var item in talenteCurrent)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);
                jSON_talentLocal.Talente_DSA.Add(jsonTalent);
                Console.WriteLine(counter++ + " " + item.Name + " " + item.ID);
            }
            Console.WriteLine("------------------------------------------------------------");
            #endregion
            #region Neue Talente
            Console.WriteLine("Neue Talente:");
            counter = 0;
            foreach (var item in exelTalente)
            {
                Console.WriteLine(counter++ + " " + item.Name + " " + item.ID);
            }
            Console.WriteLine("------------------------------------------------------------");
            #endregion

            jSON_talentLocal.Talente_DSA    =  new List<JSON_Talent>(jSON_talentLocal.Talente_DSA.OrderBy(x => x.Name));
            jSON_talentLocal.Families_DSA   = new List<JSON_TalentLanguageFamily>();


            //Set Guid
            counter = 0;
            foreach (var item in exelTalente)
            {                
                if(item.Name == "Schleuder")
                {

                }

                var doppleTalent = jSON_talentLocal.Talente_DSA.Where(x => x.Name.Trim() == item.Name.Trim()).FirstOrDefault();
                if (doppleTalent != null)
                {
                    Console.WriteLine(counter++ + " Doppeltes Talent: " + doppleTalent.Name + " " + item.ID + " " + doppleTalent.ID);
                    jSON_talentLocal.Talente_DSA.Remove(doppleTalent);
                    item.ID = doppleTalent.ID;
                }
            }
            Console.WriteLine("------------------------------------------------------------");

            foreach (var item in jSON_talentLocal.Talente_DSA)
            {
                Console.WriteLine(counter++ + " Nicht Doppeltes Talent: " + item.Name + " " + item.ID);
            }

            foreach(var item in exelTalente)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);


                jSON_talentLocal.Talente_DSA.Add(jsonTalent);
            }
            foreach(var item in families)
            {
                var famile = new JSON_TalentLanguageFamily
                {
                    Name = item.Name
                };

                foreach(var inneritem in item.Languages)
                {
                    famile.Languages.Add(inneritem.Key, inneritem.Value.ID);
                }
                foreach (var inneritem in item.Writings)
                {
                    famile.Writings.Add(inneritem.Key, inneritem.Value.ID);
                }
                jSON_talentLocal.Families_DSA.Add(famile);
            }

            File.WriteAllText(saveFile, jSON_talentLocal.JSONContent);
            var talents = TalentHelper.LoadTalent(jSON_talentLocal.Talente_DSA);
        }
    }
}
