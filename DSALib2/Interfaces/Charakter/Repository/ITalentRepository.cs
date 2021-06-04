using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.View;
using System;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface ITalentRepository
    {
        TalentView GetView(Guid guid);
        LanguageView GetLanguageView(Guid guidLanguage, Guid guidWriting);
        ITalent Get(Guid guid);
        List<ITalent> GetList<T>() where T : ITalent;
        List<TalentView> GetViewList<T>() where T : ITalent;
        List<LanguageView> GetViewList();

        int GetTAW(ITalent talent);
        int GetAT(AbstractTalentFighting talent);
        int GetPA(AbstractTalentFighting talent);
        int GetBL(AbstractTalentFighting talent);

        void SetbyView(TalentView view);
        void SetbyView(LanguageView view);
    }
}
