using DSALib2.Utils;

namespace DSALib2.Classes.Charakter.View
{
    public class DescriptionView
    {
        public string Name { get; set; }
        public string Anrede { get; set; }
        public string Rasse { get; set; }
        public string Kultur { get; set; }
        public string Profession { get; set; }
        public int Alter { get; set; }
        public string Geburstag { get; set; }
        public GeschlechtEnum Geschlecht { get; set; }
        public FamilienstatusEnum Familienstatus { get; set; }
        public string Hautfarbe { get; set; }
        public string Haarfarbe { get; set; }
        public string Augenfarbe { get; set; }
        public string Glaube { get; set; }
        public int Groesse { get; set; }
        public int Gewicht { get; set; }
    }
}
