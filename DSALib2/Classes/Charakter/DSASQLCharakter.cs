using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Repository.SQL;
using DSALib2.Classes.Charakter.Resources;
using DSALib2.Classes.Charakter.Values.Attribute;
using DSALib2.Classes.Charakter.Values.Extended;
using DSALib2.Classes.Charakter.Values.Other;
using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Classes.JSONSave;
using DSALib2.Classes.Tools;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.SQLDataBase;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter
{
    public class DSASQLCharakter : AbstractCharakter
    {
        private int charakterID;
        private string jsonTalentPath;
        private ApplicationContext applicationContext;

        public DSASQLCharakter(ApplicationContext applicationContext, int charakterID, string jsonTalentPath)
        {
            this.charakterID = charakterID;
            this.applicationContext = applicationContext;
            this.jsonTalentPath = jsonTalentPath;
        }

        protected override IAttributeRepository GetNewAttributeRepository()
        {
            return new SQLAttributRepository(applicationContext, charakterID);
        }
        protected override IResourcesRepository GetNewResourcesRepository()
        {
            var resourceList = new List<IResource>()
            {
                new AstralEnergy(Attribute),
                new Endurance(Attribute),
                new KarmaEnergy(),
                new KarmaEnergyNeutral(),
                new MagicResistance(Attribute),
                new Vitality(Attribute)
            };
            return new ResourceRepository(resourceList);
        }
        protected override ITalentRepository GetNewTalentRepository()
        {
            var file        = System.IO.File.ReadAllText(jsonTalentPath);
            var jsonFile    = JSONTalentSaveFile.DeSerializeJson(file, out string error);

            var talentList  = TalentJsonLoader.LoadTalent(jsonFile.Talente);
            var families = TalentJsonLoader.LoadLanguageFamily(jsonFile.Families, talentList);

            return new SQLTalentRepository(this.applicationContext, this, this.charakterID, talentList, families);
        }
        protected override ITraitRepository GetNewTraitRepository()
        {
            throw new NotImplementedException();
        }
        protected override IValueRepository GetNewValueRepository()
        {
            var list = new List<IValue>()
            {
                new BaseAttack(Attribute),
                new BaseParade(Attribute),
                new BaseBlock(Attribute),
                new BaseRange(Attribute),
                new BaseInitiative(Attribute),
                new ControllValue(),
                new ArtifactControl(Attribute, Resources),
                new WoundSwell(Attribute),
                new Rapture(),
                new SpeedLand(),
                new Repute(),
            };
            return new SQLValueRepository(applicationContext, list);
        }

        protected override IAPRepository GetAPRepository()
        {
            return new SQLAPRepository(applicationContext, charakterID);
        }
    }
}
