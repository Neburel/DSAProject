using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Values.Settable;
using DSALib2.Classes.Charakter.View;
using DSALib2.Classes.JSONSave;
using DSALib2.Interfaces.Charakter;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib2.Classes.Charakter
{
    public abstract class AbstractCharakter
    {
        private IDescriptionRepository description;
        private IAttributeRepository attribute;
        private IResourcesRepository resources;
        private ITalentRepository talente;
        private ITraitRepository traits;
        private IValueRepository values;
        private IAPRepository ap;
        private IMoneyRepository money;

        public IDescriptionRepository Description { get { if (description == null) { description = GetDescriptionRepository(); } return description; } private set => description = value; }
        public IAttributeRepository Attribute { get { if (attribute == null) { attribute = GetNewAttributeRepository(); } return attribute; } private set => attribute = value; }
        public IResourcesRepository Resources { get { if (resources == null) { resources = GetNewResourcesRepository(); } return resources; } private set => resources = value; }
        public ITalentRepository Talente { get { if (talente == null) { talente = GetNewTalentRepository(); } return talente; } private set => talente = value; }
        public ITraitRepository Traits { get { if (traits == null) { traits = GetNewTraitRepository(); } return traits; } private set => traits = value; }
        public IValueRepository Values { get { if (values == null) { values = GetNewValueRepository(); } return values; } private set => values = value; }
        public IAPRepository AP { get { if (ap == null) { ap = GetAPRepository(); } return ap; } private set => ap = value; }
        public IMoneyRepository Money { get { if (money == null) { money = GetMoneyRepository(); } return money; } private set => money = value; }
        protected abstract IDescriptionRepository GetDescriptionRepository();
        protected abstract IAttributeRepository GetNewAttributeRepository();
        protected abstract IResourcesRepository GetNewResourcesRepository();
        protected abstract ITalentRepository GetNewTalentRepository();
        protected abstract ITraitRepository GetNewTraitRepository();
        protected abstract IValueRepository GetNewValueRepository();
        protected abstract IAPRepository GetAPRepository();
        protected abstract IMoneyRepository GetMoneyRepository();

        public AbstractCharakter()
        {
        }

        //Der Import Überschreibt einen Charakter!
        public void Import(JSONCharakter jsonCharakter)
        {
            #region Nullprüfungen
            if (jsonCharakter == null) throw new ArgumentNullException(nameof(jsonCharakter));
            if (jsonCharakter.MotherLanguages == null) jsonCharakter.MotherLanguages = new Dictionary<Guid, bool>();
            if (jsonCharakter.DeductionTalent == null) jsonCharakter.DeductionTalent = new Dictionary<Guid, Guid>();
            #endregion
            //CreateNew(jsonCharakter.ID);
            var descriptionView = new DescriptionView()
            {
                Name = jsonCharakter.Name
            };
            #region Traits Laden
            var traitViewList = new List<TraitView>();
            if (jsonCharakter.Traits != null)
            {
                foreach (var item in jsonCharakter.Traits)
                {
                    var trait = this.Traits.GetEmptyView();

                    trait.Type = item.TraitType;
                    trait.Description = item.Description;
                    trait.GP = item.GP;
                    trait.Name = item.Title;
                    trait.Value = item.Value;
                    trait.APGain = item.APEarned;
                    trait.APInvest = item.APInvest;

                    foreach (var innerItems in item.AttributeValues)
                    {
                        trait.AttributList.Add(new IDValueView<Utils.CharakterAttribut>
                        {
                            ID = innerItems.Key,
                            Value = innerItems.Value
                        });
                    }
                    foreach (var innerItems in item.ValueValues)
                    {
                        var value = Values.GetList().Where(x => x.Name == innerItems.Key).FirstOrDefault();
                        if (value != null)
                        {
                            trait.ValueList.Add(new IDValueView<string> {
                                ID = value.GetType().ToString(),
                                Value = innerItems.Value
                            });
                        }
                    }
                    foreach (var innerItems in item.ResourceValues)
                    {
                        var res = Resources.GetList().Where(x => x.Name == innerItems.Key).FirstOrDefault();
                        if (res != null)
                        {
                            trait.ResourceList.Add(new IDValueView<string>
                            {
                                ID = res.GetType().ToString(),
                                Value = innerItems.Value
                            });
                        }
                    }

                    var talentTraitViewList = new List<TalentView>();
                    foreach (var innerItems in item.TawBonus)
                    {
                        var talent = Talente.GetList<ITalent>().Where(x => x.ID == innerItems.Key).FirstOrDefault();

                        if (talent != null)
                        {
                            talentTraitViewList.Add(new TalentView
                            {
                                ID = talent.ID,
                                TAW = innerItems.Value
                            });
                        }
                    }
                    foreach (var innerItems in item.AtBonus)
                    {
                        var talent = Talente.GetList<ITalent>().Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        TalentView talentView = talentTraitViewList.Where(x => x.ID == innerItems.Key).FirstOrDefault(); ;
                        if (talentView == null && talent != null)
                        {
                            talentView = new TalentView
                            {
                                ID = talent.ID
                            };
                            talentTraitViewList.Add(talentView);
                        }

                        if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                        {
                            talentView.AT = innerItems.Value;
                        }
                    }
                    foreach (var innerItems in item.PaBonus)
                    {
                        var talent = Talente.GetList<ITalent>().Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        TalentView talentView = talentTraitViewList.Where(x => x.ID == innerItems.Key).FirstOrDefault(); ;
                        if (talentView == null && talent != null)
                        {
                            talentView = new TalentView
                            {
                                ID = talent.ID
                            };
                            talentTraitViewList.Add(talentView);
                        }

                        if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                        {
                            talentView.PA = innerItems.Value;
                        }
                    }
                    foreach (var innerItems in item.BLBonus)
                    {
                        var talent = Talente.GetList<ITalent>().Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        TalentView talentView = talentTraitViewList.Where(x => x.ID == innerItems.Key).FirstOrDefault(); ;
                        if (talentView == null && talent != null)
                        {
                            talentView = new TalentView
                            {
                                ID = talent.ID
                            };
                            talentTraitViewList.Add(talentView);
                        }

                        if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                        {
                            talentView.BL = innerItems.Value;
                        }
                    }
                    trait.TalentList = talentTraitViewList;
                    traitViewList.Add(trait);
                }
            }
            #endregion
            #region Talente Laden
            var talentViewList = new List<TalentView>();
            foreach (var item in jsonCharakter.TalentTAW)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                if (talent != null)
                {
                    talentViewList.Add(new TalentView
                    {
                        ID = talent.ID,
                        TAW = item.Value
                    });
                }
            }
            foreach (var item in jsonCharakter.TalentAT)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                TalentView talentView = talentViewList.Where(x => x.ID == item.Key).FirstOrDefault();

                if (talentView == null && talent != null)
                {
                    talentView = new TalentView
                    {
                        ID = talent.ID
                    };
                    talentViewList.Add(talentView);
                }

                if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    talentView.AT = item.Value;
                }
            }
            foreach (var item in jsonCharakter.TalentPA)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                TalentView talentView = talentViewList.Where(x => x.ID == item.Key).FirstOrDefault();

                if (talentView == null && talent != null)
                {
                    talentView = new TalentView
                    {
                        ID = talent.ID
                    };
                    talentViewList.Add(talentView);
                }

                if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    talentView.PA = item.Value;
                }
            }
            foreach (var item in jsonCharakter.TalentBL)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                TalentView talentView = talentViewList.Where(x => x.ID == item.Key).FirstOrDefault();

                if (talentView == null && talent != null)
                {
                    talentView = new TalentView
                    {
                        ID = talent.ID
                    };
                    talentViewList.Add(talentView);
                }

                if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    talentView.PA = item.Value;
                }
            }
            foreach (var item in jsonCharakter.DeductionTalent)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                TalentView talentView = talentViewList.Where(x => x.ID == item.Key).FirstOrDefault();

                if (talentView == null && talent != null)
                {
                    talentView = new TalentView
                    {
                        ID = talent.ID
                    };
                    talentViewList.Add(talentView);
                }
                talentView.DeductionSelected = new DeductionView()
                {
                    ID = item.Key
                };
            }

            foreach (var item in talentViewList)
            {
                this.Talente.SetbyView(item);
            }

            var languageList = Talente.GetViewList();
            foreach (var item in jsonCharakter.MotherLanguages)
            {
                var talent = Talente.GetList<ITalent>().Where(x => x.ID == item.Key).FirstOrDefault();
                LanguageView languageView = languageList.Where(x => x.IDSprache == item.Key).FirstOrDefault();
                if(languageView == null)
                {
                    languageView = new LanguageView
                    {
                        IDSprache = talent.ID
                    };
                }
                languageView.Mother = item.Value;
            }
            #endregion
            #region Descriptoren Laden, Veraltet, soll abgeschafft werden
            var descriptorList = jsonCharakter.Descriptors;

            descriptionView.Alter = GetDescriptorInt("Alter", descriptorList);
            descriptionView.Anrede = GetDescriptorText("Anrede", descriptorList);
            descriptionView.Augenfarbe = GetDescriptorText("Augenfarbe", descriptorList);
            descriptionView.Familienstatus = (FamilienstatusEnum) GetDescriptorInt("Familienstatus", descriptorList);
            descriptionView.Geburstag = GetDescriptorText("Geburstag", descriptorList);
            descriptionView.Geschlecht = (GeschlechtEnum) GetDescriptorInt("Geschlecht", descriptorList);
            descriptionView.Gewicht = GetDescriptorInt("Gewicht", descriptorList);
            descriptionView.Glaube = GetDescriptorText("Glaube", descriptorList);
            descriptionView.Groesse = GetDescriptorInt("Größe", descriptorList);
            descriptionView.Haarfarbe = GetDescriptorText("Haarfarbe", descriptorList);
            descriptionView.Hautfarbe = GetDescriptorText("Hautfarbe", descriptorList);
            descriptionView.Kultur = GetDescriptorText("Kultur-/en", descriptorList);
            descriptionView.Name = GetDescriptorText("Name", descriptorList);
            descriptionView.Profession = GetDescriptorText("Profession", descriptorList);
            descriptionView.Rasse = GetDescriptorText("Rasse-/en", descriptorList);
            #endregion
            #region Money
            var moneyView = new MoneyView();
            if (jsonCharakter.Money != null)
            {
                var money = jsonCharakter.Money;
                moneyView.BankDublonen = money.Bank;
                moneyView.Dublonen = money.D;
                moneyView.Heller = money.H;
                moneyView.Kupfer = money.K;
                moneyView.Silber = money.S;
            }
            #endregion
            #region Abenteuerpunkte
            var apView = new APView
            {
                APGainHand = jsonCharakter.AktAP,
                APInvestHand = jsonCharakter.InvestAP
            };
            #endregion
            #region Werte Setzen
            this.Description.SetByView(descriptionView);
            foreach (var item in jsonCharakter.AttributeBaseValue.Keys)
            {
                Attribute.SetAKT(item, jsonCharakter.AttributeBaseValue[item]);
            }
            if (jsonCharakter.SettableValues != null)
            {
                foreach (var item in jsonCharakter.SettableValues)
                {
                    var value = Values.GetList().Where(x => x.Name == item.Key).FirstOrDefault();

                    if (value != null && typeof(AbstractSettableValue).IsAssignableFrom(value.GetType()))
                    {
                        Values.SetAKT((AbstractSettableValue)value, item.Value);
                    }
                }
            }
            foreach (var item in languageList)
            {
                this.Talente.SetbyView(item);
            }
            foreach(var item in traitViewList)
            {
                this.Traits.SetByView(item);
            }
            this.Money.SetByView(moneyView);
            this.AP.SetbyView(apView);
            #endregion
        }
        public abstract void Delete();
        public JSONCharakter Export()
        {
            var descriptionView = Description.GetView();
            var attributViewList = Attribute.GetViewList();
            var talentView = Talente.GetViewList<ITalent>();
            var languageTalents = Talente.GetViewList();
            var traitViewList = Traits.GetViewList();
            var moneyView = Money.GetView();
            var apView = AP.GetView();

            var charakter = new JSONCharakter
            {
                Name = descriptionView.Name,
                SaveTime = DateTime.Now
            };
            #region Values Speichern
            charakter.SettableValues = new Dictionary<string, int>();
            var settableValues = Values.GetList().Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType()));
            foreach (var item in settableValues)
            {
                var value = Values.GetAKT(item);
                charakter.SettableValues.Add(item.Name, value);
            }
            #endregion
            #region Attribute Speichern
            var attributeDictionary = new Dictionary<CharakterAttribut, int>();
            foreach (var attribut in attributViewList)
            {
                attributeDictionary.Add(attribut.ID, attribut.AKT);
            }
            charakter.AttributeBaseValue = attributeDictionary;
            #endregion
            #region Talente Speichern
            charakter.TalentTAW = new Dictionary<Guid, int>();
            charakter.TalentAT = new Dictionary<Guid, int>();
            charakter.TalentPA = new Dictionary<Guid, int>();
            charakter.TalentBL = new Dictionary<Guid, int>();
            charakter.DeductionTalent = new Dictionary<Guid, Guid>();
            charakter.MotherLanguages = new Dictionary<Guid, bool>();

            foreach (var item in talentView)
            {
                if (item.TAW > 0)
                {
                    charakter.TalentTAW.Add(item.ID, item.TAW);
                }
                if (item.PA != null && item.PA > 0)
                {
                    charakter.TalentPA.Add(item.ID, (int)item.PA);
                }
                if (item.AT != null && item.AT > 0)
                {
                    charakter.TalentAT.Add(item.ID, (int)item.AT);
                }
                if (item.BL != null && item.BL > 0)
                {
                    charakter.TalentBL.Add(item.ID, (int)item.BL);
                }
                if (item.DeductionSelected != null)
                {
                    charakter.DeductionTalent.Add(item.ID, item.DeductionSelected.ID);
                }
            }

            foreach (var item in languageTalents.Where(x => x.Mother == true))
            {
                charakter.MotherLanguages.Add(item.IDSprache, (bool)item.Mother);
            }

            charakter.TalentGuidsNames = new Dictionary<Guid, string>();
            foreach (var item in talentView)
            {
                if (!charakter.TalentGuidsNames.ContainsKey(item.ID))
                {
                    charakter.TalentGuidsNames.Add(item.ID, item.Name);
                }
            }
            #endregion
            #region Descriptor Speichern
            charakter.Descriptors = new List<JSONDescriptor>
            {
                CreateDescriptior("Alter", descriptionView.Alter),
                CreateDescriptior("Anrede", descriptionView.Anrede),
                CreateDescriptior("Augenfarbe", descriptionView.Augenfarbe),
                CreateDescriptior("Familienstatus", (int)descriptionView.Familienstatus),
                CreateDescriptior("Geburstag", descriptionView.Geburstag),
                CreateDescriptior("Geschlecht", (int)descriptionView.Geschlecht),
                CreateDescriptior("Gewicht", descriptionView.Gewicht),
                CreateDescriptior("Glaube", descriptionView.Glaube),
                CreateDescriptior("Größe", descriptionView.Groesse),
                CreateDescriptior("Haarfarbe", descriptionView.Haarfarbe),
                CreateDescriptior("Hautfarbe", descriptionView.Hautfarbe),
                CreateDescriptior("Kultur-/en", descriptionView.Kultur),
                CreateDescriptior("Name", descriptionView.Name),
                CreateDescriptior("Profession", descriptionView.Profession),
                CreateDescriptior("Rasse-/en", descriptionView.Rasse)
            };
            #endregion
            #region Traits Speichern
            charakter.Traits = new List<JSONTrait>();
            foreach (var item in traitViewList)
            {
                var jTrait = new JSONTrait
                {
                    TraitType = item.Type,
                    Description = item.Description,
                    GP = item.GP,
                    Title = item.Name,
                    Value = item.Value,
                    APEarned = item.APGain,
                    APInvest = item.APInvest,
                    AttributeValues = new Dictionary<CharakterAttribut, int>(),
                    ResourceValues = new Dictionary<string, int>(),
                    ValueValues = new Dictionary<string, int>(),
                    TawBonus = new Dictionary<Guid, int>(),
                    AtBonus = new Dictionary<Guid, int>(),
                    PaBonus = new Dictionary<Guid, int>(),
                    BLBonus = new Dictionary<Guid, int>()
                };
                foreach (var innerItem in item.AttributList)
                {
                    jTrait.AttributeValues.Add(innerItem.ID, innerItem.Value);
                }
                foreach (var innerItem in item.ResourceList)
                {
                    jTrait.ResourceValues.Add(innerItem.Name, innerItem.Value);
                }
                foreach (var innerItem in item.ValueList)
                {
                    jTrait.ValueValues.Add(innerItem.Name, innerItem.Value);
                }
                foreach (var innerItem in item.TalentList)
                {
                    if(innerItem.TAW != 0)
                    {
                        jTrait.TawBonus.Add(innerItem.ID, innerItem.TAW);
                    }
                    if (innerItem.AT != null && innerItem.AT != 0)
                    {
                        jTrait.AtBonus.Add(innerItem.ID, (int)innerItem.AT);
                    }
                    if (innerItem.PA != null && innerItem.PA != 0)
                    {
                        jTrait.PaBonus.Add(innerItem.ID, (int)innerItem.PA);
                    }
                    if (innerItem.BL != null && innerItem.BL != 0)
                    {
                        jTrait.BLBonus.Add(innerItem.ID, (int)innerItem.BL);
                    }
                }
                charakter.Traits.Add(jTrait);
            }
            #endregion
            #region Money
            charakter.Money = new JSONMoney() { 
                Bank = moneyView.BankDublonen,
                D = moneyView.Dublonen,
                H = moneyView.Heller,
                K = moneyView.Kupfer,
                S = moneyView.Silber
            };
            #endregion
            #region Anderes Laden
            charakter.AktAP = apView.APGainHand;
            charakter.InvestAP = apView.APInvestHand;
            #endregion
            return charakter;
        }
        private string GetDescriptorText(string name, List<JSONDescriptor> descriptorList)
        {
            var descriptor = descriptorList.Where(x => x.DescriptionTitle.Replace(":", "") == name).FirstOrDefault();

            if(descriptor != null)
            {
                return descriptor.DescriptionText;
            }
            return null;
        }
        private int GetDescriptorInt(string name, List<JSONDescriptor> descriptorList)
        {
            var descriptor = descriptorList.Where(x => x.DescriptionTitle.Replace(":", "") == name).FirstOrDefault();

            if (descriptor != null)
            {
                if(int.TryParse(descriptor.DescriptionText, out int value))
                {
                    return value;
                }
            }
            return 0;
        }
        private JSONDescriptor CreateDescriptior(string title, int text)
        {
            return new JSONDescriptor()
            {
                DescriptionTitle = title,
                DescriptionText = text.ToString()
            };
        }
        private JSONDescriptor CreateDescriptior(string title, string text)
        {
            return new JSONDescriptor()
            {
                DescriptionTitle = title,
                DescriptionText = text
            };
        }
    }
}
