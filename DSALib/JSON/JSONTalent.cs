using DSALib.JSON;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{
    [DataContract]
    public class JSONTalent : AbstractJSONSerializable<JSONTalent>
    {
        [DataMember]
        public int OrginalPos { get; set; }
        [DataMember]
        public string BE { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NameExtension { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember(Name = "LastEditDate")]
        public string LastEditDateAsString { get; set; }
        [DataMember]
        public Guid ID { get; set; }
        //[DataMember(Name = "Deductions")]
        //public Dictionary<Guid, int> DeductionTalents { get; set; } = new Dictionary<Guid, int>();
        [DataMember(Name = "DeductionTalentList")]
        public List<JSONTalentDeduction> DeductionTalentList { get; set; } = new List<JSONTalentDeduction>();
        [DataMember(Name = "DeductionString")]
        public List<string> DeductionStrings { get; set; } = new List<string>();
        [DataMember]
        public List<CharakterAttribut> Probe { get; set; }
        #region Requirements
        [DataMember]
        public List<string> RequirementStrings { get; set; }
        [DataMember]
        public Dictionary<Guid, int> RequirementNeed { get; set; }
        [DataMember]
        public Dictionary<Guid, int> RequirementOff { get; set; }
        [DataMember]
        public Dictionary<CharakterAttribut, int> RequirementAttributs { get; set; }
        #endregion
        [IgnoreDataMember]
        public DateTime SaveTime
        {
            get => Convert.ToDateTime(LastEditDateAsString, Utils.Helper.CultureInfo);
            set
            {
                LastEditDateAsString = value.ToLongTimeString();
            }
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
