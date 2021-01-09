using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.View;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter.Repository
{
    public interface ITraitRepository
    {
        public int GetAttribut(CharakterAttribut attribut);
        public int GetResource(IResource resource);
        public int GetValue(IValue value);
        public int GetTaW(ITalent value);
        public int GetAT(AbstractTalentFighting value);
        public int GetPA(AbstractTalentFighting value);
        public int GetBL(AbstractTalentFighting value);
        public int GetAPEarned();
        public int GetAPInvest();

        public TraitView GetEmptyView();
        public List<TraitView> GetViewList();

        public void SetByView(TraitView view);
    }
}
