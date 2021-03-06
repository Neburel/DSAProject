﻿using DSALib.Classes.Interfaces;
using DSALib.util.ErrrorManagment;
using DSAProjektKomponententest.Classes.Charakter.util;
using DSAProjektKomponententest.Classes.util;
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
                ErrorHelper.ExpectErrorNull(error);
                var currentValue = Charakter.Attribute.GetAttributAKTValue(item, out error);
                ErrorHelper.ExpectErrorNull(error);
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
                ErrorHelper.ExpectErrorNull(error);
                var currentValue = Charakter.Attribute.GetAttributAKTValue(item, out error);
                ErrorHelper.ExpectErrorNull(error);
                Assert.AreEqual(dic[item], currentValue);
            }
            Charakter.Save(fileName, out error);
            ErrorHelper.ExpectErrorNull(error);
            Charakter_Load.Load(fileName, out error);
            ErrorHelper.ExpectErrorNull(error);

            foreach (var item in dic.Keys)
            {
                var currentValue = Charakter_Load.Attribute.GetAttributAKTValue(item, out error);
                ErrorHelper.ExpectErrorNull(error);
                Assert.AreEqual(dic[item], currentValue);
            }
        }
        #region Hilfsmethoden
        protected abstract void GenerateProperties();
        #endregion
    }
}
