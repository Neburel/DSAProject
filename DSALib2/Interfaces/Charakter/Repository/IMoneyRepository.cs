using DSALib2.Classes.Charakter.View;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IMoneyRepository
    {
        MoneyView GetView();
        void SetByView(MoneyView view);
    }
}
