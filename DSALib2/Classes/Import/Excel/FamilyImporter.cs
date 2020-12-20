using DSALib2.Charakter.Talente;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib2.Classes.Import.Excel
{
    public class FamilyImporter
    {
        public LanguageFamily Family { get; set; }
        public List<TalentExcelImport> Speaking { get; set; } = new List<TalentExcelImport>();
        public List<TalentExcelImport> Writing { get; set; } = new List<TalentExcelImport>();

        public override string ToString()
        {
            return Family.ToString();
        }
    }
}
