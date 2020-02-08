using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{
    [DataContract]
    public class JSONDescriptor : AbstractJSONSerializable<JSONCharakter>
    {
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public string DescriptionTitle { get; set;}
        [DataMember]
        public string DescriptionText { get; set; }
    }
}
