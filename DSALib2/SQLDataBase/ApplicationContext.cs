using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Data.SqlClient;
using System.IO;

namespace DSALib2.SQLDataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<t_Attribute> t_Attribute { get; set; }
        public DbSet<t_Charakter> t_Charakter { get; set; }
        public DbSet<t_Values> t_Values { get; set; }
        public DbSet<t_AP> t_AP { get; set; }
        public DbSet<T_Talente> T_Talente { get; set; }

        public DbSet<T_Trait> T_Trait { get; set; }
        public DbSet<T_TraitAttribute> T_TraitAttribute { get; set; }
        public DbSet<T_TraitResources> T_TraitResources { get; set; }
        public DbSet<T_TraitTalente> T_TraitTalente { get; set; }
        public DbSet<T_TraitValues> T_TraitValues { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Ersetzung druch Konstruct Electron Databas nötig
            //Ohne kommt es zu verschiedenen möglichen Fehler
            //->Datenbank schließt sich für alle Request
            //->Electron Fehler: Assembly Error (siehe Startup)

            var extension           = optionsBuilder.Options.GetExtension<SqlServerOptionsExtension>();
            var oldConnectionString = extension.Connection.ConnectionString;
            var newSQLConnection    = new SqlConnection(oldConnectionString);
                      
            optionsBuilder.UseSqlServer(newSQLConnection);
            
            base.OnConfiguring(optionsBuilder);
        }

        private void WorkinVersion1(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = "C:\\Users\\chris\\AppData\\Roaming\\Electron";
            var dbName = "DSADatabaseTemplate.mdf";
            var newDB = Path.Combine(dbPath, dbName);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(localdb)\\MSSQLLocalDB";
            builder.InitialCatalog = dbName;
            builder.IntegratedSecurity = true;
            builder.AttachDBFilename = newDB;
            builder.MultipleActiveResultSets = true;
            var connectionString = new SqlConnection(builder.ConnectionString);
            optionsBuilder.UseSqlServer(connectionString);

            Console.WriteLine(connectionString.ConnectionString);
        }
    }

    public abstract class BaseTabel
    {
        public int Id { get; set; }
    }
    public abstract class BaseTabelCharakter : BaseTabel
    {
        public int CharakterID { get; set; }
    }
    public abstract class BaseTabelTrait : BaseTabelCharakter
    {
        public int TraitID { get; set; }
    }
    public class t_Attribute : BaseTabelCharakter
    {
        public int AttributID { get; set; }
        public int Value { get; set; }
    }

    public class t_Charakter : BaseTabel
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUsed { get; set; }
    }
    public class t_Values : BaseTabelCharakter
    {
        public int Value { get; set; }
        public string Type { get; set; }
    }
    public class t_AP : BaseTabelCharakter
    {
        public int AP { get; set; }
        public int APInvested { get; set; }
    }
    public class T_Talente : BaseTabelCharakter
    {
        new public string Id { get; set; }
        public int TAW { get; set; }
        public int? AT { get; set; }
        public int? PA { get; set; }
        public int? BL { get; set; }
        public string DeductionID { get; set; }
        public bool? Mother { get; set; }
    }

    public class T_Trait : BaseTabelCharakter
    {
        public int APGain { get; set; }
        public int APInvested { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string GP { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
    public class T_TraitAttribute : BaseTabelTrait
    {
        public int AttributID { get; set; }
        public int Value { get; set; }
    }
    public class T_TraitResources : BaseTabelTrait
    {
        public int Value { get; set; }
        public string Type { get; set; }
    }
    public class T_TraitValues : BaseTabelTrait
    {
        public int Value { get; set; }
        public string Type { get; set; }
    }
    public class T_TraitTalente : BaseTabelTrait
    {
        public string TalentID { get; set; }
        public int TAW { get; set; }
        public int? AT { get; set; }
        public int? PA { get; set; }
        public int? BL { get; set; }
    }
}
