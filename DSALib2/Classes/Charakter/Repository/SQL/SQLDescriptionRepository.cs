using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLDescriptionRepository : GeneralDescriptionRepository
    {
        private int charakterID;
        private InnerSQLRepository repo;
        private SQLCharakterRepository charakterRepo;
        public SQLDescriptionRepository(ApplicationContext context, AbstractCharakter charakter, int charakterID) 
        {
            this.charakterID = charakterID;
            repo = new InnerSQLRepository(context, charakterID);
            charakterRepo = new SQLCharakterRepository(context);
        }

        public override DescriptionView GetView()
        {
            var tabel = repo.Get();
            return new DescriptionView()
            {
                Alter = tabel.Alter,
                Anrede = tabel.Anrede,
                Augenfarbe = tabel.Augenfarbe,
                Familienstatus = tabel.FamilienstatusID,
                Geburstag = tabel.Geburstag,
                Geschlecht = tabel.GeschlechtID,
                Gewicht = tabel.Gewicht,
                Glaube = tabel.Glaube,
                Groesse = tabel.Groesse,
                Haarfarbe = tabel.Haarfarbe,
                Hautfarbe = tabel.Hautfarbe,
                Kultur = tabel.Kultur,
                Name = tabel.Name,
                Profession = tabel.Profession,
                Rasse = tabel.Rasse
            };
        }

        public override void SetByView(DescriptionView view)
        {
            var tabel = repo.Get();
            tabel.Alter = view.Alter;
            tabel.Anrede = view.Anrede;
            tabel.Augenfarbe = view.Augenfarbe;
            tabel.FamilienstatusID = view.Familienstatus;
            tabel.Geburstag = view.Geburstag;
            tabel.GeschlechtID = view.Geschlecht;
            tabel.Gewicht = view.Gewicht;
            tabel.Glaube = view.Glaube;
            tabel.Groesse = view.Groesse;
            tabel.Haarfarbe = view.Haarfarbe;
            tabel.Hautfarbe = view.Hautfarbe;
            tabel.Kultur = view.Kultur;
            tabel.Name = view.Name;
            tabel.Profession = view.Profession;
            tabel.Rasse = view.Rasse;
            repo.Update(tabel);
            repo.Submit();

            var current = charakterRepo.GetByID(this.charakterID);
            current.Name = tabel.Name;
        }

        private class InnerSQLRepository : BaseCharakterRepository<T_Descriptions>
        {
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context, charakterID) { }

            protected override void CreateNewEntry()
            {
                var tabel = new T_Descriptions()
                {
                    CharakterID = charakterID
                };
                this.Insert(tabel);
                this.Submit();
            }
        }
    }
}
