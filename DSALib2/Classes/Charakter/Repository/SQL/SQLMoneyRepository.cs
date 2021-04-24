using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLMoneyRepository : GeneralMoneyRepository
    {
        InnerSQLRepository repo;
        public SQLMoneyRepository(ApplicationContext context, int charakterID)
        {
            repo = new InnerSQLRepository(context, charakterID);
        }

        public override MoneyView GetView()
        {
            var tabel = this.repo.Get();
            return new MoneyView()
            {
                BankDublonen = tabel.BankDublonen,
                Dublonen = tabel.Dublonen,
                Heller = tabel.Heller,
                Kupfer = tabel.Kupfer,
                Silber = tabel.Silber
            };
        }
        public override void SetByView(MoneyView view)
        {
            var current = this.repo.Get();
            current.Heller = view.Heller;
            current.Kupfer = view.Kupfer;
            current.Silber = view.Silber;
            current.Dublonen = view.Dublonen;
            current.BankDublonen = view.BankDublonen;

            this.repo.Update(current);
            repo.Submit();
        }
        public void Delete()
        {
            repo.Delete();
        }
        private class InnerSQLRepository : BaseCharakterRepository<T_Money>
        {
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context, charakterID) { }

            protected override T_Money CreateNewEntry()
            {
                var tabel = new T_Money()
                {
                    CharakterID = charakterID,
                    BankDublonen = 0,
                    Dublonen = 0,
                    Heller = 0,
                    Kupfer = 0,
                    Silber = 0,
                };
                this.Insert(tabel);
                this.Submit();
                return tabel;
            }
            public void Delete()
            {
                var value = this.Get();
                base.Delete(value);
                this.Submit();
            }
        }

    }
}
