using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System;

using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLValueRepository : BaseRepository<t_Values>, IValueRepository
    {
        private List<IValue> valueList;
        public SQLValueRepository(ApplicationContext context, List<IValue> valueList) : base(context)
        {
            this.valueList = valueList ?? throw new ArgumentNullException(nameof(valueList));
        }
        #region Getter
        public int GetAKT(IValue item)
        {
            if (typeof(AbstractSettableValue).IsAssignableFrom(item.GetType()))
            {
                var typeString = item.GetType().Name;
                return 0;
            }
            else
            {
                return item.Value;
            }
        }
        public int GetMOD(IValue item)
        {
            return 0;
        }
        public int GetMAX(IValue item)
        {
            return GetAKT(item) + GetMOD(item);
        }
        public List<IValue> GetList()
        {
            return valueList;
        }
        #endregion
        #region Setter
        public void SetAKT(AbstractSettableValue item, int value)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region View
        private ValueView GetView(IValue item)
        {
            return new ValueView
            {
                AKT = GetAKT(item),
                MOD = GetMOD(item),
                MAX = GetMAX(item),
                Name = item.Name
            };
        }
        public List<ValueView> GetViewList()
        {
            var list = GetList();
            var retList = new List<ValueView>();
            foreach (var item in list)
            {
                retList.Add(GetView(item));
            }
            return retList;
        }
        #endregion
    }
}
