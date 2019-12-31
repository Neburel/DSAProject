using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
