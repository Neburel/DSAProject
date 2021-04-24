using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
{
    [DataContract]
    public class JSONTalentLanguageFamily
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Dictionary<int, Guid> Writings { get; set; } 
        [DataMember]
        public Dictionary<int, Guid> Languages { get; set; }
        [DataMember]
        public KeyValuePair<string, string> Languages2 { get; set; }
    }
}
