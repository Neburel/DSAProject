using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLCharakterRepository : BaseRepository<t_Charakter>
    {
        public SQLCharakterRepository(ApplicationContext context) : base(context) {
            
        }

        public List<t_Charakter> GetList()
        {
            return dbSet.ToList();
        }

        public void SetbyView(t_Charakter charakter)
        {
            charakter.LastUsed = DateTime.Now; 
            this.Update(charakter);
            Submit();
        }

        public t_Charakter CreateDSACharakter(ApplicationContext context, string charakterName)
        {
            var charakter = new t_Charakter() { Name = charakterName, Created = DateTime.Now, LastUsed = DateTime.Now };
            Insert(charakter);
            context.SaveChanges();

            //Standart Start Werte

            context.SaveChanges();
            return charakter;
        }
    }
}
