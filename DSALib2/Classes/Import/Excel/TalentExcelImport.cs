namespace DSALib2.Classes.Import
{
    public class TalentExcelImport
    {
        public int OrginalPosition { get; set; }

        public string Talent { get; set; }
        public string Title { get; set; }
        public string Probe { get; set; }
        public string TaW { get; set; }
        public string BE { get; set; }
        public string Billiger { get; set; }
        public string Spezialisierung { get; set; }
        public string Waffenmeister { get; set; }
        public string Ableiten { get; set; }
        public string Anforderungen { get; set; }
        public string VerwandteFertigkeiten { get; set; }

        public string Sprache { get; set; }
        public string Schrift { get; set; }

        public string Komplex1 { get; set; }
        public string Komplex2 { get; set; }

        public string AlterName { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Talent)) return Talent;
            if (!string.IsNullOrEmpty(Sprache)) return Sprache;
            if (!string.IsNullOrEmpty(Schrift)) return Schrift;
            return base.ToString();
        }
    }
}
