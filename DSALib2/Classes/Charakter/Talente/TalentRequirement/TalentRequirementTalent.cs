
using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Talente.TalentRequirement
{
    public class TalentRequirementTalent : ITalentRequirement
    {
        public int ReqNeed { get; private set; }
        public int ReqOff { get; private set; }
        public ITalent Talent { get; private set; }

        public TalentRequirementTalent(ITalent talent, int reqNeed, int reqoff = 0)
        {
            Talent = talent;
            ReqNeed = reqNeed;
            ReqOff = reqoff;
        }

        public string RequirementString()
        {
            var ret = string.Empty;

            if(ReqOff != 0)
            {
                ret = ReqOff.ToString(DSALib.Utils.Helper.CultureInfo) + "+" + " ";
            }
            ret = ret + Talent.Name;
            ret = ret + " " + ReqNeed;

            return ret;
        }
    }
}
