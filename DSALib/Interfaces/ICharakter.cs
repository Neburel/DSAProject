﻿using DSALib.Classes.JSON;
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
        ICharakterValues Values { get; }
        ICharakterAttribut Attribute { get; }
        ICharakterResources Resources { get;}
        CharakterTalente Talente { get; }
        CharakterDescription Descriptions { get; }
        

        JSON_Charakter CreateSave();
        void Load(JSON_Charakter json_charakter);
    }
}
