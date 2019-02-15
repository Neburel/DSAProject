using DSAProject;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.JSON
{
    [TestClass]
    public class JSON_Charakter_Test
    {
        [TestMethod]
        public void Simple_JSON_Charakter_TestAttribute()
        {
            int exceptedValue = 10;
            JSON_Charakter jSON_Charakter = new JSON_Charakter
            {
                AttributeBaseValue = new Dictionary<CharakterAttribut, int>()
            };
            jSON_Charakter.AttributeBaseValue.Add(CharakterAttribut.Charisma, exceptedValue);

            var content = jSON_Charakter.JSONContent;

            var newValue = JSON_Charakter.DeSerializeJson(content, out string error);
            newValue.AttributeBaseValue.TryGetValue(CharakterAttribut.Charisma, out int currentvalue);

            Assert.AreEqual(exceptedValue, currentvalue);
        }
    }
}
