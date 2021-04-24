using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLAPRepository : GeneralAPRepository
    {
        private InnerSQLRepository repo;
        public SQLAPRepository(ApplicationContext context, AbstractCharakter charakter, int charakterID) : base(charakter)
        {
            repo = new InnerSQLRepository(context, charakterID);
        }

        public override int GetAPEarnedAKT()
        {
            return this.repo.Get().AP;
        }
        public override int GetAPInvestAKT()
        {
            return this.repo.Get().APInvested;
        }
        public void Delete()
        {
            repo.Delete();
        }

        public override void SetbyView(APView view)
        {
            var tabel = repo.Get();
            tabel.AP = view.APGainHand;
            tabel.APInvested = view.APInvestHand;

            repo.Update(tabel);
            repo.Submit();
        }
        private class InnerSQLRepository : BaseCharakterRepository<t_AP>
        {
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context, charakterID) { }

            protected override t_AP CreateNewEntry()
            {
                var tabel = new t_AP()
                {
                    AP = 0,
                    APInvested = 0,
                    CharakterID = charakterID
                };
                this.Insert(tabel);
                this.Submit();
                return tabel;
            }
            public void Delete()
            {
                var x = this.Get();
                this.Delete(x);
                this.Submit();
            }
        }
    }
}
