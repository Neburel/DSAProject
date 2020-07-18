using DSALib.Charakter.UserGenerated;
using DSALib.Exceptions;
using System.Collections.Generic;

namespace DSALib.Charakter.Other
{
    
    public class Spell : AbstractUserGenerateAttribute
    {
        public int Level { get; set; }
        public string Effect { get; set; }
        public string Komplex1 { get; set; }
        public string Komplex2 { get; set; }
        public string Characteristics { get; set; }
        public string Difficult { get; set; }


        public Spell(string name, List<CharakterAttribut> attributList) : base(name, attributList)
        {
            if (string.IsNullOrEmpty(name)) throw new DSABaseException(ErrorCode.NullValue, "Zauber ohne name");
        }
    }
}
