using DSALib.JSON;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
{

    [DataContract]
    public class JSON_TalentSaveFile : AbstractJSONSerializable<JSON_TalentSaveFile>
    {
        [DataMember(Name = "Talente_DSA")]
        public List<JSON_Talent> Talente { get; set; }
        [DataMember(Name = "Families_DSA")]
        public List<JSON_TalentLanguageFamily>  Families { get; set; }


        [DataMember]
        public List<JSON_Talent> Talente_PNP { get; set; }

    }
}
