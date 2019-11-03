using DSALib.Classes.JSON;
using DSAProject.Classes;
using DSAProject.Classes.Interfaces;
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

            var exelTalente = TalentHelper.ExcelImport(importFile);
            var jsonString = string.Empty;
            var talenteCurrent = new ObservableCollection<ITalent>();

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

            foreach (var item in talenteCurrent)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);
                jSON_talentLocal.Talente_DSA.Add(jsonTalent);
            }
            jSON_talentLocal.Talente_DSA =  new List<JSON_Talent>(jSON_talentLocal.Talente_DSA.OrderBy(x => x.Name));

            //Set Guid
            foreach (var item in exelTalente)
            {                
                var doppleTalent = jSON_talentLocal.Talente_DSA.Where(x => x.Name == item.Name.Trim()).FirstOrDefault();
                if (doppleTalent != null)
                {
                    jSON_talentLocal.Talente_DSA.Remove(doppleTalent);
                    item.ID = doppleTalent.ID; 
                }
            }
            foreach(var item in exelTalente)
            {
                var jsonTalent = TalentHelper.CreateJSON(
                    talent: item);


                jSON_talentLocal.Talente_DSA.Add(jsonTalent);
            }

            File.WriteAllText(saveFile, jSON_talentLocal.JSONContent);


            var talents = TalentHelper.LoadTalent(jSON_talentLocal.Talente_DSA);
        }
    }
}
