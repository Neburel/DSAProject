using DSALib;

using DSAProject.Classes.Interfaces;

using System;

namespace DSAProject.Classes.Charakter.Talente.TalentRequirement
{
    public class TalentRequirementAttribut : ITalentRequirement
    {
        public CharakterAttribut Attribut { get; private set; }
        public int AttributValue { get; private set; }

        public TalentRequirementAttribut(CharakterAttribut attribut, int value)
        {
            Attribut = attribut;
            AttributValue = value;
        }

        public string RequirementString()
        {
            throw new NotImplementedException();
        }
    }
}
