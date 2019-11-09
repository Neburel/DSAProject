using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace DSALib.Utils
{
    public static class LogStrings
    {
        public static event EventHandler<string> WriteLog;
        public static event EventHandler<string> PrintString;

        public static List<string> Logs { get; private set; } = new List<string>();

        public static string GetLogString(LogLevel logLevel, Error error, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            return GetLogString(logLevel, error.ErrorCode.ToString() + " " + error.Message, callerMemberName, CallerFilePath, callerLineNumber);
        }
        public static string GetLogString(LogLevel logLevel, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var innerClass = Path.GetFileName(CallerFilePath);
            return GetLogString(logLevel, "Klasse: " + innerClass + " Methode: " + callerMemberName + " CallerLineNumber: " + callerLineNumber + " Nachricht: " + message);
        }
        private static string GetLogString(LogLevel logLevel, string message)
        {
            var currentTime = DateTime.Now.ToString();
            message = currentTime + ": " + message;

            if(logLevel == LogLevel.ActionLog || logLevel == LogLevel.ErrorLog)
            {
                Logs.Add(message);
                WriteLog?.Invoke(null, message);
            }
            PrintString?.Invoke(null, message);

            return message;
        }
    }

}

