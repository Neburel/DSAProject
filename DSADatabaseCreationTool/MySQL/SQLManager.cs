using DSADatabaseCreationTool.DSATalenteTableAdapters;
using DSALib.Classes.JSON;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSADatabaseCreationTool.MySQL
{
    public class SQLManager
    {
        private SqlConnection dsaDB;
        private DSATalenteTableAdapter dSATalenteTableAdapter;
        public SQLManager()
        {
            dSATalenteTableAdapter  = new DSATalenteTableAdapter();
            dsaDB                   = dSATalenteTableAdapter.Connection;
            dsaDB                   = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\04_Projekte\\DSAProject\\DSADatabaseCreationTool\\DSADatabase.mdf;Integrated Security=True");
        }

        public void InsertTalent(JSON_Talent talent, out string error)
        {
            error = null;
            //Exestiert das Talent bereits
            try
            {
                if(dsaDB.State != System.Data.ConnectionState.Open)
                {
                    dsaDB.Open();
                }
                var talentTable = new DSATalente.DSATalenteDataTable();
                dSATalenteTableAdapter.Fill(talentTable);

                var existGUID = talentTable.Where(x => !x.IsGUIDNull()).Where(x => x.GUID == talent.ID.ToString()).ToList();
                if(existGUID.Count > 0)
                {
                    error = "Das Talent exestiert bereits";
                }
                else
                {
                    SqlCommand command      = dsaDB.CreateCommand();
                    command.CommandText     = "INSERT INTO DSATalente(GUID, Name, Be) VALUES(@guid, @name, @be)";
                    command.Parameters.AddWithValue("@guid", talent.ID);
                    command.Parameters.AddWithValue("@name", talent.ID);
                    command.Parameters.AddWithValue("@be", talent.ID);
                    var k = command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                dsaDB.Close();
            }
        }
    }
}
