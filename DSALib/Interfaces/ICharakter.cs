﻿using DSALib.Charakter;
using DSALib.Charakter.Functions;
using DSALib.Classes.JSON;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
using System;
using System.Collections.Generic;

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
        CharakterTraits Traits { get; }
        CharakterOther Other { get; }

        JSON_Charakter CreateSave();
        void Load(JSON_Charakter json_charakter, List<ITalent> talents);
    }
}
