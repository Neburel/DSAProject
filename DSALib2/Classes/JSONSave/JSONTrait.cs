using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
{
    [DataContract]
    public class JSONTrait : AbstractJSONSerializable<JSONTrait>
    {
        [DataMember(Name = nameof(TraitType))]
        public int TraitTypeAsInt { get; set; }
        [DataMember]
        public int APEarned { get; set; }
        [DataMember]
        public int APInvest { get; set; }

        [DataMember]
        public string GP { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Dictionary<string, int> ValueValues { get; set; }
        [DataMember]
        public Dictionary<string, int> ResourceValues { get; set; }
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeValues { get; set; }

        [DataMember]
        public Dictionary<Guid, int> TawBonus { get; set; }
        [DataMember]
        public Dictionary<Guid, int> AtBonus { get; set; }
        [DataMember]
        public Dictionary<Guid, int> PaBonus { get; set; }
        [DataMember]
        public Dictionary<Guid, int> BLBonus { get; set; }

        [IgnoreDataMember]
        public TraitType TraitType
        {
            get => (TraitType)TraitTypeAsInt;
            set => TraitTypeAsInt = (int)value;
        }
    }
}
