using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private static void Log(string message)
        {
            var currentTime = DateTime.Now.ToString();
            message = currentTime + ": " + message + Environment.NewLine;
            WriteToFile(message);
        }
        public static void Log(LogLevel logLevel, string message)
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
        public static void Log(LogLevel logLevel, string message, string sendingClass, string sendingMethod)
        {
            Log(logLevel, "Klasse: " + sendingClass + " Methode: " + sendingMethod + " Nachricht: " + message + "\n");
        }
        private static void WriteToFile(string message)
        {
            var semaphoreSlim = new SemaphoreSlim(0);
            var task = new Task(async () =>
            {
                #region FileStringErstellen
                var date        = DateTime.Today.ToString("d").Replace(".", "");
                var fileString  = Path.Combine(folder, fileName + "_" + date + fileExtension);
                #endregion
                var file        = await GetFileAsync(fileString);
                await FileIO.AppendTextAsync(file, message);

                semaphoreSlim.Release();
            });
            task.Start();
            semaphoreSlim.Wait();
        }
        private static void DebugPrint(LogLevel logLevel, string message)
        {
            if (isDebugging)
            {
                System.Diagnostics.Debug.Print(DateTime.Now.ToString() + " " + logLevel.ToString() + ": " + message);
            }
        }
        private static async Task<StorageFile> GetFileAsync(string file)
        {

            var appInstalledFolder  = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var localFolder         = ApplicationData.Current.LocalFolder;

            System.Diagnostics.Debug.Print("Log File Folder Path: " + localFolder.Path.ToString() + "\n");

            var sfile       = await localFolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);
            return sfile;
        }
    }
}
