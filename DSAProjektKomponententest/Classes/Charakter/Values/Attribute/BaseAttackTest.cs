using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSALib.Classes.Charakter.Values.Attribute;
using DSALib.Classes.Interfaces;
using DSALib.util.ErrrorManagment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSAProjektKomponententest.Classes.Charakter.Values.Attribute
{
    [TestClass]
    public class BaseAttackTest : AbstractAttributeValuesTest
    {
        public override int ExpectedValue()
        {
            var var1 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Mut, out Error error);
            Assert.AreEqual(error, null);
            var var2 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Gewandheit, out error);
            Assert.AreEqual(error, null);
            var var3 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Körperkraft, out error);
            Assert.AreEqual(error, null);

            return (var1 + var2 + var3) / 5;
        }

        public override IValue GenerateValue()
        {
            return new BaseAttack(Attribute);
        }
    }
}
