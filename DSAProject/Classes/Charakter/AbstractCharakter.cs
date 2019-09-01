using DSALib;
using DSALib.Classes.JSON;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;

using System;
using System.Collections.Generic;
using System.IO;

namespace DSAProject.Classes.Charakter
{
    public abstract class AbstractCharakter : ICharakter
    {
        #region Properties
        private Guid ID { get; set; } 
        public String Name { get; set; }
        public ICharakterValues Values { get; private set; }
        public ICharakterAttribut Attribute { get; private set; }
        public CharakterTalente  CharakterTalente { get; private set; }
        public CharakterDescription CharakterDescriptions { get; private set; } 
        #endregion
        public AbstractCharakter()
        {
            ID                      = Game.Game.GenerateNextCharakterGUID();
            Attribute               = CreateAttribute();
            Values                  = CreateValues();
            CharakterTalente        = new CharakterTalente(this);
            CharakterDescriptions   = new CharakterDescription();

            if (Attribute == null )
            {
                throw new ArgumentNullException(nameof(Attribute) + " Die Attribute wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
            else if(Values == null)
            {
                throw new ArgumentNullException(nameof(Values) + " Die Values wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
        }
        #region AbstractMethods
        protected abstract ICharakterValues CreateValues();
        protected abstract ICharakterAttribut CreateAttribute();
        #endregion
        #region Methods
        public void Save(out Error error)
        {
            error           = new Error();
            var charakter   = new JSON_Charakter();

            if(Name == null || Name == string.Empty)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = "Der Charakter benötigt einen Namen"
                };
            }
            else
            {
                #region Descriptor Speichern
                charakter.Descriptors = new List<JSON_Descriptor>();
                foreach(var item in this.CharakterDescriptions.Descriptions)
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
                    error = null;
                    var value = Attribute.GetAttributAKTValue(attribut, out error);
                    if(error == null)
                    {
                        attributeDictionary.Add(attribut, value);
                    } 
                    else
                    {
                        var internError = new Error
                        {
                            ErrorCode = ErrorCode.Error,
                            Message = "Beim Speichern des Attributes " + attribut.ToString() + " ist ein Fehler aufgetreten: " + error.Message
                        };
                        Logger.Log(LogLevel.ErrorLog, internError.Message, nameof(AbstractCharakter), nameof(Save));
                        error = internError;
                        break;
                    }
                }
                charakter.AttributeBaseValue = attributeDictionary;
                #endregion
                #region Speichern in Datei
                if(error == null)
                {
                    var filePath = Path.Combine(Game.Game.CharakterSaveFolder, ID.ToString() + ".save");
                    FileManagment.WriteToFile(charakter.JSONContent, filePath, Windows.Storage.CreationCollisionOption.ReplaceExisting, out error);

                    if(error == null) {
                        #region Meta File
                        var metaFile = new JSON_CharakterMetaData
                        {
                            ID          = ID,
                            Name        = Name,
                            SaveFile    = ID.ToString() + ".save",
                            SaveTime    = DateTime.Now,
                            Game        = this.GetType().ToString()
                        };
                        var y = DateTime.Now;
                        var metaFilePath = Path.Combine(Game.Game.CharakterMetaFolder, ID.ToString() + ".save");
                        FileManagment.WriteToFile(metaFile.JSONContent, metaFilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting, out error);
                        #endregion
                    }
                }
                #endregion
            }
        }
        public void Load(string fileName, out Error error)
        {
            error       = new Error();
            var file    = Path.Combine(Game.Game.CharakterSaveFolder, fileName);
            var ret     = FileManagment.LoadTextFile(file, out error);

            if(error == null)
            {
                #region DeSerializeJson
                var json_charakter = JSON_Charakter.DeSerializeJson(ret, out string errorstring);
                if(errorstring != null)
                {
                    error = new Error
                    {
                        ErrorCode = ErrorCode.SerializationError,
                        Message = errorstring
                    };
                }
                #endregion
                else
                {
                    #region Descriptoren Laden
                    foreach(var item in json_charakter.Descriptors)
                    {
                        this.CharakterDescriptions.AddDescripton(new Descriptor
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
                        Attribute.SetAttributAKTValue(item, json_charakter.AttributeBaseValue[item], out error);
                        if(error != null)
                        {
                            break;
                        }
                    }
                    #endregion
                    if (error != null)
                    {

                    }
                }
            }
        }
        #endregion
    }
}
