using DSALib2.Charakter.Talente;
using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentLanguage;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    public class SQLTalentRepository : GeneralTalentRepository
    {
        private readonly InnerSQLTalentRepository talentRepository;
        public SQLTalentRepository(ApplicationContext context, AbstractCharakter charakter, int charakterID, List<ITalent> list, List<LanguageFamily> familieList) : base(charakter, list, familieList) {
            talentRepository = new InnerSQLTalentRepository(context, charakterID);
        }
        
        protected override int GetTAW(ITalent talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.TAW;
            else return 0;
        }
        protected override int GetAT(AbstractTalentFighting talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.AT != null ? (int)sql.AT : 0;
            else return 0;
        }
        protected override int GetPA(AbstractTalentFighting talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.PA != null ? (int)sql.PA : 0;
            else return 0;
        }
        protected override int GetBL(AbstractTalentFighting talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.BL != null ? (int)sql.BL : 0;
            else return 0;
        }
        protected override int GetDeduction(ITalent talent)
        {
            return this.talentRepository.GetDeduction(talent);
        }
        protected override Guid? GetDeductionID(ITalent talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.DeductionID != null ? new Guid(sql.DeductionID) : null;
            return null;
        }
        protected override bool? GetMother(TalentSpeaking talent)
        {
            var sql = this.talentRepository.Get(talent);
            if (sql != null) return sql.Mother;
            return null;
        }
        public override void SetbyView(TalentView view)
        {
            var talent = this.Get(view.ID);
            var taw = view.TAW - this.GetModTaW(talent);

            if (typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
            {
                var fightingTalent = (AbstractTalentFighting)talent;
                this.talentRepository.SetTalent(
                view.ID, 
                taw,
                view.DeductionSelected?.ID,
                view.AT - GetModAT(fightingTalent),
                view.PA - GetModPA(fightingTalent),
                view.BL - GetModBL(fightingTalent));
            }
            else
            {
                this.talentRepository.SetTalent(
                    view.ID,
                    taw,
                    view.DeductionSelected?.ID);
            }
        }
        public override void SetbyView(LanguageView view)
        {
            this.talentRepository.SetTalent(view.IDSprache, view.TawSprache, null, null, null, null, view.Mother);
            this.talentRepository.SetTalent(view.IDSchrift, view.TawSchrift, null, null, null, null, null);
        }
        public void DeleteAll()
        {
            this.talentRepository.DeleteAll();
        }

        private class InnerSQLTalentRepository : BaseRepository<T_Talente>
        {
            private readonly int charakterID;
            public InnerSQLTalentRepository(ApplicationContext context, int charakterID) : base(context){ this.charakterID = charakterID; }
            internal T_Talente Get(ITalent talent)
            {
                return dbSet.Where(x => x.TalentID == talent.ID.ToString() && x.CharakterID == charakterID).FirstOrDefault();
            }
            internal int GetDeduction(ITalent talent)
            {
                var list = dbSet.Where(x => x.DeductionID == talent.ID.ToString() && x.CharakterID == charakterID).ToList();
                return list.Count;
            }
            internal void SetTalent(Guid talentID, int tawValue, Guid? deductionID = null, int? atValue = null, int? paValue = null, int? blValue = null, bool? mother = null)
            {
                var tabel = GetTabel(talentID);
                tabel.TAW = tawValue;
                tabel.AT = atValue;
                tabel.PA = paValue;
                tabel.BL = blValue;
                tabel.DeductionID = deductionID?.ToString();
                tabel.Mother = mother;
                Submit();
            }
            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach(var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
            #region Helper Funktions
            protected T_Talente GetTabel(Guid talentID)
            {
                var tabel = dbSet.Where(x => x.TalentID == talentID.ToString() && x.CharakterID == charakterID).FirstOrDefault();
                if (tabel == null)
                {
                    CreateNewEntry(talentID);
                    tabel = GetTabel(talentID);
                }
                return tabel;
            }
            protected void CreateNewEntry(Guid talentID)
            {
                var tabel = new T_Talente()
                {
                    TalentID = talentID.ToString(),
                    CharakterID = charakterID,
                    TAW = 0
                };
                this.Insert(tabel);
                this.Submit();
            }
            #endregion
        }
    }
}


