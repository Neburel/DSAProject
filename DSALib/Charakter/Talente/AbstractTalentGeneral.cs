using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;

using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter.Talente
{
    public class AbstractTalentGeneral : AbstractTalent
    {
        #region Variables
        public override int BaseDeduction => 10;
        #endregion
        #region Properties
        public ITalent FatherTalent { get; set; }
        public List<CharakterAttribut> Attributs { get; private set; }
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
            var ret = string.Empty;

            if (FatherTalent != null)
            {
                ret = FatherTalent.ToString() + "(";
            }
            ret = ret + Name;

            if (NameExtension != null && NameExtension != string.Empty)
            {
                ret = ret + "(" + NameExtension.ToUpper() + ")";
            }
            if (FatherTalent != null)
            {
                ret = ret + ")";
            }

            return ret;
        }


        public string ProbeText
        {
            get => GetProbeText();
        }
    }
}
