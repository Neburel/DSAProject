using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralValueRepository : IValueRepository
    {
        private List<IValue> valueList;
        private AbstractCharakter abstractCharakter;
        public GeneralValueRepository(AbstractCharakter abstractCharakter, List<IValue> valueList) {
            this.abstractCharakter = abstractCharakter;
            this.valueList = valueList ?? throw new ArgumentNullException(nameof(valueList));
        }

        public int GetAKT(IValue item)
        {
            if (typeof(AbstractSettableValue).IsAssignableFrom(item.GetType()))
            {
                return GetAktSettable((AbstractSettableValue)item);
            }
            else
            {
                return item.Value;
            }
        }
        protected abstract int GetAktSettable(AbstractSettableValue value);
        public int GetMOD(IValue item)
        {
            return this.abstractCharakter.Traits.GetValue(item);
        }
        public int GetMAX(IValue item)
        {
            return GetAKT(item) + GetMOD(item);
        }

        public IValue GetItemByType(Type type)
        {
            var list = GetList();
            return list.Where(x => x.GetType() == type).FirstOrDefault();
        }
        public List<IValue> GetList()
        {
            return valueList;
        }
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

        public abstract void SetAKT(AbstractSettableValue item, int value);
    }
}
