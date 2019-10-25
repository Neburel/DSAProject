using System;
using System.Runtime.Serialization;

namespace DSALib.Classes.JSON
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
                SaveTimeAsString = value.ToString("MM/dd/yyyy H:mm");
            }
        }
        [IgnoreDataMember]
        public string ShortGame
        {
            get => Game.Substring(Game.Length - 3);
        }
    }
}
