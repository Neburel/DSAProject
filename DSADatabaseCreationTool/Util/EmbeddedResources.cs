using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DSADatabaseCreationTool.Util
{
    public enum EmbedddedRes
    {
        JSONTalentFiles
    }

    public static class EmbeddedResources
    {
        public static string LoadTextResource(EmbedddedRes res, string ResourceName)
        {
            string result       = string.Empty;
            var assembly        = Assembly.GetExecutingAssembly();
            var resourceName    = "DSADatabaseCreationTool.Resources.";
            
            if(res == EmbedddedRes.JSONTalentFiles)
            {
                resourceName = resourceName + "TalentFiles.";
            }
            resourceName = resourceName + ResourceName;
            //"DSADatabaseCreationTool.Resources.TalentFiles.Talente_01092019.json"

            if (AllResources().Contains(resourceName))
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }


        public static List<string> AllResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var k = assembly.GetManifestResourceNames().ToList();
            return k;
        }

    }
}
