using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLAPRepository : BaseCharakterRepository<t_AP>, IAPRepository
    {
        public SQLAPRepository(ApplicationContext context, int charakterID) : base(context, charakterID){}
        #region Getter
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
        public int GetAPEarnedAKT()
        {
            var tabel = dbSet.Where(x => x.CharakterID == this.charakterID).FirstOrDefault();
            if (tabel == null) return 0;
            else return tabel.AP;
        }
        public int GetAPEarnedMOD()
        {
            return 0;
        }
        public int GetAPEarnedMAX()
        {
            return GetAPEarnedAKT() + GetAPEarnedMOD();
        }
        public int GetAPInvestedAKT()
        {
            var tabel = dbSet.Where(x => x.CharakterID == this.charakterID).FirstOrDefault();
            if (tabel == null) return 0;
            else return tabel.APInvested;
        }
        public int GetAPInvestedMOD()
        {
            return 0;
        }
        public int GetAPInvestedMAX()
        {
            return GetAPInvestedAKT() + GetAPInvestedMOD();
        }
        public APView GetView()
        {
            var ret = new APView()
            {
                AP = GetAPEarnedMAX(),
                APInvested = GetAPInvestedMAX(),
                Level = GetLevel()
            };
            ret.APLeft = ret.AP - ret.APInvested;
            return ret;
        }
        #endregion
        #region Setter
        public void SetAPEarned(int value)
        {
            var tabel = GetTabel();
            tabel.AP = value;
            Submit();
        }
        public void SetAPInvested(int value)
        {
            var tabel = GetTabel();
            tabel.APInvested = value;
            Submit();
        }       
        #endregion
        #region Helper Funktions
        protected override void CreateNewEntry()
        {
            var tabel = new t_AP()
            {
                AP = 0,
                APInvested = 0,
                CharakterID = charakterID
            };
            this.Insert(tabel);
            this.Submit();
        }
        #endregion
    }
}
