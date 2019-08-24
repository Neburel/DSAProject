using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.JSON
{
    [DataContract]
    public class JSON_Descriptor : AbstractJSONSerializable<JSON_Charakter>
    {
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public string DescriptionTitle { get; set;}
        [DataMember]
        public string DescriptionText { get; set; }
    }
}
