using DSALib.JSON;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{
    [DataContract]
    public class JSON_Charakter : AbstractJSONSerializable<JSON_Charakter>
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
            get => Convert.ToDateTime(SaveTimeAsString);
            set
            {
                SaveTimeAsString = value.ToString("dd/MM/yyyy H:mm");
            }
        }
        #endregion
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeBaseValue { get; set; }
        [DataMember]
        public Dictionary<string, int> SettableValues { get; set; }
        [DataMember]
        public List<JSON_Descriptor> Descriptors { get; set; }
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
        public List<JSON_Trait> Traits { get; set; }
    }
}
