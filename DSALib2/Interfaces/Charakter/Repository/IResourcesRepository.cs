using DSALib2.Classes.Charakter.View;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface IResourcesRepository
    {
        int GetAKT(IResource value);
        int GetMOD(IResource value);
        int GetMAX(IResource value);
        List<IResource> GetList();
        List<ResourceView> GetViewList();
    }
}
