using DSALib2.Classes.Charakter.Talente;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Charakter.Talente.TalentLanguage
{
    public class TalentWriting : AbstractTalentLanguage
    {
        public TalentWriting(Guid id) : base(id) 
        {
            AttributList = new List<CharakterAttribut>
            {
               CharakterAttribut.Klugheit,
                CharakterAttribut.Klugheit,
                CharakterAttribut.Fingerfertigkeit
            };
            this.NameExtension = "Schrift";
        }
        public override string ToString()
        {
            return base.ToString() +"(Schrift)" ; 
        }
    }
}
