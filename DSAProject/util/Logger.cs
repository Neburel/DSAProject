﻿using DSALib;
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
        private static readonly bool isDebugging = true;
        private static readonly string fileName  = "Error";
        private static readonly string fileExtension = ".log";
        private static readonly string folder = "Logging";
        #endregion
        public static void Log(Error error, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            if(error != null)
            {
                Log(LogLevel.ErrorLog, LogStrings.GetLogString(error, callerMemberName, CallerFilePath, callerLineNumber));
            }
        }
        public static void Log(LogLevel logLevel, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string CallerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(logLevel, LogStrings.GetLogString(message, callerMemberName, CallerFilePath, callerLineNumber));
        }
        private static void Log(LogLevel logLevel, string message)
        {
            if (logLevel == LogLevel.ErrorLog)
            {
                Log(logLevel.ToString() + " " + message);
            }
            else if (logLevel == LogLevel.ActionLog)
            {
                Log(logLevel.ToString() + " " + message);
            }
            DebugPrint(logLevel, message);
        }
        private static void Log(string message)
        {
            //var currentTime = DateTime.Now.ToString();
            //message = currentTime + ": " + message + Environment.NewLine;
            var date = DateTime.Today.ToString("d").Replace(".", "");
            var fileString = Path.Combine(folder, fileName + "_" + date + fileExtension);

            FileManagment.FileManagment.WriteToFile(message, fileString, CreationCollisionOption.OpenIfExists, out Error error);
        }
        private static void DebugPrint(LogLevel logLevel, string message)
        {
            if (isDebugging)
            {
                System.Diagnostics.Debug.Print(DateTime.Now.ToString() + " " + logLevel.ToString() + ": " + message + "\n");
            }
        }
      
    }
}