using DSAProject.Classes.Interfaces;
using DSAProject.Classes.JSON;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter
{
    public abstract class AbstractCharakter : ICharakter
    {
        #region Properties
        public ICharakterAttribut Attribute { get; private set; }
        public IRace Race { get; private set; }
        private static string SaveFolder => "CharakterSaves";
        #endregion
        public AbstractCharakter()
        {
            Attribute = CharakterCreateAttribute();

            if(Attribute == null)
            {
                throw new ArgumentNullException(nameof(Attribute) + " Die Attribute wurde nicht gesetzt. Bitte Implementieren sie die dazu Notwendige Methode");
            }
        }
        #region AbstractMethods
        protected abstract ICharakterAttribut CharakterCreateAttribute();
        #endregion
        #region Methods
        public void Save(string fileName, out Error error)
        {
            error           = new Error();
            var charakter   = new JSON_Charakter();
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
                var file = Path.Combine(SaveFolder, fileName);
                FileManagment.WriteToFile(charakter.JSONContent, file, out error);
            }
            #endregion
        }
        public void Load(string fileName, out Error error)
        {
            error       = new Error();
            var file    = Path.Combine(SaveFolder, fileName);
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
                    #region Attribute Laden
                    foreach(var item in json_charakter.AttributeBaseValue.Keys)
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
