using System;
using System.Runtime.Serialization;

namespace DSALib2.Classes.JSONSave
{
    [DataContract]
    public class JSONCharakterMetaData : AbstractJSONSerializable<JSONCharakterMetaData>
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
            get => Convert.ToDateTime(SaveTimeAsString, DSALib.Utils.Helper.CultureInfo);
            set
            {
                SaveTimeAsString = value.ToString("MM/dd/yyyy H:mm", DSALib.Utils.Helper.CultureInfo);
            }
        }
        [IgnoreDataMember]
        public string ShortGame
        {
            get => Game.Substring(Game.Length - 3);
        }
    }
}
