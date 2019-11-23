using DSALib.Charakter.Talente.TalentLanguage;
using System.Collections.Generic;

namespace DSALib.Charakter.Talente
{
    public class LanguageFamily
    {
        public string Name { get; private set; }
        public Dictionary<int, TalentWriting> Writings { get; } = new Dictionary<int, TalentWriting>();
        public Dictionary<int, DSAProject.Classes.Charakter.Talente.TalentLanguage.TalentLanguage> Languages { get; } = new Dictionary<int, DSAProject.Classes.Charakter.Talente.TalentLanguage.TalentLanguage>();

        public LanguageFamily(string name)
        {
            Name = name;
        }
    }
}
