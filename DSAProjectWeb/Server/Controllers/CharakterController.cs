using DSALib2.Classes.Charakter;
using DSALib2.Classes.Charakter.Repository.SQL;
using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSALib2.Utils;
using DSAProject2Web.Server.Entities;
using DSAProjectWeb.Server.ControllersBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharakterController : CharakterDataBaseController
    {
        public CharakterController(ApplicationContext context, IConfiguration configuration, ILogger<TalentController> logger) : base(context, configuration, logger) { }

        [Route("Create")]
        [HttpPost]
        public string HttpRequest()
        {
            var repo        = new SQLCharakterRepository(Context);
            var charakter   = repo.CreateDSACharakter(Context, "New Charakter");
            
            var abstractcharakter = GetDSASQLCharakter(charakter.Id);

            //Attribute Default
            var attributRepo = new SQLAttributRepository(Context, abstractcharakter, charakter.Id);

            var attributList = new List<CharakterAttribut>
            {
                CharakterAttribut.Mut,
                CharakterAttribut.Klugheit,
                CharakterAttribut.Intuition,
                CharakterAttribut.Charisma,
                CharakterAttribut.Fingerfertigkeit,
                CharakterAttribut.Gewandheit,
                CharakterAttribut.Konstitution,
                CharakterAttribut.Körperkraft,
                CharakterAttribut.Sozialstatus
            };
            foreach (var attribut in attributList)
            {
                attributRepo.SetAKT(attribut, 9);
            }


            //Test Charakter
            var talentListFighting = abstractcharakter.Talente.GetViewList<AbstractTalentFighting>();
            var talentListGeneral= abstractcharakter.Talente.GetViewList<AbstractTalentGeneral>();

            var talent1 = talentListFighting[5];
            talent1.TAW = 4;
            talent1.PA = 4;
            talent1.AT = 5;
            talent1.BL = 6;

            var talent2 = talentListGeneral[5];
            talent2.TAW = 19;
            talent2.AT = 19;

            var newTrait = abstractcharakter.Traits.GetEmptyView();
            newTrait.Name = "TestItem";
            newTrait.Description = "Dies ist ein Automatisch angelegter Testeintrag";
            newTrait.APGain = 20;
            newTrait.APInvest = 20;
            newTrait.GP = "0";
            newTrait.Value = "x";
            newTrait.Type = TraitType.Keiner;
            newTrait.AttributList[0].Value = 10;
            newTrait.AttributList[7].Value = 8;
            newTrait.ResourceList[0].Value = 1;
            newTrait.ResourceList[4].Value = 3;
            newTrait.ValueList[0].Value = 4;
            newTrait.ValueList[2].Value = 2;
            newTrait.TalentList.Add(talent1);
            newTrait.TalentList.Add(talent2);

            abstractcharakter.Traits.SetByView(newTrait);
          

            return CreateResponse(charakter);
        }

        [Route("GetList")]
        [HttpPost]
        public string GetList()
        {
            var repo = new SQLCharakterRepository(Context);
            return CreateResponse(repo.GetList());
        }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody]CharakterIDRequest request)
        {
            var repo = new SQLCharakterRepository(Context);
            repo.SetCharakter(request.CharakterID);
            return CreateResponse(repo.GetList());
        }
    }
}
