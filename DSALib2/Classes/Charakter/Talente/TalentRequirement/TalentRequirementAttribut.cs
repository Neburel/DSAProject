using DSALib2.Interfaces.Charakter;
using DSALib2.Utils;
using System;

namespace DSALib2.Classes.Charakter.Talente.TalentRequirement
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
