
using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Talente.TalentRequirement
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
