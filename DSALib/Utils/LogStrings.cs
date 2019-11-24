using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace DSALib.Utils
{
    /// <summary>
    /// Klasse die fehler in der Lib Aufzeichnet, die keine Exceptions werfen können damit kein Abbruch erzeugt wird.
    /// Die Fehler können über die Events abgefangen und Geschrieben werden
    /// </summary>
    public static class LogStrings
    {
        public static event EventHandler<string> WriteLog;
        public static event EventHandler<string> PrintString;

        /// <summary>
        /// Die Klasse Sammelt alle Logs die über diese Klasse geschrieben werden
        /// </summary>
        public static List<string> Logs { get; private set; } = new List<string>();

        public static void LogString(LogLevel logLevel, Error error, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            LogString(logLevel, error.ErrorCode.ToString() + " " + error.Message, callerMemberName, CallerFilePath, callerLineNumber);
        }
        public static void LogString(LogLevel logLevel, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var innerClass = Path.GetFileName(CallerFilePath);
            LogString(logLevel, "Klasse: " + innerClass + " Methode: " + callerMemberName + " CallerLineNumber: " + callerLineNumber + " Nachricht: " + message);
        }
        private static void LogString(LogLevel logLevel, string message)
        {
            var currentTime = DateTime.Now.ToString();
            message = currentTime + ": " + message;

            if(logLevel == LogLevel.ActionLog || logLevel == LogLevel.ErrorLog)
            {
                Logs.Add(message);
                WriteLog?.Invoke(null, message);
            }
            PrintString?.Invoke(null, message);
        }
    }

}

