using DSALib.Charakter;
using DSALib.Charakter.Functions;
using DSALib.Charakter.Other;
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
        CharakterSpellBook CharakterSpellBook { get; }
        CharakterValues Values { get; }
        CharakterAttribute Attribute { get; }
        CharakterResources Resources { get;}
        CharakterTalente Talente { get; }
        CharakterDescription Descriptions { get; }
        CharakterTraits Traits { get; }
        CharakterOther Other { get; }
        Money Money { get; }

      JSONCharakter CreateSave();
        void Load(JSONCharakter jsoncharakter, List<ITalent> talents);
    }
}
