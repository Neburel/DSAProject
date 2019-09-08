using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente.TalentDeductions
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
    }
}
