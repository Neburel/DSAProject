using Microsoft.EntityFrameworkCore;

namespace DSALib2.SQLDataBase 
{
    public class ApplicationContext : DbContext
    {
        public DbSet<t_Attribute> t_Attribute { get; set; }
        public DbSet<t_Charakter> t_Charakter { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }

    public class t_Attribute
    {
        public int Id { get; set; }
        public int AttributID { get; set; }
        public int CharakterID { get; set; }
        public int Value { get; set; }
    }

    public class t_Charakter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
