using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace DSALib.Utils
{
    public static class LogStrings
    {
        public static string GetLogString(Error error, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            return GetLogString(error.ErrorCode.ToString() + " " + error.Message, callerMemberName, CallerFilePath, callerLineNumber);
        }
        public static string GetLogString(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var innerClass = Path.GetFileName(CallerFilePath);
            return GetLogString("Klasse: " + innerClass + " Methode: " + callerMemberName + " CallerLineNumber: " + callerLineNumber + " Nachricht: " + message);
        }
        private static string GetLogString(string message)
        {
            var currentTime = DateTime.Now.ToString();
            message = currentTime + ": " + message;

            return message;
        }
    }

}

