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
        protected TEntity GetTabel()
        {
            var tabel = dbSet.Where(x => x.Id == charakterID).FirstOrDefault();
            if (tabel == null)
            {
                CreateNewEntry();
                tabel = GetTabel();
            }
            return tabel;
        }
        protected abstract void CreateNewEntry();
    }
}
