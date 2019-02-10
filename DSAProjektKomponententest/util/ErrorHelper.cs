using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProjektKomponententest.util
{
    public static class ErrorHelper
    {
        public static string AssertFailMessage(Error error, string message)
        {
            return "Es ist ein Fehler aufgetreten. " + error.ErrorCode.ToString() + " " + error.Message;
        }
    }
}
