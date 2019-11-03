using DSALib;
using DSALib.Charakter;
using DSALib.Charakter.Other;
using DSALib.Classes.JSON;
using DSALib.Interfaces;
using DSALib.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Charakter.Talente;
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
        #endregion
        public AbstractCharakter(Guid id)
        {
            ID = id;
            Traits = new CharakterTraits();
            Talente = new CharakterTalente(this);
            Descriptions = new CharakterDescription();
            Attribute = CreateAttribute();
            Values = CreateValues();
            Resources = CreateResources();


            if (Attribute == null)
            {
                throw new ArgumentNullException(nameof(Attribute) + " Die Attribute wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
            else if (Values == null)
            {
                throw new ArgumentNullException(nameof(Values) + " Die Values wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
            else if (Resources == null)
            {
                throw new ArgumentNullException(nameof(Traits));
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
            #region Speichern Möglich?
            if (Name == null || Name == string.Empty)
            {
                throw new Exception("Der Charakter benötigt einen Namen");
            }
            #endregion
            var charakter = new JSON_Charakter
            {
                ID          = ID,
                Name        = Name,
                SaveTime    = DateTime.Now
            };

            #region Values Speichern
            //Kein Sepeichern notwendig, da es errechnet wird....
            #endregion
            #region Attribute Speichern
            var attributeDictionary = new Dictionary<CharakterAttribut, int>();
            var attribute = Attribute.UsedAttributs;

            foreach (var attribut in attribute)
            {
                Error error = null;
                var value = Attribute.GetAttributAKTValue(attribut, out error);
                if (error != null)
                {
                    throw new Exception("Beim Speichern des Attributes " + attribut.ToString() + " ist ein Fehler aufgetreten: " + error.Message);
                }
                else
                {
                    attributeDictionary.Add(attribut, value);
                }
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
            foreach(var item in Traits.traits)
            {
                var jTrait = new JSON_Trait
                {
                    TraitType       = item.TraitType,
                    Description     = item.Description,
                    GP              = item.GP,
                    Title           = item.Title,
                    Value           = item.Value,
                    AttributeValues = new Dictionary<CharakterAttribut, int>(),
                    ResourceValues  = new Dictionary<string, int>(),
                    ValueValues     = new Dictionary<string, int>(),
                    TawBonus        = new Dictionary<Guid, int>(),
                    AtBonus         = new Dictionary<Guid, int>(),
                    PaBonus         = new Dictionary<Guid, int>()
                    
                };
                foreach(var innerItem in item.UsedAttributs())
                {
                    jTrait.AttributeValues.Add(innerItem, item.GetValue(innerItem));
                }
                foreach(var innerItem in item.UsedResources())
                {
                    jTrait.ResourceValues.Add(innerItem.Name, item.GetValue(innerItem));
                }
                foreach(var innerItem in item.UsedValues())
                {
                    jTrait.ValueValues.Add(innerItem.Name, item.GetValue(innerItem));
                }
                foreach(var innerItem in item.GetTawBonus())
                {
                    jTrait.TawBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                foreach(var innerItem in item.GetATBonus())
                {
                    jTrait.AtBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                foreach(var innerItem in item.GetPABonus())
                {
                    jTrait.PaBonus.Add(innerItem.Key.ID, innerItem.Value);
                }
                charakter.Traits.Add(jTrait);
            }
            #endregion
            return charakter;
        }
        public void Load(JSON_Charakter json_charakter, List<ITalent> talents) 
        {
            ID = json_charakter.ID;
            Name = json_charakter.Name;
            #region Attribute Laden
            foreach (var item in json_charakter.AttributeBaseValue.Keys)
            {
                Attribute.SetAKTValue(item, json_charakter.AttributeBaseValue[item], out Error error);
                if (error != null)
                {
                    throw new Exception(error.Message);
                }
            }
            #endregion
            #region Values Laden
            //kein Laden notwending
            #endregion
            #region Resources Laden
            //kein Laden notwendig
            #endregion
            #region Talente Laden
            foreach(var item in json_charakter.TalentTAW)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault();
                if(talent != null)
                {
                    Talente.SetTAW(talent, item.Value);
                }
            }
            foreach (var item in json_charakter.TalentAT)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault(null);
                if (talent != null && talent.GetType() == typeof(AbstractTalentFighting))
                {
                    Talente.SetAT((AbstractTalentFighting)talent, item.Value);
                }
            }
            foreach (var item in json_charakter.TalentPA)
            {
                var talent = talents.Where(x => x.ID == item.Key).FirstOrDefault(null);
                if (talent != null && talent.GetType() == typeof(AbstractTalentFighting)) 
                {
                    Talente.SetPA((AbstractTalentFighting)talent, item.Value);
                }
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
                    foreach(var innerItems in item.AttributeValues)
                    {
                        trait.SetValue(innerItems.Key, innerItems.Value);
                    }
                    foreach(var innerItems in item.ValueValues)
                    {
                        var value = GetValue(innerItems.Key);
                        if (value != null)
                        {
                            trait.SetValue(value, innerItems.Value);
                        }
                    }
                    foreach(var innerItems in item.ResourceValues)
                    {
                        var res = GetResource(innerItems.Key);
                        if (res != null)
                        {
                            trait.SetValue(res, innerItems.Value);
                        }
                    }

                    foreach(var innerItems in item.TawBonus)
                    {
                        var talent = talents.Where(x => x.ID == innerItems.Key).FirstOrDefault();
                        if(talent != null)
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
