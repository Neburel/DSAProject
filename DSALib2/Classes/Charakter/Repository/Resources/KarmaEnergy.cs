using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Resources
{
    class KarmaEnergy : IResource
    {
        public string Name => DSALib2.Resources.KarmaEnergy;
        public string ShortName => DSALib2.Resources.KarmaEnergyShort;
        public int Value => 0;
        public string InfoText => "";
    }
}
