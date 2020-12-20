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
        public Dictionary<int, Guid> Writings { get; set; } = new Dictionary<int, Guid>();
        [DataMember]
        public Dictionary<int, Guid> Languages { get; set; } = new Dictionary<int, Guid>();
    }
}
