﻿using DSALib;
using DSALib.Charakter;
using DSALib.Charakter.Functions;
using DSALib.Charakter.Other;
using DSALib.Charakter.Values.Settable;
using DSALib.Classes.JSON;
using DSALib.Interfaces;
using DSALib.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter
{
    public abstract class AbstractCharakter : ICharakter
    {
        #region Properties
        public Guid ID { get; set; }
        public string Name { get; set; }
        public CharakterValues Values { get; private set; }
        public CharakterAttribute Attribute { get; private set; }
        public CharakterResources Resources { get; private set; }
        public CharakterTalente Talente { get; private set; }
        public CharakterDescription Descriptions { get; private set; }
        public CharakterTraits Traits { get; private set; }
        public CharakterOther Other { get; private set; }
        #endregion
        public AbstractCharakter(Guid id)
        {
            CreateNew(id);
        }

        private void CreateNew(Guid id)
        {
            ID = id;
            Traits = new CharakterTraits();
            Talente = new CharakterTalente(this);
            Descriptions = new CharakterDescription();
            Other = new CharakterOther();
            Attribute = CreateAttribute();
            Resources = CreateResources();
            Values = CreateValues();

            if (Attribute == null)
            {
                throw new NotImplementedException(nameof(Attribute));
            }
            else if (Values == null)
            {
                throw new NotImplementedException(nameof(Values));
            }
            else if (Resources == null)
            {
                throw new NotImplementedException(nameof(Traits));
            }
            else if (Values.UsedValues.Where(x => Values.UsedValues.Where(y => y.Name == x.Name).Count() > 1).ToList().Any())
            {
                throw new ArgumentException(DSALib.Resources.ErrorValueDobbleElement);
            }


            Traits.AttributeChanged += (sender, args) =>
            {
                var value = Traits.GetValue(args);
                Attribute.SetModValue(args, value);
            };
            Traits.ResourceChanged += (sender, args) =>
            {
                var value = Traits.GetValue(args);
                Resources.SetModValue(args, value);
            };
            Traits.ValueChanged += (sender, args) =>
            {
                var value = Traits.GetValue(args);
                Values.SetModValue(args, value);
            };
        }
        #region AbstractMethods
        protected abstract CharakterValues CreateValues();
        protected abstract CharakterAttribute CreateAttribute();
        protected abstract CharakterResources CreateResources();
        #endregion
        #region Methods
        public JSON_Charakter CreateSave()
        {
            var charakter = new JSON_Charakter
            {
                ID = ID,
                Name = Name,
                SaveTime = DateTime.Now
            };
            #region Values Speichern
            charakter.SettableValues = new Dictionary<string, int>();
            var settableValues = Values.UsedValues.Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType()));
            foreach (var item in settableValues)
            {
                var value = Values.GetAKTValue(item, out Error error);
                charakter.SettableValues.Add(item.Name, value);
            }
            #endregion
            #region Attribute Speichern
            var attributeDictionary = new Dictionary<CharakterAttribut, int>();
            var attribute = Attribute.UsedAttributs;

            foreach (var attribut in attribute)
            {
                var value = Attribute.GetAttributAKTValue(attribut);
                attributeDictionary.Add(attribut, value);
            }
            charakter.AttributeBaseValue = attributeDictionary;
            #endregion
            #region Resources Speichern
            //kein Speichern notwendig
            #endregion
            #region Talente Speichern
            charakter.TalentTAW = new Dictionary<Guid, int>();
            charakter.TalentAT = new Dictionary<Guid, int>();
            charakter.TalentPA = new Dictionary<Guid, int>();
            charakter.MotherLanguages = new Dictionary<Guid, bool>();


            foreach (var item in Talente.TAWDictionary)
            {
                if (item.Value > 0)
                {
                    charakter.TalentTAW.Add(item.Key.ID, item.Value);
                }
            }
            foreach (var item in Talente.ATDictionary)
            {
                if (item.Value > 0)
                {
                    charakter.TalentAT.Add(item.Key.ID, item.Value);
                }
            }
            foreach (var item in Talente.PADictionary)
            {
                if (item.Value > 0)
                {
                    charakter.TalentPA.Add(item.Key.ID, item.Value);
                }
            }
            foreach (var item in Talente.MotherDicionary)
            {
                charakter.MotherLanguages.Add(item.Key.ID, item.Value);
            }

            var allTalents = new List<ITalent>(Talente.TAWDictionary.Keys);
            allTalents.AddRange(Talente.ATDictionary.Keys);
            allTalents.AddRange(Talente.PADictionary.Keys);

            charakter.TalentGuidsNames = new Dictionary<Guid, string>();
            foreach (var item in allTalents)
            {
                if (!charakter.TalentGuidsNames.ContainsKey(item.ID))
                {
                    charakter.TalentGuidsNames.Add(item.ID, item.Name);
                }
            }
            #endregion
            #region Descriptor Speichern
            charakter.Descriptors = new List<JSON_Descriptor>();
            foreach (var item in this.Descriptions.Descriptions)
            {
                charakter.Descriptors.Add(new JSON_Descriptor
                {
                    Priority = item.Priority,
                    DescriptionTitle = item.DescriptionTitle,
                    DescriptionText = item.DescriptionText
                });
            }
            #endregion
            #region Traits Speichern
            charakter.Traits = new List<JSON_Trait>();
            foreach (var item in Traits.traits)
            {
                var jTrait = new JSON_Trait
                {
                    TraitType = item.TraitType,
                    Description = item.Description,
                    GP = item.GP,
                    Title = item.Title,
                    Value = item.Value,
                    AttributeValues = new Dictionary<CharakterAttribut, int>(),
                    ResourceValues = new Dictionary<string, int>(),
                    ValueValues = new Dictionary<string, int>(),
                    TawBonus = new Dictionary<Guid, int>(),
                    AtBonus = new Dictionary<Guid, int>(),
                    PaBonus = new Dictionary<Guid, int>()

                };
                foreach (var innerItem in item.UsedAttributs())
                {
                    jTrait.AttributeValues.Add(innerItem, item.GetValue(innerItem));
                }
                foreach (var innerItem in item.UsedResources())
                {
                    jTrait.ResourceValues.Add(innerItem.Name, item.GetValue(innerItem));
                }
                foreach (var innerItem in item.UsedValues())
                {
                    jTrait.ValueValues.Add(innerItem.Name, item.GetValue(innerItem));
                }
                foreach (var innerItem in item.GetTawBonus())
                {
                    jTrait.TawBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                foreach (var innerItem in item.GetATBonus())
                {
                    jTrait.AtBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                foreach (var innerItem in item.GetPABonus())
                {
                    jTrait.PaBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                charakter.Traits.Add(jTrait);
            }
            #endregion
            #region Anderes Laden
            charakter.AktAP = Other.TotalAPPlayer;
            charakter.InvestAP = Other.InvestedAPPlayer;
            #endregion
            return charakter;
        }
        public void Load(JSON_Charakter json_charakter, List<ITalent> talents)
        {
            #region Nullprüfungen
            if (json_charakter.MotherLanguages == null) json_charakter.MotherLanguages = new Dictionary<Guid, bool>();
            #endregion
            CreateNew(json_charakter.ID);
            Name = json_charakter.Name;
            #region Attribute Laden
            foreach (var item in json_charakter.AttributeBaseValue.Keys)
            {
                Attribute.SetAKTValue(item, json_charakter.AttributeBaseValue[item]);
            }
            #endregion
            #region Values Laden
            if (json_charakter.SettableValues != null)
            {
                foreach (var item in json_charakter.SettableValues)
                {
                    var settableValue = Values.UsedValues.Where(x => x.Name == item.Key).FirstOrDefault();
                    if (settableValue != null && typeof(AbstractSettableValue).IsAssignableFrom(settableValue.GetType()))
                    {
                        Values.SetAKTValue((AbstractSettableValue)settableValue, item.Value);
                    }
                }
            }
            #endregion
            #region Resources Laden
            //kein Laden notwendig
            #endregion
            #region Talente Laden
            foreach (var item in json_charakter.TalentTAW)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault();
                if (talent != null)
                {
                    Talente.SetTAW(talent, item.Value);
                }
                else
                {
                    TalentMissing(json_charakter, item.Key);
                }
            }
            foreach (var item in json_charakter.TalentAT)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault();
                if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    Talente.SetAT((AbstractTalentFighting)talent, item.Value);
                }
                else
                {
                    TalentMissing(json_charakter, item.Key);
                }
            }
            foreach (var item in json_charakter.TalentPA)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault();
                if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    Talente.SetPA((AbstractTalentFighting)talent, item.Value);
                }
                else
                {
                    TalentMissing(json_charakter, item.Key);
                }
            }
            foreach (var item in json_charakter.MotherLanguages)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault();
                Talente.SetMother((TalentLanguage)talent, item.Value);
            }
            #endregion
            #region Descriptoren Laden
            foreach (var item in json_charakter.Descriptors)
            {
                this.Descriptions.AddDescripton(new Descriptor
                {
                    Priority = item.Priority,
                    DescriptionText = item.DescriptionText,
                    DescriptionTitle = item.DescriptionTitle
                });
            }
            #endregion
            #region Traits Laden
            if (json_charakter.Traits != null)
            {
                foreach (var item in json_charakter.Traits)
                {
                    var trait = new Trait
                    {
                        TraitType = item.TraitType,
                        Description = item.Description,
                        GP = item.GP,
                        Title = item.Title,
                        Value = item.Value,
                    };
                    foreach (var innerItems in item.AttributeValues)
                    {
                        trait.SetValue(innerItems.Key, innerItems.Value);
                    }
                    foreach (var innerItems in item.ValueValues)
                    {
                        var value = GetValue(innerItems.Key);
                        if (value != null)
                        {
                            trait.SetValue(value, innerItems.Value);
                        }
                    }
                    foreach (var innerItems in item.ResourceValues)
                    {
                        var res = GetResource(innerItems.Key);
                        if (res != null)
                        {
                            trait.SetValue(res, innerItems.Value);
                        }
                    }

                    foreach (var innerItems in item.TawBonus)
                    {
                        var talent = talents.Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        if (talent != null)
                        {
                            trait.SetTaWBonus(talent, innerItems.Value);
                        }
                    }
                    foreach (var innerItems in item.AtBonus)
                    {
                        var talent = talents.Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                        {
                            trait.SetATBonus((AbstractTalentFighting)talent, innerItems.Value);
                        }
                    }
                    foreach (var innerItems in item.PaBonus)
                    {
                        var talent = talents.Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        if (talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                        {
                            trait.SetPABonus((AbstractTalentFighting)talent, innerItems.Value);
                        }
                    }

                    Traits.AddTrait(trait);
                }
            }
            #endregion
            #region Anderes Laden
            Other.TotalAPPlayer = json_charakter.AktAP;
            Other.InvestedAPPlayer = json_charakter.InvestAP;
            #endregion
        }
        private void TalentMissing(JSON_Charakter json_charakter, Guid guid)
        {
            try
            {
                var talent = json_charakter.TalentGuidsNames.Where(x => x.Key == guid).FirstOrDefault();
                LogStrings.LogString(LogLevel.ErrorLog, "Talent Fehlt: " + talent.Key + " " + talent.Value);
            }
            catch (Exception)
            {

            }
        }
        internal IValue GetValue(string name)
        {
            return Values.UsedValues.Where(x => x.Name == name).FirstOrDefault();
        }
        internal IResource GetResource(string name)
        {
            return Resources.UsedValues.Where(x => x.Name == name).FirstOrDefault();
        }
        #endregion
    }
}
