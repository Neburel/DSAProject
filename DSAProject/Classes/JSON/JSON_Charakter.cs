using DSAProject.Classes.Charakter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.JSON
{
    [DataContract]
    public class JSON_Charakter : AbstractJSONSerializable<JSON_Charakter>
    {
        [DataMember]
        public Dictionary<CharakterAttribut, int> AttributeBaseValue {get; set;}
    }
}
