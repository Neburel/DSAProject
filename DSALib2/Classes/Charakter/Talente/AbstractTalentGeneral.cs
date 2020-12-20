using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Talente
{
    public class AbstractTalentGeneral : AbstractTalent
    {
        #region Variables
        public override int BaseDeduction => 10;
        #endregion
        #region Properties
        public List<CharakterAttribut> Attributs { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Sammlungseigenschaften müssen schreibgeschützt sein", Justification = "<Ausstehend>")]
        public List<ITalentRequirement> Requirements { get; set; }
        #endregion
        public AbstractTalentGeneral(Guid id, List<CharakterAttribut> attributes) : base(id)
        {
            Requirements    = new List<ITalentRequirement>();
            Attributs       = new List<CharakterAttribut>();

            if(attributes != null)
            {
                foreach (var item in attributes)
                {
                    this.Attributs.Add(item);
                }
            }
        }
        public string GetProbeText()
        {
            var ret = string.Empty;
            var i = 0;


            foreach(var item in Attributs)
            {
                if (i == 0)
                {
                    ret = Helper.GetShort(item);
                } 
                else
                {
                    ret = ret + "/" + Helper.GetShort(item);
                }
                i++;
            }

            return ret;
        }
        public override string ToString()
        {
            var ret = Name;

            if (!string.IsNullOrEmpty(NameExtension))
            {
                ret = ret + "(" + NameExtension.ToUpper(Helper.CultureInfo) + ")";
            }            
            return ret;
        }
    }
}
