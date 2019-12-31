using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente.TalentDeductions
{
    public class TalentDeductionTalent : ITalentDeduction
    {
        public bool IgnoreValue { get; set; }
        public int Value { get; private set; }
        public ITalent Talent { get; private set; }
        
        public TalentDeductionTalent(ITalent talent, int value, int deductionBaseValue)
        {
            Value = value;
            Talent = talent;

            if(value == deductionBaseValue)
            {
                IgnoreValue = true;
            }
            else
            {
                IgnoreValue = false;
            }
        }

        public string GetDeductionString()
        {
            var ret = string.Empty;
            if (typeof(AbstractTalentFighting).IsAssignableFrom(Talent.GetType()))
            {
                ret = Talent.Name;
            }
            else
            {
                ret = Talent.ToString();
            }

            if (!IgnoreValue)
            {
                ret = ret + "(" + Value.ToString(DSALib.Utils.Helper.CultureInfo) + ")";
            }
            return ret;
        }
    }
}
