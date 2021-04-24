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
using DSALib2.Classes.Charakter.Talente;
using System.Collections.Generic;

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
            if (request.TalentType == TalentTypeEnum.Close)
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
            else if (request.TalentType == TalentTypeEnum.General)
            {
                return GetTalent<AbstractTalentGeneral>(request);
            }
            else if(request.TalentType == TalentTypeEnum.Language)
            {
                return GetTalent<AbstractTalentLanguage>(request);
            }
            throw new System.Exception("Ungültige ID");
        }
        [Route("GetViewList")]
        [HttpPost]
        public string GetTalentViewList([FromBody] CharakterTalentRequest request)
        {   
            if(request.TalentType == TalentTypeEnum.Close)
            {
                return GetTalentView<TalentClose>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Crafting)
            {
                return GetTalentView<TalentCrafting>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Knowldage)
            {
                return GetTalentView<TalentKnowldage>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Nature)
            {
                return GetTalentView<TalentNature>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Physical)
            {
                return GetTalentView<TalentPhysical>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Range)
            {
                return GetTalentView<TalentRange>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Social)
            {
                return GetTalentView<TalentSocial>(request);
            }
            else if (request.TalentType == TalentTypeEnum.Weaponless)
            {
                return GetTalentView<TalentWeaponless>(request);
            }
            throw new System.Exception("Ungültige ID");
        }
        [Route("GetLanguageList")]
        [HttpPost]
        public string GetLanguageList([FromBody] CharakterIDRequest request)
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
            talentRepo.SetbyView(request.Data);

            var viewItem = talentRepo.GetView(request.Data.ID);
            return CreateResponse(viewItem);
        }
        [Route("SetLanguage")]
        [HttpPost]
        public string SetLanguageView([FromBody] CharakterDataRequest<LanguageView> request)
        {
            var charakter = GetDSASQLCharakter(request);
            var talentRepo = charakter.Talente;
            talentRepo.SetbyView(request.Data);

            var viewItem = talentRepo.GetLanguageView(request.Data.IDSprache, request.Data.IDSchrift);
            return CreateResponse(viewItem);
        }

        private string GetTalent<T>(CharakterIDRequest request) where T : ITalent
        {
            var charakter = GetDSASQLCharakter(request);
            var talentRepo = charakter.Talente;
            var list = talentRepo.GetList<T>();
            return CreateResponse(list);
        }
        private string GetTalentView<T>(CharakterIDRequest request) where T : ITalent
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

            var result = TalentMerger.Test(importResult.TalentList, jsonTalentList, importResult.OldNameDictionary);
            var list = new List<JSONTalent>();
            foreach(var item in result)
            {
                var talent = TalentHelper.CreateJSON(item);
                list.Add(talent);
                talent.JSONContent = null;
            }

            var languageList = new List<JSONTalentLanguageFamily>();
            foreach(var item in importResult.LanguageFamilyList)
            {
                var jsonLanguageFamilie = new JSONTalentLanguageFamily();
                jsonLanguageFamilie.Languages = new Dictionary<int, System.Guid>();
                jsonLanguageFamilie.Writings = new Dictionary<int, System.Guid>();
                jsonLanguageFamilie.Name = item.Name;

                foreach(var langaugeItem in item.Languages)
                {
                    if(langaugeItem.Value != null) { 
                        jsonLanguageFamilie.Languages.Add(langaugeItem.Key, langaugeItem.Value.ID);
                    }
                }
                foreach (var writingItems in item.Writings)
                {
                    if(writingItems.Value != null) { 
                        jsonLanguageFamilie.Writings.Add(writingItems.Key, writingItems.Value.ID);
                    }
                }
                languageList.Add(jsonLanguageFamilie);

            }

            var saveFile = new JSONTalentSaveFile();
            saveFile.Talente    = list;
            saveFile.Families = languageList;

            //Clear JSONContent
            saveFile.JSONContent = null;
            

            return CreateResponse(saveFile.JSONContent);
        }
    }
}
