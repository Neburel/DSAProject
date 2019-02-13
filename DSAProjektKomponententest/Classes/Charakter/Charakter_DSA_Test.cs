using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
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
    public class Charakter_DSA_Test : AbstractCharakterTest
    {
        protected override ICharakter Charakter { get; set; }
        protected override ICharakter Charakter_Load { get; set; }

        protected override void GenerateProperties()
        {
            Charakter       = new Charakter_DSA();
            Charakter_Load  = new Charakter_DSA();
        }

        [TestMethod]
        public void BasisTestCreationAttribute()
        {
            var charakter_DSA   = new Charakter_DSA();
            var attribute       = charakter_DSA.Attribute;
            var list            = attribute.UsedAttributs;

            Assert.AreEqual(9, list.Count);
            var error = new Error();

            foreach(var item in list)
            {
                Assert.AreEqual(attribute.GetAttributAKTValue(item, out error), 0);
                CheckError(error);
            }
        }
    }
}
