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

        public t_Charakter CreateDSACharakter(ApplicationContext context, string charakterName)
        {
            var charakter = new t_Charakter() { Name = charakterName, created = DateTime.Now };
            Insert(charakter);
            context.SaveChanges();

            var attributRepo = new SQLAttributRepository(context, charakter.Id);

            var attributList = new List<CharakterAttribut>
            {
                CharakterAttribut.Mut,
                CharakterAttribut.Klugheit,
                CharakterAttribut.Intuition,
                CharakterAttribut.Charisma,
                CharakterAttribut.Fingerfertigkeit,
                CharakterAttribut.Gewandheit,
                CharakterAttribut.Konstitution,
                CharakterAttribut.Körperkraft,
                CharakterAttribut.Sozialstatus
            };
            foreach (var attribut in attributList)
            {
                var newAttribut = new t_Attribute()
                {
                    CharakterID = charakter.Id,
                    AttributID = (int)attribut,
                    Value = 0
                };
                attributRepo.Insert(newAttribut);
            }
            context.SaveChanges();
            return charakter;
        }
    }
}
