using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSALib2.Classes.Charakter
{
    public class SaveGameFile
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SaveTime { get; set; }
        public int AktAP { get; set; }
        public int InvestAP { get; set; }
        public Dictionary<string, int> TalentBL { get; set; }
    }
}
