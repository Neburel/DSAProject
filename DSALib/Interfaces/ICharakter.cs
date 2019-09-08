using DSALib.Classes.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
using System;

namespace DSAProject.Classes.Interfaces
{
    public interface ICharakter
    {
        Guid ID { get; }
        string Name { get; set; }
        ICharakterValues Values { get; }
        ICharakterAttribut Attribute { get; }
        CharakterTalente CharakterTalente { get; }
        CharakterDescription CharakterDescriptions { get; }
        

        JSON_Charakter CreateSave();
        void Load(JSON_Charakter json_charakter);
    }
}
