using DSALib2.Interfaces.Charakter.Repository;

namespace DSALib2.Classes.Charakter
{
    public abstract class AbstractCharakter
    {
        private IAttributeRepository attribute;
        private IResourcesRepository resources;
        private ITalentRepository talente;
        private ITraitRepository traits;
        private IValueRepository values;

        public IAttributeRepository Attribute { get { if (attribute == null) { attribute = GetNewAttributeRepository(); } return attribute; } private set => attribute = value; }
        public IResourcesRepository Resources { get { if (resources == null) { resources = GetNewResourcesRepository(); } return resources; } private set => resources = value; }
        public ITalentRepository Talente { get { if (talente == null) { talente = GetNewTalentRepository(); } return talente; } private set => talente = value; }
        public ITraitRepository Traits { get { if (traits == null) { traits = GetNewTraitRepository(); } return traits; } private set => traits = value; }
        public IValueRepository Values { get { if (values == null) { values = GetNewValueRepository(); } return values; } private set => values = value; }

        protected abstract IAttributeRepository GetNewAttributeRepository();
        protected abstract IResourcesRepository GetNewResourcesRepository();
        protected abstract ITalentRepository GetNewTalentRepository();
        protected abstract ITraitRepository GetNewTraitRepository();
        protected abstract IValueRepository GetNewValueRepository();
    }
}
