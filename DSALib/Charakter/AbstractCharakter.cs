using DSALib;
using DSALib.Charakter;
using DSALib.Classes.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;

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
        public CharakterTalente  Talente { get; private set; }
        public CharakterDescription Descriptions { get; private set; }
        public CharakterTraits Traits { get; private set; }
        #endregion
        public AbstractCharakter(Guid id)
        {
            ID              = id;
            Traits          = new CharakterTraits();
            Talente         = new CharakterTalente(this);
            Descriptions    = new CharakterDescription();
            Attribute       = CreateAttribute();
            Values          = CreateValues();
            Resources       = CreateResources();
            

            if (Attribute == null )
            {
                throw new ArgumentNullException(nameof(Attribute) + " Die Attribute wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
            else if(Values == null)
            {
                throw new ArgumentNullException(nameof(Values) + " Die Values wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
            else if(Resources == null)
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
            var charakter   = new JSON_Charakter();

            if(Name == null || Name == string.Empty)
            {
                throw new Exception("Der Charakter benötigt einen Namen");
            }
            else
            {
                #region Descriptor Speichern
                charakter.Descriptors = new List<JSON_Descriptor>();
                foreach(var item in this.Descriptions.Descriptions)
                {
                    charakter.Descriptors.Add(new JSON_Descriptor
                    {
                        Priority            = item.Priority,
                        DescriptionTitle    = item.DescriptionTitle,
                        DescriptionText     = item.DescriptionText
                    });
                }
                #endregion
                #region Attribute Speichern
                var attributeDictionary = new Dictionary<CharakterAttribut, int>();
                var attribute           = Attribute.UsedAttributs;

                foreach(var attribut in attribute)
                {
                    Error error = null;
                    var value = Attribute.GetAttributAKTValue(attribut, out error);
                    if(error != null)
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
            }
            return charakter;
        }
        public void Load(JSON_Charakter json_charakter)
        {   
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
        }
        #endregion
    }
}
