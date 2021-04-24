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
            return new SQLAttributRepository(applicationContext, this, charakterID);
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
            return new GeneralResourceRepository(this, resourceList);
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
            return new SQLTraitRepository(applicationContext, this, this.charakterID);
        }
        protected override IValueRepository GetNewValueRepository()
        {
            var deathThresholdTime = new DeathThresholdTime(Attribute);
            var list = new List<IValue>()
            {
                new BaseAttack(Attribute),
                new BaseParade(Attribute),
                new BaseBlock(Attribute),
                new BaseRange(Attribute),
                new BaseInitiative(Attribute),
                deathThresholdTime,
                new ControllValue(),
                new ArtifactControl(Attribute, Resources),
                new WoundSwell(Attribute),
                new Rapture(),
                new SpeedLand(),
                new Repute()
            };
            var values = new SQLValueRepository(applicationContext, this, list, charakterID);
            deathThresholdTime.SetValue(values);
            return values;
        }

        protected override IAPRepository GetAPRepository()
        {
            return new SQLAPRepository(applicationContext, this, charakterID);
        }

        protected override IDescriptionRepository GetDescriptionRepository()
        {
            return new SQLDescriptionRepository(applicationContext, this, charakterID);
        }

        protected override IMoneyRepository GetMoneyRepository()
        {
            return new SQLMoneyRepository(applicationContext, charakterID);
        }

        public override void Delete()
        {
            ((SQLMoneyRepository)this.Money).Delete();
            ((SQLAPRepository)this.AP).Delete();
            ((SQLDescriptionRepository)this.Description).Delete();
            ((SQLAttributRepository)this.Attribute).DeleteAll();
            ((SQLTalentRepository) this.Talente).DeleteAll();
            ((SQLTraitRepository)this.Traits).DeleteAll();

            var repo = new SQLCharakterRepository(this.applicationContext);
            repo.Delete(this.charakterID);
            repo.Submit();
        }
    }
}
