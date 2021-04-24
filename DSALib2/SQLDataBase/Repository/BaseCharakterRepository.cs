using System.Linq;

namespace DSALib2.SQLDataBase.Repository
{
    public abstract class BaseCharakterRepository<TEntity> : BaseRepository<TEntity>  where TEntity : BaseTabelCharakter
    {
        protected int charakterID;
        public BaseCharakterRepository(ApplicationContext context, int charakterID) : base(context)
        {
            this.charakterID = charakterID;
        }
        public TEntity Get()
        {
            var tabel = dbSet.Where(x => x.CharakterID == charakterID).FirstOrDefault();
            if (tabel == null)
            {
                CreateNewEntry();
                tabel = Get();
            }
            return tabel;
        }
        public void DeleteAll()
        {
            var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
            foreach (var item in talentList)
            {
                this.Delete(item);
            }
            this.Submit();
        }
        protected abstract TEntity CreateNewEntry();
    }
}
