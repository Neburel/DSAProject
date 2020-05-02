using DSAProject.Classes.Interfaces;

namespace DSAProject.Classes.Charakter.Talente.TalentDeductions
{
    public class TalentDeductionTalent : ITalentDeduction
    {
        public bool IgnoreValue { get; set; }
        public int Value { get; private set; }
        public ITalent Talent { get; private set; }
        public string Description { get; private set; }
        
        public TalentDeductionTalent(ITalent talent, int value, int deductionBaseValue, string description = "")
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
            Description = description;
        }

        public string GetDeductionString()
        {
            var ret = string.Empty;
            if (Talent != null && typeof(AbstractTalentFighting).IsAssignableFrom(Talent.GetType()))
            {
                ret = Talent.Name;
            }           
            else if(Talent != null)
            {
                ret = Talent.ToString();
            }
            if (!string.IsNullOrEmpty(Description))
            {
                ret = ret + " " + "(" + Description + ")";
            }

            if (!IgnoreValue)
            {
                ret = ret + "(" + Value.ToString(DSALib.Utils.Helper.CultureInfo) + ")";
            }
            return ret;
        }
        public override string ToString()
        {
            return GetDeductionString();
        }
    }
}
