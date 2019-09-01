using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{
    [DataContract]
    public class JSON_Charakter : AbstractJSONSerializable<JSON_Charakter>
    {
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeBaseValue { get; set; }
        [DataMember]
        public List<JSON_Descriptor> Descriptors { get; set; }
    }
}
