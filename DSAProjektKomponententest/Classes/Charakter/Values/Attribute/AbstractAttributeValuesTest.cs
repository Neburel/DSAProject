using DSALib.Classes.Charakter;
using DSALib.Classes.Interfaces;
using DSALib.util.ErrrorManagment;
using DSAProjektKomponententest.Classes.Charakter.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.Charakter.Values.Attribute
{
    [TestClass]
    public abstract class AbstractAttributeValuesTest
    {
        #region Properties
        protected CharakterAttribute Attribute { get; private set; }
        protected IValue Value { get; private set; }
        #endregion
        [TestInitialize]
        public void TestInitialize()
        {
            //Attribute werden nicht getestet, dazu exestieren andere Klasen
            var attributedic    = Generator.GenerateCharakterAttributDictionary(); 
            Attribute           = new CharakterAttribute(attributedic.Keys.ToList());
            foreach(var item in attributedic.Keys)
            {
                Attribute.SetAttributAKTValue(item, attributedic[item], out Error error);
                Assert.AreEqual(error, null);
            }
            Value = GenerateValue();
        }
        public abstract IValue GenerateValue();
        public abstract int ExpectedValue();
        [TestMethod]
        public void BaseTest()
        {
            Assert.AreEqual(ExpectedValue(), Value.Value);
        }
    }
}
