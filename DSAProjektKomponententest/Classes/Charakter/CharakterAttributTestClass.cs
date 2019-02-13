using DSAProject.Classes.Charakter;
using DSAProject.util.ErrrorManagment;
using DSAProjektKomponententest.Classes.Charakter.util;
using DSAProjektKomponententest.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.Charakter
{
    [TestClass]
    public class CharakterAttributTestClass
    {
        [TestMethod]
        public void BasisAttributTest()
        {
            var value                       = 5;
            var list                        = new List<CharakterAttribut> { CharakterAttribut.Charisma, CharakterAttribut.Fingerfertigkeit, CharakterAttribut.Gewandheit };
            CharakterAttribute attribute    = new CharakterAttribute(list);
            attribute.SetAttributAKTValue(CharakterAttribut.Charisma, value, out Error error);

            if (error != null)
            {
                Assert.Fail(ErrorHelper.AssertFailMessage(error, ""));  
            } 
            else
            {
                var x = attribute.GetAttributAKTValue(CharakterAttribut.Charisma, out error);

                if (error != null)
                {
                    Assert.Fail(ErrorHelper.AssertFailMessage(error, ""));
                } 
                else
                {
                    Assert.AreEqual(value, x);
                }
            }
        }
        [TestMethod]
        public void RandomGeneratetTest()
        {
            var dic         = Generator.GenerateCharakterAttributDictionary();
            var attribute   = new CharakterAttribute(dic.Keys.ToList());

            foreach(var key in dic.Keys)
            {
                attribute.SetAttributAKTValue(key, dic[key], out Error error);
                if (error != null)
                {
                    Assert.Fail(ErrorHelper.AssertFailMessage(error, ""));
                }
            }

            foreach (var key in dic.Keys)
            {
                var x = attribute.GetAttributAKTValue(key, out Error error);

                if (error != null)
                {
                    Assert.Fail(ErrorHelper.AssertFailMessage(error, ""));
                } 
                else
                {
                    Assert.AreEqual(dic[key], x);
                }
            }
        }
    }
}
