using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralAPRepository : IAPRepository
    {
        protected AbstractCharakter charakter;
        public GeneralAPRepository(AbstractCharakter charakter)
        {
            this.charakter = charakter;
        }

        public abstract int GetAPEarnedAKT();
        public abstract int GetAPInvestAKT();
        public int GetAPEarnedMOD()
        {
            return this.charakter.Traits.GetAPEarned();
        }
        public int GetAPInvestMOD()
        {
            return this.charakter.Traits.GetAPInvest();
        }
        public int GetAPEarnedMAX()
        {
            return GetAPEarnedAKT() + GetAPEarnedMOD();
        }
        public int GetAPInvestedMAX()
        {
            return GetAPInvestAKT() + GetAPInvestMOD();
        }

        public int GetLevel()
        {
            var maxAP = GetAPEarnedMAX();
            int result;
            if (maxAP > 1000)
            {
                result = maxAP / 1000 + 4;
            }
            else
            {
                result = maxAP / 200;
            }
            return 1 + result;
        }

        public APView GetView()
        {
            var aPInvested = this.GetAPInvestedMAX();
            var apInvestedHand = this.GetAPInvestAKT();
            var apInvestedTrait = this.GetAPInvestMOD();
            var apGain = this.GetAPEarnedMAX();
            var apGainHand = this.GetAPEarnedAKT();
            var apGainTrait = this.GetAPEarnedMOD();

            return new APView()
            {
                APInvested = aPInvested,
                APInvestHand = apInvestedHand,
                APInvestTrait = apInvestedTrait,
                APGain = apGain,
                APGainHand = apGainHand,
                APGainTrait = apGainTrait,
                APLeft = apGain - aPInvested,
                Level = this.GetLevel()
            };
        }
        public abstract void SetbyView(APView view);

    }
}
