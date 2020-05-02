using DSALib.JSON;
using DSALib.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{
    [DataContract]
    public class JSONCharakter : AbstractJSONSerializable<JSONCharakter>
    {
        #region Meta Data
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember(Name = "SaveTime")]
        public string SaveTimeAsString { get; set; }

        [IgnoreDataMember]
        public DateTime SaveTime
        {
            get => Convert.ToDateTime(SaveTimeAsString, Helper.CultureInfo);
            set
            {
                SaveTimeAsString = value.ToString("dd/MM/yyyy H:mm", Helper.CultureInfo);
            }
        }
        #endregion
        [DataMember]
        public int AktAP { get; set; }
        [DataMember]
        public int InvestAP { get; set; }
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeBaseValue { get; set; }
        [DataMember]
        public Dictionary<string, int> SettableValues { get; set; }
        [DataMember]
        public List<JSONDescriptor> Descriptors { get; set; }
        [DataMember]
        public Dictionary<Guid, int> TalentTAW { get; set; }
        [DataMember]
        public Dictionary<Guid, int> TalentAT { get; set; }
        [DataMember]
        public Dictionary<Guid, int> TalentPA { get; set; }
        [DataMember]
        public Dictionary<Guid, bool> MotherLanguages { get; set; }
        [DataMember]
        public Dictionary<Guid, string> TalentGuidsNames { get; set; }
        [DataMember]
        public List<JSONTrait> Traits { get; set; }
        [DataMember]
        public JSONMoney Money { get; set; }
        [DataMember]
        public Dictionary<Guid, Guid> DeductionTalent { get; set; }
    }
}
