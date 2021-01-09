using DSALib2.Classes.Charakter.View;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IAPRepository
    {
        APView GetView();
        void SetbyView(APView view);
    }
}
