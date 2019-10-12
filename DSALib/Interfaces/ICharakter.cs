using DSALib.Charakter;
using DSALib.Classes.JSON;
using DSALib.Interfaces;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
using System;

namespace DSAProject.Classes.Interfaces
{
    public interface ICharakter
    {
        Guid ID { get; }
        string Name { get; set; }
        CharakterValues Values { get; }
        CharakterAttribute Attribute { get; }
        CharakterResources Resources { get;}
        CharakterTalente Talente { get; }
        CharakterDescription Descriptions { get; }
        

        JSON_Charakter CreateSave();
        void Load(JSON_Charakter json_charakter);
    }
}
