using DSALib2.Classes.Charakter.Repository.General;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter;
using DSALib2.SQLDataBase;
using DSALib2.SQLDataBase.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter.Repository.SQL
{
    class SQLTraitRepository : GeneralTraitRepository
    {
        private AbstractCharakter charakter;

        private InnerSQLTraitRepository traitRepository;
        private SQLAttributRepository attributRepository;
        private SQLResourceRepository resourceRepository;
        private SQLValueRepository valueRepository;
        private SQLTalentRepository talentRepository;

        public SQLTraitRepository(ApplicationContext context, AbstractCharakter charakter, int charakterID) : base(charakter) { 
            traitRepository = new InnerSQLTraitRepository(context, charakterID);
            attributRepository = new SQLAttributRepository(context, charakterID);
            resourceRepository = new SQLResourceRepository(context, charakterID);
            valueRepository = new SQLValueRepository(context, charakterID);
            talentRepository = new SQLTalentRepository(context, charakterID);
            this.charakter = charakter;
        }

        public override int GetAttribut(CharakterAttribut attribut)
        {
            return this.attributRepository.GetTotal(attribut);
        }
        public override int GetValue(IValue value)
        {
            return this.valueRepository.GetTotal(value.ToString());
        }
        public override int GetResource(IResource resource)
        {
            return this.resourceRepository.GetTotal(resource.ToString());
        }
        public override int GetTaW(ITalent value)
        {
            return this.talentRepository.GetTotalTAW(value.ID.ToString());
        }
        public override int GetAT(AbstractTalentFighting value)
        {
            return this.talentRepository.GetTotalAT(value.ID.ToString());
        }
        public override int GetPA(AbstractTalentFighting value)
        {
            return this.talentRepository.GetTotalPA(value.ID.ToString());
        }
        public override int GetBL(AbstractTalentFighting value)
        {
            return this.talentRepository.GetTotalBL(value.ID.ToString());
        }

        private TraitView GetView(T_Trait trait)
        {
            var attributList = new List<IDValueView<CharakterAttribut>>();
            var sqlAttribut = attributRepository.GetList(trait.Id);
            foreach(var item in sqlAttribut)
            {
                attributList.Add(new IDValueView<CharakterAttribut>
                {
                    ID = (CharakterAttribut)item.AttributID,
                    Value = item.Value,
                    Name = Enum.GetName(typeof(CharakterAttribut), item.AttributID),
                });
            }

            var resourceList = new List<IDValueView<string>>();
            var sqlresource = resourceRepository.GetList(trait.Id);
            foreach (var item in sqlresource)
            {
                var innerType = DSAUtil.GetType(item.Type);
                var innerItem = this.charakter.Resources.GetByType(innerType);

                resourceList.Add(new IDValueView<string>
                {
                    ID = item.Type,
                    Value = item.Value,
                    Name = innerItem.Name
                });
            }

            var valueList = new List<IDValueView<string>>();
            var sqlvalue = valueRepository.GetList(trait.Id);
            foreach (var item in sqlvalue)
            {
                var innerType = DSAUtil.GetType(item.Type);
                var innerItem = this.charakter.Values.GetItemByType(innerType);

                valueList.Add(new IDValueView<string>
                {
                    ID = item.Type,
                    Value = item.Value,
                    Name = innerItem.Name
                });
            }

            var talentList = new List<TalentView>();
            var sqltalentList = talentRepository.GetList(trait.Id);
            foreach (var item in sqltalentList)
            {
                var talentGuid = new Guid(item.TalentID);
                var talent = charakter.Talente.Get(talentGuid);
                talentList.Add(new TalentView()
                {
                    ID = talentGuid,
                    AT = item.AT,
                    PA = item.PA,
                    BL = item.BL,
                    TAW = item.TAW,
                    Name = talent.Name
                });
            }


            var view = new TraitView
            {
                ID = trait.Id,
                APGain = trait.APGain,
                APInvest = trait.APInvested,
                Description = trait.Description,
                GP = trait.GP,
                Name = trait.Name,
                Type = (TraitType)trait.Type,
                Value = trait.Value,

                ModifyDate = trait.ModifyDate,
                CreationDate = trait.CreationDate,                               

                AttributList = attributList,
                ResourceList = resourceList,
                TalentList = talentList,
                ValueList = valueList
            };
            view.LongDescription = this.GenerateLongDescription(view);
            return view;
        }
        public override List<TraitView> GetViewList()
        {
            var sqlTraitList = traitRepository.GetList();
            var retList = new List<TraitView>();
            foreach(var item in sqlTraitList)
            {
                retList.Add(GetView(item));
            };
            return retList;
        }

        public override void SetByView(TraitView view)
        {
            var trait = traitRepository.Get(view.ID);

            trait.Name = view.Name;
            trait.Description = view.Description;
            trait.Type = (int)view.Type;
            trait.GP = view.GP;
            trait.Value = view.Value;

            trait.APGain = view.APGain;
            trait.APInvested = view.APInvest;

            trait.ModifyDate = DateTime.Now;
            trait.CreationDate = view.CreationDate;

            traitRepository.Update(trait);
            traitRepository.Submit();

            attributRepository.SetList(view.AttributList, trait.Id);
            attributRepository.Submit();

            resourceRepository.SetList(view.ResourceList, trait.Id);
            resourceRepository.Submit();

            valueRepository.SetList(view.ValueList, trait.Id);
            valueRepository.Submit();

            talentRepository.SetList(view.TalentList, trait.Id);
            talentRepository.Submit();
        }

        public override int GetAPEarned()
        {
            var x = this.traitRepository.GetTotalAPGain();
            return x;
        }
        public override int GetAPInvest()
        {
            return this.traitRepository.GetTotalAPInvest();
        }

        public void DeleteAll()
        {
            this.attributRepository.DeleteAll();
            this.resourceRepository.DeleteAll();
            this.valueRepository.DeleteAll();
            this.talentRepository.DeleteAll();
            this.traitRepository.DeleteAll();
        }

        private class InnerSQLTraitRepository : BaseCharakterRepository<T_Trait>
        {
            public InnerSQLTraitRepository(ApplicationContext context, int charakterID) : base(context, charakterID) { this.charakterID = charakterID; }

            public T_Trait Get(int? traitID)
            {
                var tabel = dbSet.Where(x => x.Id == traitID && x.CharakterID == charakterID).FirstOrDefault();
                if (tabel == null)
                {
                    tabel = CreateNewEntry();
                }
                return tabel;
            }
            
            public List<T_Trait> GetList()
            {
                return dbSet.Where(x => x.CharakterID == charakterID).ToList();
            }
            override protected T_Trait CreateNewEntry()
            {
                var tabel = new T_Trait()
                {
                    CharakterID = charakterID,
                    CreationDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Name = "NewTrait",
                };
                this.Insert(tabel);
                this.Submit();
                return tabel;
            }

            public int GetTotalAPGain()
            {
                return dbSet.Where(x => x.CharakterID == charakterID).Sum(x => x.APGain);
            }
            public int GetTotalAPInvest()
            {
                return dbSet.Where(x => x.CharakterID == charakterID).Sum(x => x.APInvested);
            }

            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach (var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
        }
        private class SQLAttributRepository : BaseRepository<T_TraitAttribute>
        {
            private int charakterID;
            public SQLAttributRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }
            public T_TraitAttribute Get(CharakterAttribut item, int traitID)
            {
                return dbSet.Where(x => x.AttributID == (int)item && x.CharakterID == charakterID && x.TraitID == traitID).FirstOrDefault();
            }
            public int GetTotal(CharakterAttribut item)
            {
                return dbSet.Where(x => x.AttributID == (int)item && x.CharakterID == charakterID).Sum(x => x.Value);
            }
            public void Set(CharakterAttribut item, int value, int traitID)
            {
                var akt = Get(item, traitID);
                if (akt != null)
                {
                    akt.Value = value;
                    Update(akt);
                }
                else
                {
                    akt = new T_TraitAttribute()
                    {
                        CharakterID = charakterID,
                        TraitID = traitID,
                        AttributID = (int)item,
                        Value = value
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
            public void SetList(List<IDValueView<CharakterAttribut>> list, int traitID)
            {
                if (list == null) return;
                foreach(var item in list)
                {
                    Set(item.ID, item.Value, traitID);
                }
            }
            public List<T_TraitAttribute> GetList(int traitID)
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID && x.TraitID == traitID);
                return list.ToList();
            }
            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach (var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
        }
        private class SQLResourceRepository : BaseRepository<T_TraitResources>
        {
            private int charakterID;
            public SQLResourceRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }
            public T_TraitResources Get(string type, int traitID)
            {
                return dbSet.Where(x => x.Type == type && x.CharakterID == charakterID && x.TraitID == traitID).FirstOrDefault();
            }
            public int GetTotal(string type)
            {
                return dbSet.Where(x => x.Type == type && x.CharakterID == charakterID).Sum(x => x.Value);
            }
            public void Set(string type, int value, int traitID)
            {
                var akt = Get(type, traitID);
                if (akt != null)
                {
                    akt.Value = value;
                    Update(akt);
                }
                else
                {
                    akt = new T_TraitResources()
                    {
                        CharakterID = charakterID,
                        TraitID = traitID,
                        Type = type,
                        Value = value
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
            public void SetList(List<IDValueView<string>> list, int traitID)
            {
                if (list == null) return;
                foreach (var item in list)
                {
                    Set(item.ID, item.Value, traitID);
                }
            }
            public List<T_TraitResources> GetList(int traitID)
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID && x.TraitID == traitID);
                return list.ToList();
            }

            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach (var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
        }
       
        private class SQLValueRepository : BaseRepository<T_TraitValues>
        {
            private int charakterID;
            public SQLValueRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }
            public T_TraitValues Get(string type, int traitID)
            {
                return dbSet.Where(x => x.Type == type && x.CharakterID == charakterID && x.TraitID == traitID).FirstOrDefault();
            }
            public int GetTotal(string type)
            {
                return dbSet.Where(x => x.Type == type && x.CharakterID == charakterID).Sum(x => x.Value);
            }
            public void Set(string type, int value, int traitID)
            {
                var akt = Get(type, traitID);
                if (akt != null)
                {
                    akt.Value = value;
                    Update(akt);
                }
                else
                {
                    akt = new T_TraitValues()
                    {
                        CharakterID = charakterID,
                        TraitID = traitID,
                        Type = type,
                        Value = value
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
            public void SetList(List<IDValueView<string>> list, int traitID)
            {
                if (list == null) return;
                foreach (var item in list)
                {
                    Set(item.ID, item.Value, traitID);
                }
            }
            public List<T_TraitValues> GetList(int traitID)
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID && x.TraitID == traitID);
                return list.ToList();
            }

            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach (var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
        }
        private class SQLTalentRepository : BaseRepository<T_TraitTalente>
        {
            private int charakterID;
            public SQLTalentRepository(ApplicationContext context, int charakterID) : base(context) { this.charakterID = charakterID; }
            public T_TraitTalente Get(string talentID, int traitID)
            {
                return dbSet.Where(x => x.TalentID == talentID && x.CharakterID == charakterID && x.TraitID == traitID).FirstOrDefault();
            }
            public int GetTotalTAW(string talentID)
            {
                return dbSet.Where(x => x.TalentID == talentID && x.CharakterID == charakterID).Sum(x => x.TAW);
            }
            public int GetTotalAT(string talentID)
            {
                return dbSet.Where(x => x.TalentID == talentID && x.CharakterID == charakterID).Sum(x => x.AT != null ? (int)x.AT : 0);
            }
            public int GetTotalPA(string talentID)
            {
                return dbSet.Where(x => x.TalentID == talentID && x.CharakterID == charakterID).Sum(x => x.PA != null ? (int)x.AT : 0);
            }
            public int GetTotalBL(string talentID)
            {
                return dbSet.Where(x => x.TalentID == talentID && x.CharakterID == charakterID).Sum(x => x.BL != null ? (int)x.AT : 0);
            }
            public void Set(string talentID, int traitID, int? taw = null, int? at = null, int? pa = null, int? bl = null)
            {
                if (taw == null) taw = 0;
                var akt = Get(talentID, traitID);
                if (akt != null)
                {
                    akt.TAW = (int)taw;
                    akt.AT = at;
                    akt.PA = pa;
                    akt.BL = bl;
                    Update(akt);
                }
                else
                {
                    akt = new T_TraitTalente()
                    {
                        CharakterID = charakterID,
                        TraitID = traitID,
                        TalentID = talentID,
                        TAW = (int)taw,
                        AT = at,
                        PA = pa,
                        BL = bl
                    };
                    Insert(akt);
                }
                context.SaveChanges();
            }
            public void SetList(List<TalentView> list, int traitID)
            {
                if (list == null) return;
                foreach (var item in list)
                {
                    Set(item.ID.ToString(), traitID, item.TAW, item.AT, item.PA, item.BL);
                }
            }
            public List<T_TraitTalente> GetList(int traitID)
            {
                var list = dbSet.Where(x => x.CharakterID == charakterID && x.TraitID == traitID);
                return list.ToList();
            }
            public void DeleteAll()
            {
                var talentList = this.dbSet.Where(x => x.CharakterID == this.charakterID).ToList();
                foreach (var item in talentList)
                {
                    this.Delete(item);
                }
                this.Submit();
            }
        }
    }
    
}
