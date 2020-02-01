using System;


namespace DSAProject.Classes.Charakter.Talente.TalentLanguage
{
    public class TalentSpeaking : AbstractTalentLanguage
    {
        public TalentSpeaking(Guid id) : base(id) 
        {
            AttributList = new System.Collections.Generic.List<DSALib.CharakterAttribut>
            {
                DSALib.CharakterAttribut.Klugheit,
                DSALib.CharakterAttribut.Intuition,
                DSALib.CharakterAttribut.Charisma
            };
        }
        public override string ToString()
        {
            return base.ToString() + "(Sprache)";
        }
    }
}


