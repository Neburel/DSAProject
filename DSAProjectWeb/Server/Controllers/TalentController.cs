using DSALib2.SQLDataBase;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSAProject2Web.Server.Entities;
using DSALib2.Classes.Import.Excel;
using Microsoft.Extensions.Configuration;
using DSALib2.Classes.JSONSave;
using System.Collections.Generic;
using DSALib2.Charakter.Talente;
using DSALib2.Classes.Tools;
using DSAProjectWeb.Server.ControllersBase;
using DSALib2.Classes.Charakter;
using System;
using DSALib2.Classes.Charakter.Talente.TalentGeneral;
using DSALib2.Interfaces.Charakter;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TalentController : CharakterDataBaseController
    {
        public TalentController(ApplicationContext context, IConfiguration configuration, ILogger<TalentController> logger) : base(context, configuration, logger) {
        }

        [Route("Set")]
        [HttpPost]
        public string GetTalentPhysical([FromBody] CharakterValueRequest request)
        {
            return GetTalent<TalentPhysical>(request);
        }


        private string GetTalent<T>(CharakterValueRequest request) where T : ITalent
        {
            var charakter = new DSASQLCharakter(Context, request.CharakterID, this.JsonTalentFile);
            var talentRepo = charakter.Talente;
            var list = talentRepo.GetViewList<T>();
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
               
        [Route("Test")]
        [HttpPost]
        public string Test()
        {
            return CreateResponse();
        }
    }
}
