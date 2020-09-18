using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLAttributRepository : BaseRepository<t_Attribute>, IAttributeRepository
    {
        private int charakterID; 
        public SQLAttributRepository(ApplicationContext context, int charakterID) : base(context) 
        {
            this.charakterID = charakterID;
        }
  
        public int GetAKT(CharakterAttribut attribut)
        {
            var result = GetAKTInner(attribut);
            if(result != null)
            {
                return result.Value;
            }
            else
            {
                return 0;
            }            
        }

        private t_Attribute GetAKTInner(CharakterAttribut attribut)
        {
            return dbSet.Where(x => x.AttributID == (int)attribut && x.CharakterID == charakterID).FirstOrDefault();
        }

        public int GetMOD(CharakterAttribut attribut)
        {
            return 0;
        }

        public int GetMAX(CharakterAttribut attribut)
        {
            return GetAKT(attribut) + GetMOD(attribut);
        }

        public List<CharakterAttribut> GetAttributList()
        {
            var retList = new List<CharakterAttribut>();
            var list = dbSet.Where(x => x.CharakterID == charakterID);
            foreach(var item in list)
            {
                retList.Add((CharakterAttribut)item.AttributID);
            }
            return retList;
        }
        
        public void SetAKT(CharakterAttribut attribut, int value)
        {
            var akt = GetAKTInner(attribut);
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
                    Value = value
                };
                Insert(akt);
            }
            context.SaveChanges();
        }

        private AttributView GetView(CharakterAttribut item)
        {
            return new AttributView
            {
                ID = item,
                AKT = GetAKT(item),
                MOD = GetMOD(item),
                MAX = GetMAX(item),
                Name = Enum.GetName(typeof(CharakterAttribut), item),
                NameShort = Enum.GetName(typeof(CharakterAttribut), item),
            };
        }
        public List<AttributView> GetViewList()
        {
            var list = GetAttributList();
            var retList = new List<AttributView>();
            foreach(var item in list)
            {
                retList.Add(GetView(item));
            }
            return retList;
        }
    }
}
