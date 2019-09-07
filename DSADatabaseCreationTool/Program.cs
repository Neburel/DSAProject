using DSADatabaseCreationTool.MySQL;
using DSADatabaseCreationTool.Util;
using DSALib.Classes.JSON;
using DSALib.Utils;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DSADatabaseCreationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = LogStrings.GetLogString("Hallo");

            return;

            var jsonConent  = EmbeddedResources.LoadTextResource(EmbedddedRes.JSONTalentFiles, "Talente_01092019.json");
            var jsonFile    = JSON_TalentSaveFile.DeSerializeJson(jsonConent, out string error);

            foreach(var talent in jsonFile.Talente_DSA)
            {
                new SQLManager().InsertTalent(talent, out error);
                if(error != null)
                {
                    Console.WriteLine(error);
                }
            }

            return;



            var talentAdapter       = new DSATalenteTableAdapters.DSATalenteTableAdapter();
            var connection          = talentAdapter.Connection;
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DSADatabase.mdf;Integrated Security=True");
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\04_Projekte\\DSAProject\\DSADatabaseCreationTool\\DSADatabase.mdf;Integrated Security=True");

            var talentTable         = new DSATalente.DSATalenteDataTable();
            var talentdataset       = new DSATalente();

            try
            {
                connection.Open();
                talentAdapter.Fill(talentTable);
                talentAdapter.Insert("Hallo", "Test", "Test");

                var talentsWhoNeedAnInit = talentTable.Where(x => x.IsGUIDNull()).ToList();
                foreach(var talentItem in talentsWhoNeedAnInit)
                {
                    var check = true;
                    while (check)
                    {
                        var newGuid = Guid.NewGuid().ToString();
                        var guidExist = talentTable.Where(x => !x.IsGUIDNull()).Where(x => x.GUID == newGuid).ToList();
                        if (guidExist.Count == 0)
                        {
                            check = false;
                            talentItem.GUID     = newGuid;
                            //talentAdapter.Update(talentItem);
                            //talentAdapter.Fill(talentTable);

                            SqlCommand command = connection.CreateCommand();
                            command.CommandText = "UPDATE DSATalente SET GUID = @guid WHERE ID = @id";
                            command.Parameters.AddWithValue("@guid", talentItem.GUID);
                            command.Parameters.AddWithValue("@id", talentItem.Id);
                            var k = command.ExecuteNonQuery();
                        }
                    }
                }


                /*
                foreach (DSATalente.DSATalenteRow item in talentTable.Rows)
                {
                    var id      = item["ID"];
                    var name    = item["Name"];
                    var guid    = item["GUID"];

                    
                }
                */

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                connection.Close();
            }
            while (true) { };

        }
    }
}
