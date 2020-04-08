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
            var talenteCurrent  = new List<ITalent>();
            var counter = 0;

            if (File.Exists(talentFile))
            {
                jsonString = File.ReadAllText(talentFile);
                var json = JSONTalentSaveFile.DeSerializeJson(jsonString, out string serrorAssest);
                talenteCurrent = TalentHelper.LoadTalent(json.Talente);
            }

            var jSON_talentLocal = new JSONTalentSaveFile
            {
                Talente = new List<JSONTalent>()
            };

            #region Alte Talente
            Console.WriteLine("Alte Talente:");
            counter = 0;
            foreach (var item in talenteCurrent)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);
                jSON_talentLocal.Talente.Add(jsonTalent);
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

            jSON_talentLocal.Talente    =  new List<JSONTalent>(jSON_talentLocal.Talente.OrderBy(x => x.Name));
            jSON_talentLocal.Families   = new List<JSONTalentLanguageFamily>();

            var neueTalente = new List<ITalent>();
            //Set Guid
            counter = 0;
            foreach (var item in exelTalente)
            {
                var doppleTalent = jSON_talentLocal.Talente.Where(x => x.Name.Trim() == item.Name.Trim()).FirstOrDefault();
                if (doppleTalent != null)
                {
                    Console.WriteLine(counter++ + " Doppeltes Talent: " + doppleTalent.Name + " " + item.ID + " " + doppleTalent.ID);
                    jSON_talentLocal.Talente.Remove(doppleTalent);
                    item.ID = doppleTalent.ID;
                }
                else
                {
                    neueTalente.Add(item);
                }
            }
            Console.WriteLine("------------------------------------------------------------");

            foreach (var item in jSON_talentLocal.Talente)
            {
                Console.WriteLine(counter++ + " Nicht Doppeltes Altes Talent: " + item.Name + " " + item.ID);
            }
            Console.WriteLine("------------------------------------------------------------");

            foreach (var item in neueTalente)
            {
                Console.WriteLine("Neues Talent: " + item.Name + " " + item.ID);
            }
            Console.WriteLine("------------------------------------------------------------");
            foreach (var item in exelTalente)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);
                jSON_talentLocal.Talente.Add(jsonTalent);
                Console.WriteLine("Hinzugefügtes Talent: " + item.Name + " " + item.ID);
            }
            Console.WriteLine("------------------------------------------------------------");
            foreach (var item in families)
            {
                var famile = new JSONTalentLanguageFamily
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
                jSON_talentLocal.Families.Add(famile);
                Console.WriteLine("Hinzugefügtes Familie: " + item.Name);
            }

            File.WriteAllText(saveFile, jSON_talentLocal.JSONContent);
            var talents = TalentHelper.LoadTalent(jSON_talentLocal.Talente);
            Console.ReadKey();
        }
    }
}
