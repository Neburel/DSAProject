using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.JSON
{
    [DataContract]
    public class JSON_Talent : AbstractJSONSerializable<JSON_Talent>
    {
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
        [DataMember]
        public Guid FatherTalent { get; set; }
        [DataMember(Name = "Deductions")]
        public Dictionary<Guid, int> DeductionTalents { get; set; } = new Dictionary<Guid, int>();
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
            get => Convert.ToDateTime(LastEditDateAsString);
            set
            {
                LastEditDateAsString = value.ToLongTimeString();
            }
        }
    }
}
