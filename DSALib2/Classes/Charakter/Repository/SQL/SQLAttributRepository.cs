using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using DSALib2.Utils;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLAttributRepository : GeneralAttributRepository
    {
        private InnerSQLRepository repo;

        public SQLAttributRepository(ApplicationContext context, AbstractCharakter charakter, int charakterID) : base(charakter) 
        {
            repo = new InnerSQLRepository(context, charakterID);
        }
  
        public override int GetAKT(CharakterAttribut attribut)
        {
            var result = this.repo.Get(attribut);
            if(result != null)
            {
                return result.Value;
            }
            else
            {
                return 0;
            }            
        }
        public override List<CharakterAttribut> GetAttributList()
        {
            var retList = new List<CharakterAttribut>();
            foreach(var item in repo.GetList())
            {
                retList.Add((CharakterAttribut)item.AttributID);
            }
            return retList;
        }

        public override void SetAKT(CharakterAttribut attribut, int value)
        {
            repo.Set(attribut, value);    
        }

        private class InnerSQLRepository : BaseRepository<t_Attribute>
        {
            private int charakterID;
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }

            public t_Attribute Get(CharakterAttribut item)
            {
                return dbSet.Where(x => x.AttributID == (int)item && x.CharakterID == charakterID).FirstOrDefault();
            }
            public List<t_Attribute> GetList()
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID);
                return list.ToList();
            }
            public void Set(CharakterAttribut item, int value)
            {
                var akt = Get(item);
                if (akt != null)
                {
                    akt.Value = value;
                    Update(akt);
                }
                else
                {
                    akt = new t_Attribute()
                    {
                        CharakterID = charakterID,
                        AttributID = (int)item,                      
                        Value = value
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
            
        }
    }
}
