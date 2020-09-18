using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLCharakterRepository : BaseRepository<t_Charakter>
    {
        public SQLCharakterRepository(ApplicationContext context) : base(context) {}
    }
}
