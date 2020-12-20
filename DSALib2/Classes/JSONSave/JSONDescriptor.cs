using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
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
