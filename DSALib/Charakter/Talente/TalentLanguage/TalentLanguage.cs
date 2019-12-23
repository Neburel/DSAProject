using System;


namespace DSAProject.Classes.Charakter.Talente.TalentLanguage
{
    public class TalentLanguage : AbstractTalentLanguage
    {
        public TalentLanguage(Guid id) : base(id) 
        {
            Attributs = new System.Collections.Generic.List<DSALib.CharakterAttribut>
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


