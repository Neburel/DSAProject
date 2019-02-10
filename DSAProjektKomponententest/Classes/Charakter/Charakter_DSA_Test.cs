using DSAProject.Classes.Charakter;
using DSAProject.util.ErrrorManagment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.Charakter
{
    [TestClass]
    public class Charakter_DSA_Test
    {
        [TestMethod]
        public void BasisTestCreationAttribute()
        {
            var charakter_DSA = new Charakter_DSA();
            var attribute = charakter_DSA.Attribute;
            var list = attribute.UsedAttributs;

            Assert.AreEqual(9, list.Count);
            var error = new Error();

            foreach(var item in list)
            {
                Assert.AreEqual(attribute.GetAttributValue(item, out error), 0);
                CheckError(error);
            }
        }

        private void CheckError(Error error)
        {
            Assert.AreEqual(null, error);
        }
    }
}
