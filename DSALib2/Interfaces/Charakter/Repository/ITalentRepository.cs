using DSALib2.Classes.Charakter.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface ITalentRepository
    {
        List<TalentView> GetViewList<T>() where T : ITalent;
    }
}
