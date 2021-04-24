using DSALib2.Classes.Charakter.Resources;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System;

namespace DSALib2.Classes.Charakter.Values.Extended
{
    public class ArtifactControl : IValue
    {
        public int Value { get => Calculate(); }
        public string Name => DSALib2.Resources.ArtifactControl;
        public string ShortName { get => Name; }
        public string InfoText => "IN + MU + MR";

        private IAttributeRepository attributeRepository;
        private IResourcesRepository resourcesRepository;
        private MagicResistance artifactControl;
        public ArtifactControl(IAttributeRepository attributeRepository, IResourcesRepository resourcesRepository)
        {
            this.attributeRepository = attributeRepository;
            this.resourcesRepository = resourcesRepository;
            artifactControl = (MagicResistance)resourcesRepository.GetByType(typeof(MagicResistance));
            if (artifactControl == null) throw new ArgumentNullException();
        }   
        protected int Calculate()
        {
            var initiative      = attributeRepository.GetMAX(Utils.CharakterAttribut.Intuition);
            var mut             = attributeRepository.GetMAX(Utils.CharakterAttribut.Mut);
            var mr              = resourcesRepository.GetMAX(artifactControl);
            return initiative + mut + mr;
        }
    }
}
