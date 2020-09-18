using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public class ResourceRepository : IResourcesRepository
    {
        private List<IResource> resourceList;
        public ResourceRepository(List<IResource> valueList)
        {
            if (valueList == null) throw new ArgumentNullException(nameof(valueList));
            resourceList = valueList;
        }
        #region Getter
        public int GetAKT(IResource value)
        {
            return 0;
        }
        public int GetMOD(IResource value)
        {
            return 0;
        }
        public int GetMAX(IResource value)
        {
            return GetAKT(value) + GetMOD(value);
        }
        public List<IResource> GetList()
        {
            return resourceList;
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
