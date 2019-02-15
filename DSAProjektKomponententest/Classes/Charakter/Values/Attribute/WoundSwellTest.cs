﻿using DSAProject.Classes.Charakter.Values.Attribute;
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
    public class WoundSwellTest : AbstractAttributeValuesTest
    {
        public override int ExpectedValue()
        {
            var var1 = Attribute.GetAttributMAXValue(DSAProject.CharakterAttribut.Konstitution, out Error error);
            ErrorHelper.ExpectErrorNull(error);

            return (var1) / 2;
        }

        public override IValue GenerateValue()
        {
            return new WoundSwell(Attribute);
        }
    }
}
