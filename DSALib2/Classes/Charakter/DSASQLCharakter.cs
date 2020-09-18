using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Repository.SQL;
using DSALib2.Classes.Charakter.Resources;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.SQLDataBase;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter
{
    public class DSASQLCharakter : AbstractCharakter
    {
        private int charakterID;
        private ApplicationContext applicationContext;

        public DSASQLCharakter(ApplicationContext applicationContext, int charakterID)
        {
            this.charakterID = charakterID;
            this.applicationContext = applicationContext;
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
            throw new NotImplementedException();
        }

        protected override ITraitRepository GetNewTraitRepository()
        {
            throw new NotImplementedException();
        }

        protected override IValueRepository GetNewValueRepository()
        {
            throw new NotImplementedException();
        }

        public static t_Charakter CreateDSACharakter(ApplicationContext context, string charakterName)
        {
            var charakterRepo   = new SQLCharakterRepository(context);

            var charakter       = new t_Charakter() { Name = charakterName };
            charakterRepo.Insert(charakter);
            context.SaveChanges();

            var attributRepo = new SQLAttributRepository(context, charakter.Id);

            foreach (var attribut in DSAUtil.getDSAAttributList())
            {
                var newAttribut = new t_Attribute()
                {
                    CharakterID = charakter.Id,
                    AttributID = (int)attribut,
                    Value = 0
                };
                attributRepo.Insert(newAttribut);
            }
            context.SaveChanges();
            return charakter;
        }
    }
}
