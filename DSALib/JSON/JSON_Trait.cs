using DSALib.Classes.JSON;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.JSON
{
    [DataContract]
    public class JSON_Trait : AbstractJSONSerializable<JSON_Trait>
    {
        [DataMember]
        public string GP { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Dictionary<string, int> ValueValues;
        [DataMember]
        public Dictionary<string, int> ResourceValues;
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeValues;
    }
}
