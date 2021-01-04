
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralAttributRepository : IAttributeRepository
    {
        private AbstractCharakter charakter;
        public GeneralAttributRepository(AbstractCharakter charakter)
        {
            this.charakter = charakter;
        }

        public abstract int GetAKT(CharakterAttribut attribut);

        public int GetMOD(CharakterAttribut attribut)
        {
            return charakter.Traits.GetAttribut(attribut);
        }
        public int GetMAX(CharakterAttribut attribut)
        {
            return GetAKT(attribut) + GetMOD(attribut);
        }
        public abstract List<CharakterAttribut> GetAttributList();
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
            foreach (var item in list)
            {
                retList.Add(GetView(item));
            }
            return retList;
        }

        public abstract void SetAKT(CharakterAttribut attribut, int value);
    }
}
