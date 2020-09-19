﻿using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace DSALib2.SQLDataBase 
{
    public class ApplicationContext : DbContext
    {
        public DbSet<t_Attribute> t_Attribute { get; set; }
        public DbSet<t_Charakter> t_Charakter { get; set; }
        public DbSet<t_Values> t_Values { get; set; }

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
        public DateTime created { get; set; }
    }
    public class t_Values
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int CharakterID { get; set; }
        public string Type { get; set; }
    }
}
