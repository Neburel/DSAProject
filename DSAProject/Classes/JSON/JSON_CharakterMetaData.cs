using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.JSON
{
    [DataContract]
    public class JSON_CharakterMetaData : AbstractJSONSerializable<JSON_CharakterMetaData>
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public string Game { get; set; }

        [DataMember]
        public string SaveFile { get; set; }

        [DataMember(Name = "SaveTime")]
        public string SaveTimeAsString { get; set; }
        
        [IgnoreDataMember]
        public DateTime SaveTime
        {
            get => Convert.ToDateTime(SaveTimeAsString);
            set
            {
                SaveTimeAsString = value.ToLongTimeString();
            }
        }
        [IgnoreDataMember]
        public string ShortGame
        {
            get => Game.Substring(Game.Length - 3);
        }
    }
}
