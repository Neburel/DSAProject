using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
{

    [DataContract]
    public class JSONTalentSaveFile : AbstractJSONSerializable<JSONTalentSaveFile>
    {
        [DataMember(Name = "Talente_DSA")]
        public List<JSONTalent> Talente { get; set; }
        [DataMember(Name = "Families_DSA")]
        public List<JSONTalentLanguageFamily>  Families { get; set; }
    }
}
