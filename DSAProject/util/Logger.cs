using DSALib;
using DSALib.Utils;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace DSAProject.util.ErrrorManagment
{
    public static class Logger
    {
        #region Variables
        private static readonly string fileName  = "Error";
        private static readonly string fileExtension = ".log";
        private static readonly string folder = "Logging";
        private static readonly bool isDebugging = true;

        #endregion
        public static void ListenLibLogger()
        {
            LogStrings.WriteLog += (sender, args) =>
            {
                Log(args + Environment.NewLine);
            };
            LogStrings.PrintString += (sender, args) =>
            {
                if (isDebugging)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " " + args + "\n");
                }
            };
        }
        public static void Log(LogLevel logLevel, DSAError error, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            if (error != null)
            {
                LogStrings.LogString(logLevel, error, callerMemberName, CallerFilePath, callerLineNumber);
            }
        }
        public static void Log(LogLevel logLevel, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            LogStrings.LogString(logLevel, message, callerMemberName, CallerFilePath, callerLineNumber);
        }
        private static void Log(string message)
        {
            var date = DateTime.Today.ToString("d").Replace(".", "");
            var fileString = Path.Combine(folder, fileName + "_" + date + fileExtension);
            FileManagment.FileManagment.WriteToFile(message, fileString, CreationCollisionOption.OpenIfExists, out DSAError error);
        }
    }
}
