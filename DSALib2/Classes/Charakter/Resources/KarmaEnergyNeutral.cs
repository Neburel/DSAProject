using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Resources
{
    class KarmaEnergyNeutral : IResource
    {
        public string Name => DSALib2.Resources.KarmaEnergyNeutral;
        public string ShortName => DSALib2.Resources.KarmaEnergyNeutralShort;
        public int Value => 0;
        public string InfoText => "";
    }
}
