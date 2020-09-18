using DSALib2.Classes.Charakter.View;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IAttributeRepository
    {
        int GetAKT(CharakterAttribut attribut);
        int GetMOD(CharakterAttribut attribut);
        int GetMAX(CharakterAttribut attribut);

        void SetAKT(CharakterAttribut attribut, int value);

        List<AttributView> GetViewList();
    }
}
