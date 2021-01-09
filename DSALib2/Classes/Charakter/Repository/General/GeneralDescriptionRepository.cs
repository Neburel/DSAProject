using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralDescriptionRepository : IDescriptionRepository
    {
        public abstract DescriptionView GetView();
        public abstract void SetByView(DescriptionView view);
    }
}
