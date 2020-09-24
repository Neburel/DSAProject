using DSALib2.Classes.Charakter.View;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IAPRepository
    {
        int GetLevel();
        int GetAPEarnedAKT();
        int GetAPEarnedMOD();
        int GetAPEarnedMAX();
        int GetAPInvestedAKT();
        int GetAPInvestedMOD();
        int GetAPInvestedMAX();
        void SetAPEarned(int value);
        void SetAPInvested(int value);
        APView GetView();
    }
}
