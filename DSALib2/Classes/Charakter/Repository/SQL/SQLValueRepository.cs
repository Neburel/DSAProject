using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Interfaces.Charakter;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System;

using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        protected override int GetAktSettable(AbstractSettableValue value)
        {
            return 0;
        }

        private class InnerSQLRepository : BaseRepository<t_Values>
        {
            private int charakterID;
            public InnerSQLRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }
        }
    }
}
