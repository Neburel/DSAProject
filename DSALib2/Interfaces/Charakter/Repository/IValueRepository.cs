using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Classes.Charakter.View;
using System;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IValueRepository
    {
        int GetAKT(IValue item);
        int GetMOD(IValue item);
        int GetMAX(IValue item);

        void SetAKT(AbstractSettableValue item, int value);

        IValue GetItemByType(Type type);
        List<ValueView> GetViewList();
    }
}
