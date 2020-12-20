using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Talente
{
    public abstract class AbstractTalent : ITalent
    {
        #region Properties
        public int OrginalPosition { get; set; }
        public Guid ID { set; get; }
        public abstract int BaseDeduction { get; }
        public string BE { get; set; }
        public string Name { get; set; }
        public string NameExtension { get; set; }
        public List<ITalentDeduction> Deductions { get; private set;}
        #endregion
        public AbstractTalent(Guid id)
        {
            ID = id;
            Deductions = new List<ITalentDeduction>();
        }
        public override string ToString()
        {
            var ret = string.Empty;
            ret += Name;

            if (!string.IsNullOrEmpty(NameExtension))
            {
                ret = ret + "(" + NameExtension.ToUpper(Helper.CultureInfo) + ")";
            }

            return ret;
        }
    }
}
