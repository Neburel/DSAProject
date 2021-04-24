using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.View
{
    public class TalentView
    {
        public Guid ID { get; set; }
        public string BE { get; set; }
        public int TAW { get; set; }
        public int? AT { get; set; }
        public int? PA { get; set; }
        public int? BL { get; set; }
        public string Name { get; set; }
        public string Probe { get; set; }
        public string ProbeString { get; set; }
        public string Spezialisierung { get; set; }
        public TextView DeductionText { get; set; }
        public TextView RequirementText { get; set; }
        public DeductionView DeductionSelected { get; set; }
        public List<DeductionView> DeductionList { get; set; }

        public override string ToString()
        {
            return Name + " " + this.ID.ToString();
        }
    }
}
