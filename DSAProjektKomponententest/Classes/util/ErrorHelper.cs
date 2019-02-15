using DSAProject.util.ErrrorManagment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.Classes.util
{
    public static class ErrorHelper
    {
        public static void ExpectErrorNull(Error error)
        {
            if(error != null)
            {
                Assert.AreEqual(null, error, error.Message);
            }
        }
    }
}
