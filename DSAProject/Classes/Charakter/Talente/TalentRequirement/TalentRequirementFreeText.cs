using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Talente.TalentRequirement
{
    public class TalentRequirementFreeText : ITalentRequirement
    {
        public string FreeText { get; private set; }


        public TalentRequirementFreeText(string freeText)
        {
            FreeText = freeText;
        }

        public string RequirementString()
        {
            return FreeText;
        }
    }
}
