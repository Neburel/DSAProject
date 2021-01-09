using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public class GeneralResourceRepository : IResourcesRepository
    {
        private AbstractCharakter charakter;
        private List<IResource> resourceList;
        public GeneralResourceRepository(AbstractCharakter charakter, List<IResource> valueList)
        {
            if (valueList == null) throw new ArgumentNullException(nameof(valueList));
            resourceList = valueList;
            this.charakter = charakter;
        }
        #region Getter
        public int GetAKT(IResource value)
        {
            return value.Value;
        }
        public int GetMOD(IResource value)
        {
            return this.charakter.Traits.GetResource(value);
        }
        public int GetMAX(IResource value)
        {
            return GetAKT(value) + GetMOD(value);
        }
        public List<IResource> GetList()
        {
            return resourceList;
        }
        public IResource GetByType(Type type)
        {
            return this.GetList().Where(x => x.GetType() == type).FirstOrDefault();
        }
        #endregion
        #region View
        private ResourceView GetView(IResource item)
        {
            return new ResourceView
            {
                AKT = GetAKT(item),
                MOD = GetMOD(item),
                MAX = GetMAX(item),
                Name = item.Name
            };
        }
        public List<ResourceView> GetViewList()
        {
            var list = GetList();
            var retList = new List<ResourceView>();
            foreach (var item in list)
            {
                retList.Add(GetView(item));
            }
            return retList;
        }
        #endregion
    }
}
