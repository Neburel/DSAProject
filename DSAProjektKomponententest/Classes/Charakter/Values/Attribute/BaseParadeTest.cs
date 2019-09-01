using DSALib.Classes.Charakter.Values.Attribute;
using DSALib.Classes.Interfaces;
using DSALib.util.ErrrorManagment;
using DSAProjektKomponententest.Classes.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.Charakter.Values.Attribute
{
    [TestClass]
    public class BaseParadeTest : AbstractAttributeValuesTest
    {
        public override int ExpectedValue()
        {
            var var1 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Intuition, out Error error);
            ErrorHelper.ExpectErrorNull(error);
            var var2 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Gewandheit, out error);
            ErrorHelper.ExpectErrorNull(error);
            var var3 = Attribute.GetAttributMAXValue(DSALib.CharakterAttribut.Körperkraft, out error);
            ErrorHelper.ExpectErrorNull(error);

            return (var1 + var2 + var3) / 5;
        }

        public override IValue GenerateValue()
        {
            return new BaseParade(Attribute);
        }
    }
}
