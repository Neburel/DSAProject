using DSALib2.Interfaces.Charakter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DSALib2.Classes.Tools
{
    public static class TalentMerger
    {
        private const string DEVIDER = "-------------------------------------------------------------";
        public static List<ITalent> Test(List<ITalent> newTalenList, List<ITalent> oldTalentList, Dictionary<ITalent, string> talentWithNewNames)
        {
            //Voraussetzung: Keine Doppelten Namen in den Listen
            CheckForDoubleNames(newTalenList);
            //CheckForDoubleNames(oldTalentList);

            Debug.WriteLine(DEVIDER);
            List<ITalent> foundTalentList = new List<ITalent>();
            List<ITalent> notFoundTalentList = new List<ITalent>();
            List<ITalent> usedOldItems = new List<ITalent>();

            newTalenList = newTalenList.OrderBy(x => x.Name).ToList();
            oldTalentList = oldTalentList.OrderBy(x => x.Name).ToList();
            
            //Die Test Funktionieren weil davon ausgegangen wird das keine Doppelten Namen exestieren
            foreach(var newTalent in newTalenList)
            {
                var oldTalent = oldTalentList.Where(x => x.Name.Trim() == newTalent.Name && x.GetType() == newTalent.GetType()).FirstOrDefault();

                if(oldTalent == null)
                {
                    var resultDic = talentWithNewNames.Where(x => x.Key == newTalent).FirstOrDefault();
                    oldTalent = oldTalentList.Where(x => x.ToString() == resultDic.Value && x.GetType() == newTalent.GetType()).FirstOrDefault();
                }

                if (oldTalent != null)
                {
                    newTalent.ID = oldTalent.ID;
                    foundTalentList.Add(newTalent);
                    usedOldItems.Add(oldTalent);
                }
                else
                {
                    notFoundTalentList.Add(newTalent);
                }
            }
            var notUsedOldItems = oldTalentList.Where(x => !usedOldItems.Contains(x)).ToList();

            Debug.WriteLine(DEVIDER);
            Debug.WriteLine("Gefundene Talente");
            foreach(var item in foundTalentList)
            {
                Debug.WriteLine(item);
            }

            Debug.WriteLine(DEVIDER);
            Debug.WriteLine("Nicht Gefundene neu Talente");
            foreach (var item in notFoundTalentList)
            {
                Debug.WriteLine(item);
            }

            Debug.WriteLine(DEVIDER);
            Debug.WriteLine("Nicht Gefundene alte Talente");
            foreach(var item in notUsedOldItems)
            {
                Debug.WriteLine(item);
            }

            return newTalenList();
            //Nicht Gefundene Talente müssen mit einer ID Belegt werden
        }
        public static void CheckDifferenzTalent(ITalent talent1, ITalent talent2)
        {

        }
        private static void CheckForDoubleNames(List<ITalent> talentList)
        {
            Debug.WriteLine(DEVIDER);
            Debug.WriteLine("Talente mit Gleichen Name");
            var talentfound = false;
            foreach (var item in talentList)
            {
                var result = talentList.Where(x => x.Name == item.Name && x != item && item.GetType() == x.GetType()).ToList();
                if (result.Count() > 1)
                {
                    talentfound = true;
                    foreach (var innerItem in result)
                    {
                        Debug.WriteLine(item);
                    }
                }
            }
            if (talentfound)
            {
                throw new ArgumentException("Keine Doppelten Namen");
            }
        }
    }
}
