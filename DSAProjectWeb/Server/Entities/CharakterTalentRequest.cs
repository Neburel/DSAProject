using DSAProject2Web.Server.Entities;
using DSAProjectWeb.Server.Util;
namespace DSAProjectWeb.Server.Entities
{
    public class CharakterTalentRequest : CharakterIDRequest
    {
        public TalentTypeEnum TalentType { get; set; }
    }
}
