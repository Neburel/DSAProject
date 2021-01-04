using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.View
{
    public class TraitView
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string GP { get; set; }
        public string Value { get; set; }

        public int APInvest { get; set; }
        public int APGain { get; set; }
        public TraitType Type { get; set; }

        public DateTime ModifyDate { get; set; }
        public DateTime CreationDate { get; set; }

        public List<TalentView> TalentList { get; set; }
        public List<IDValueView<CharakterAttribut>> AttributList { get; set; }
        public List<IDValueView<string>> ResourceList { get; set; }
        public List<IDValueView<string>> ValueList { get; set; }        
    }
}

