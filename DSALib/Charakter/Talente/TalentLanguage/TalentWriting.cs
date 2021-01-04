using DSAProject.Classes.Charakter.Talente;
using System;

namespace DSALib.Charakter.Talente.TalentLanguage
{
    public class TalentWriting : AbstractTalentLanguage
    {
        private string name;
        public TalentWriting(Guid id) : base(id) 
        {
            AttributList = new System.Collections.Generic.List<DSALib.CharakterAttribut>
            {
                DSALib.CharakterAttribut.Klugheit,
                DSALib.CharakterAttribut.Klugheit,
                DSALib.CharakterAttribut.Fingerfertigkeit
            };
            this.NameExtension = "(Schrift);";
        }
        public new string Name {
            get; 
            set; 
        }
    }
}
