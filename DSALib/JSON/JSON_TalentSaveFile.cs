using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DSALib.Classes.JSON
{

    [DataContract]
    public class JSON_TalentSaveFile : AbstractJSONSerializable<JSON_TalentSaveFile>
    {
        [DataMember]
        public List<JSON_Talent> Talente_DSA { get; set; } 
        [DataMember]
        public List<JSON_Talent> Talente_PNP { get; set; }

    }
}
