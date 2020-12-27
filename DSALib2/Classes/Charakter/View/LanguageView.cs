using System;

namespace DSALib2.Classes.Charakter.View
{
    public class LanguageView
    {
        public int TawSprache { get; set; }
        public int TawSchrift { get; set; }
        public bool IsTitle { get; set; }
        public bool? Mother { get; set; }
        public Guid IDSprache { get; set; }
        public Guid IDSchrift { get; set; }
        public string Sprache { get; set; }
        public string Schrift { get; set; }
        public string ProbeSprache { get; set; }
        public string ProbeSchrift { get; set; }
        public string KomplexSprache { get; set; }
        public string KomplexSchrift { get; set; }
    }
}
