using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using DSAProjektKomponententest.Classes.Charakter.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.Charakter
{
    [TestClass]
    public abstract class AbstractCharakterTest
    {
        #region Properties
        protected abstract ICharakter Charakter { get; set; }
        protected abstract ICharakter Charakter_Load { get; set; }
        #endregion
        [TestInitialize]
        public void TestInitialize()
        {
            GenerateProperties();
        }
        [TestMethod]
        public void AttributeSettingTest()
        {
            var dic = Generator.GenerateCharakterAttributDictionary(Charakter.Attribute.UsedAttributs);

            foreach (var item in dic.Keys)
            {
                Charakter.Attribute.SetAttributAKTValue(item, dic[item], out Error error);
                CheckError(error);
                var currentValue = Charakter.Attribute.GetAttributAKTValue(item, out error);
                CheckError(error);
                Assert.AreEqual(dic[item], currentValue);
            }
        }
        [TestMethod]
        public void SaveLoadAttributeTest()
        {
            Error error     = null;
            var fileName    = "TestSave";
            var dic         = Generator.GenerateCharakterAttributDictionary(Charakter.Attribute.UsedAttributs);

            foreach (var item in dic.Keys)
            {
                Charakter.Attribute.SetAttributAKTValue(item, dic[item], out error);
                CheckError(error);
                var currentValue = Charakter.Attribute.GetAttributAKTValue(item, out error);
                CheckError(error);
                Assert.AreEqual(dic[item], currentValue);
            }
            Charakter.Save(fileName, out error);
            CheckError(error);
            Charakter_Load.Load(fileName, out error);
            CheckError(error);

            foreach (var item in dic.Keys)
            {
                var currentValue = Charakter_Load.Attribute.GetAttributAKTValue(item, out error);
                CheckError(error);
                Assert.AreEqual(dic[item], currentValue);
            }
        }
        #region Hilfsmethoden
        protected void CheckError(Error error)
        {
            Assert.AreEqual(null, error);
        }
        protected abstract void GenerateProperties();
        #endregion
    }
}
