using DSALib2.Utils;
using System;

namespace DSALib2.Classes.Charakter.Talente.TalentLanguage
{
    public class TalentSpeaking : AbstractTalentLanguage
    {
        public TalentSpeaking(Guid id) : base(id) 
        {
            AttributList = new System.Collections.Generic.List<CharakterAttribut>
            {
                CharakterAttribut.Klugheit,
                CharakterAttribut.Intuition,
                CharakterAttribut.Charisma
            };
        }
        public override string ToString()
        {
            return base.ToString() + "(Sprache)";
        }
    }
}


