using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralMoneyRepository : IMoneyRepository
    {
        public abstract MoneyView GetView();
        public abstract void SetByView(MoneyView view);
    }
}
