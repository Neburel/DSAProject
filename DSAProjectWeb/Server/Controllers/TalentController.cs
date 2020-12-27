using DSALib2.SQLDataBase;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSAProject2Web.Server.Entities;
using DSALib2.Classes.Import.Excel;
using Microsoft.Extensions.Configuration;
using DSALib2.Classes.JSONSave;
using DSALib2.Classes.Tools;
using DSAProjectWeb.Server.ControllersBase;
using DSALib2.Classes.Charakter.Talente.TalentGeneral;
using DSALib2.Interfaces.Charakter;
using DSALib2.Classes.Charakter.View;
using DSAProjectWeb.Server.Entities;
using DSAProjectWeb.Server.Util;
using DSALib2.Classes.Charakter.Talente.TalentFighting;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TalentController : CharakterDataBaseController
    {
        public TalentController(ApplicationContext context, IConfiguration configuration, ILogger<TalentController> logger) : base(context, configuration, logger) { }

        [Route("GetList")]
        [HttpPost]
        public string GetTalentList([FromBody] CharakterTalentRequest request)
        {   
            if(request.TalentType == TalentTypeEnum.Close)
            {
                return GetTalent<TalentClose>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Crafting)
            {
                return GetTalent<TalentCrafting>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Knowldage)
            {
                return GetTalent<TalentKnowldage>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Nature)
            {
                return GetTalent<TalentNature>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Physical)
            {
                return GetTalent<TalentPhysical>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Range)
            {
                return GetTalent<TalentRange>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Social)
            {
                return GetTalent<TalentSocial>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Weaponless)
            {
                return GetTalent<TalentWeaponless>(request);
            }
            throw new System.Exception("Ungültige ID");
        }
        [Route("GetLanguageList")]
        [HttpPost]
        public string GetTalentList([FromBody] CharakterIDRequest request)
        {
            var charakter = GetDSASQLCharakter(request);
            var talentRepo = charakter.Talente;
            var list = talentRepo.GetViewList();
            return CreateResponse(list);
        }
        [Route("Set")]
        [HttpPost]
        public string SetTalentView([FromBody] CharakterDataRequest<TalentView> request)
        {
            var charakter   = GetDSASQLCharakter(request);
            var talentRepo  = charakter.Talente;
            talentRepo.SetTalentbyView(request.Data);

            var viewItem = talentRepo.GetView(request.Data.ID);
            return CreateResponse(viewItem);
        }
        [Route("SetLanguage")]
        [HttpPost]
        public string SetLanguageView([FromBody] CharakterDataRequest<LanguageView> request)
        {
            var charakter = GetDSASQLCharakter(request);
            var talentRepo = charakter.Talente;
            talentRepo.SetTalentbyView(request.Data);

            var viewItem = talentRepo.GetLanguageView(request.Data.IDSprache, request.Data.IDSchrift);
            return CreateResponse(viewItem);
        }

        private string GetTalent<T>(CharakterIDRequest request) where T : ITalent
        {
            var charakter       = GetDSASQLCharakter(request);
            var talentRepo      = charakter.Talente;
            var list            = talentRepo.GetViewList<T>();
            return CreateResponse(list);
        }

        [Route("Import")]
        [HttpPost]
        public string Import([FromBody]TalentImportRequest request)
        {
            var jsonFileTest = System.IO.File.ReadAllText(this.JsonTalentFile);
            var oldJsonFile = JSONTalentSaveFile.DeSerializeJson(jsonFileTest, out string error);
            var jsonTalentList = TalentJsonLoader.LoadTalent(oldJsonFile.Talente);
                       
            var importer = new ExcelImporter();
            var importResult = importer.ExcelImport(
                close: request.Close,
                range: request.Range,
                weaponless: request.Weaponless,
                crafting: request.Crafting,
                knowldage: request.Knowldage,
                nature: request.Nature,
                physical: request.Physical,
                social: request.Social,
                language: request.Language);

            TalentMerger.Test(importResult.TalentList, jsonTalentList, importResult.OldNameDictionary);

            return CreateResponse();
        }
    }
}
