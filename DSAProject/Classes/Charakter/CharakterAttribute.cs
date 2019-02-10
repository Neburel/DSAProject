using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter
{
    public enum CharakterAttribut
    {
        Mut                 = 1,
        Klugheit            = 2,
        Intuition           = 3,
        Charisma            = 4,
        Fingerfertigkeit    = 5,
        Gewandheit          = 6,
        Konstitution        = 7,
        Körperkraft         = 8,
        Sozialstatus        = 9
    };

    public class CharakterAttribute : ICharakterAttribut
    {
        #region Events
        public event EventHandler<CharakterAttribut> ChangedAttributBaseEvent;
        public event EventHandler<CharakterAttribut> ChangedAttributTotalEvent;
        #endregion
        #region Properties
        public List<CharakterAttribut> UsedAttributs { get => attributValues.Keys.ToList(); }
        #endregion
        #region Variables
        private Dictionary<CharakterAttribut, int> attributValues;
        private Dictionary<CharakterAttribut, int> attributTotalValues;
        private Dictionary<CharakterAttribut, int> attributMaxValues;
        #endregion

        public CharakterAttribute(List<CharakterAttribut> attributs)
        {
            attributValues = new Dictionary<CharakterAttribut, int>();
            attributMaxValues = new Dictionary<CharakterAttribut, int>();

            foreach(var item in attributs)
            {
                attributValues.Add(item, 0);
            }
        }
        public void SetAttributValue(CharakterAttribut attribut, int value, out Error error)
        {
            error = null; 
            try
            {
                error = null;
                var regularValue        = attributValues.TryGetValue(attribut, out int currentValue);
                var mattributMaxValues  = attributMaxValues.TryGetValue(attribut, out int maxValue);

                if(regularValue == false)
                {
                    error = new Error { ErrorCode = util.ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                } 
                else
                {
                    if (mattributMaxValues)
                    {
                        if(value > maxValue)
                        {
                            currentValue = maxValue;
                            error = new Error { ErrorCode = util.ErrorCode.InvalidValue, Message = "Der Maximum wert des Charakters wurde überschritten" };
                        }
                    }
                    attributValues[attribut] = value; 
                }
            }
            catch(Exception ex)
            {
                Logger.Log(util.LogLevel.ErrorLog, ex.Message, nameof(CharakterAttribute), nameof(SetAttributValue));
                error = new Error { ErrorCode = util.ErrorCode.Error, Message = ex.Message };
            }
        }
        public int GetAttributValue(CharakterAttribut attribut, out Error error)
        {
            error = null;
            var ret = -1;

            try
            {
                var regularValue = attributValues.TryGetValue(attribut, out int currentValue);

                if (regularValue == false)
                {
                    error = new Error { ErrorCode = util.ErrorCode.InvalidValue, Message = "Das Gewählte Attribut exestiert bei diesem Charakter nicht" };
                }
                else
                {
                    ret = attributValues[attribut];
                }
            }
            catch (Exception ex)
            {
                Logger.Log(util.LogLevel.ErrorLog, ex.Message, nameof(CharakterAttribute), nameof(SetAttributValue));
                error = new Error { ErrorCode = util.ErrorCode.Error, Message = ex.Message };
            }
            return ret;
        }  
    }
}
