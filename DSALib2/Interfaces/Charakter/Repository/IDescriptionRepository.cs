using DSALib2.Classes.Charakter.View;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IDescriptionRepository
    {
        DescriptionView GetView();
        void SetByView(DescriptionView view);
    }
}
