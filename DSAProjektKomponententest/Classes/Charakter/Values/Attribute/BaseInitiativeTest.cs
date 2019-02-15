using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
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
    public class BaseInitiativeTest : AbstractAttributeValuesTest
    {
        public override int ExpectedValue()
        {
            var var1 = Attribute.GetAttributMAXValue(DSAProject.CharakterAttribut.Mut, out Error error);
            ErrorHelper.ExpectErrorNull(error);
            var var2 = Attribute.GetAttributMAXValue(DSAProject.CharakterAttribut.Mut, out error);
            ErrorHelper.ExpectErrorNull(error);
            var var3 = Attribute.GetAttributMAXValue(DSAProject.CharakterAttribut.Intuition, out error);
            ErrorHelper.ExpectErrorNull(error);
            var var4 = Attribute.GetAttributMAXValue(DSAProject.CharakterAttribut.Gewandheit, out error);
            ErrorHelper.ExpectErrorNull(error);

            return (var1 + var2 + var3 + var4) / 5;
        }

        public override IValue GenerateValue()
        {
            return new BaseInitiative(Attribute);
        }
    }
}
