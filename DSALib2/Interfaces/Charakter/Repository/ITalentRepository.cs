using DSALib2.Classes.Charakter.View;
using System;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface ITalentRepository
    {
        TalentView GetView(Guid guid);
        LanguageView GetLanguageView(Guid guidLanguage, Guid guidWriting);
        List<TalentView> GetViewList<T>() where T : ITalent;
        List<LanguageView> GetViewList();

        void SetTalentbyView(TalentView view);
        void SetTalentbyView(LanguageView view);
    }
}
