using DSALib2.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Charakter.Talente
{
    public class LanguageFamily
    {
        public string Name { get; private set; }
        public Dictionary<int, TalentWriting> Writings { get; } = new Dictionary<int, TalentWriting>();
        public Dictionary<int, TalentSpeaking> Languages { get; } = new Dictionary<int, TalentSpeaking>();

        public LanguageFamily(string name)
        {
            Name = name;
        }
        public int GetHighestPosition()
        {
            int x = 0;
            int y = 0;

            if(Writings.Count != 0)
            {
                x = Writings.Keys.Max();
            }
            if(Languages.Count != 0)
            {
                y = Languages.Keys.Max();
            }
            return x > y ? x : y;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
