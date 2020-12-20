using DSALib2.Charakter.Talente;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLTalentRepository : ITalentRepository
    {
        private List<ITalent> talentList;
        private List<LanguageFamily> familieList;

        public SQLTalentRepository(List<ITalent> list, List<LanguageFamily> familieList)
        {
            this.talentList = list;
            this.familieList = familieList;
        }

        public List<TalentView> GetViewList<T>() where T : ITalent
        {
            var ret     = new List<TalentView>();
            var list    = talentList.Where(x => x.GetType() == typeof(T)).ToList();
            foreach(var item in list)
            {
                ret.Add(new TalentView()
                {

                });
            }
            return ret;
        }
    }
}
