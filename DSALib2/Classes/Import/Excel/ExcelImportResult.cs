using DSALib2.Charakter.Talente;
using DSALib2.Interfaces.Charakter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib2.Classes.Import.Excel
{
    public class ExcelImportResult
    {
        public List<LanguageFamily> LanguageFamilyList { get; set; }
        public List<ITalent> TalentList { get; set; }
        public Dictionary<ITalent, string> OldNameDictionary { get; set; }
    }
}


