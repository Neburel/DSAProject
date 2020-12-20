using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Talente.TalentDeductions
{
    public class TalentDeductionFreeText : ITalentDeduction
    {
        public string Text { get; private set; } = string.Empty;

        public TalentDeductionFreeText(string text) 
        {
            Text = text;
        }

        public string GetDeductionString()
        {
            return Text;
        }
        public override string ToString()
        {
            return GetDeductionString();
        }
    }
}
