using DSALib2.Classes.Import;
using System.Collections.Generic;
namespace DSAProject2Web.Server.Entities
{
    public class TalentImportRequest
    {
        public List<TalentExcelImport> Weaponless { get; set; }
        public List<TalentExcelImport> Close { get; set; }
        public List<TalentExcelImport> Range { get; set; }
        public List<TalentExcelImport> Language { get; set; }
        public List<TalentExcelImport> Physical { get; set; }
        public List<TalentExcelImport> Social { get; set; }
        public List<TalentExcelImport> Nature { get; set; }
        public List<TalentExcelImport> Knowldage { get; set; }
        public List<TalentExcelImport> Crafting { get; set; }
    }
}
