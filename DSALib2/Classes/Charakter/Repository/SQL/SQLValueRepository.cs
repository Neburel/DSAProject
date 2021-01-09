using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Interfaces.Charakter;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System;

using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLValueRepository : GeneralValueRepository
    {
        private InnerSQLRepository repo;
        public SQLValueRepository(ApplicationContext context, AbstractCharakter abstractCharakter, List<IValue> valueList, int charakterID) : base(abstractCharakter, valueList)
        {
            repo = new InnerSQLRepository(context, charakterID);
        }

        public override void SetAKT(AbstractSettableValue item, int value)
        {
            this.repo.Set(item.GetType().ToString(), value);
        }

        protected override int GetAktSettable(AbstractSettableValue value)
        {
            var table = this.repo.Get(value.GetType().ToString());
            if (table != null) return table.Value;
            return 0;
        }

        private class InnerSQLRepository : BaseRepository<t_Values>
        {
            private int charakterID;
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }

            public t_Values Get(string type)
            {
                return dbSet.Where(x => x.Type == type && x.CharakterID == charakterID).FirstOrDefault();
            }
            public List<t_Values> GetList()
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID);
                return list.ToList();
            }
            public void Set(string type, int value)
            {
                var akt = Get(type);
                if (akt != null)
                {
                    akt.Value = value;
                    Update(akt);
                }
                else
                {
                    akt = new t_Values()
                    {
                        CharakterID = charakterID,
                        Type = type,
                        Value = value
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
        }
    }
}
