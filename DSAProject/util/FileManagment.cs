using DSALib;
using DSALib.Utils;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace DSAProject.util.FileManagment
{
    public static class FileManagment
    {
        public static void WriteToFile(string message, string fileString, CreationCollisionOption option, out Error error)
        {
            error = null;

            try
            {
                var semaphoreSlim = new SemaphoreSlim(0);
                var task = new Task(async () =>
                {
                    var file = await GetFileAsync(fileString, option);
                    await FileIO.AppendTextAsync(file, message);

                    semaphoreSlim.Release();
                });
                task.Start();
                semaphoreSlim.Wait();
            }
            catch(Exception ex)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
                Logger.Log(LogLevel.ErrorLog, "Schreiben in Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(fileString));
            }
        }
        public static string LoadTextFile(string fileString, out Error error)
        {
            error               = null;
            var retText         = string.Empty;
            var semaphoreSlim   = new SemaphoreSlim(0);

            try
            {
                var task = new Task(async () =>
                {
                    var file    = await GetFileAsync(fileString, CreationCollisionOption.OpenIfExists);
                    retText     = await FileIO.ReadTextAsync(file);
                    semaphoreSlim.Release();
                });
                task.Start();
                semaphoreSlim.Wait();
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
                Logger.Log(LogLevel.ErrorLog, "Laden aus einer Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(fileString));
            }
            return retText;
        }
        public static string LoadTextAssestFile(string fileString, out Error error)
        {
            error = null;
            var retText = string.Empty;
            var semaphoreSlim = new SemaphoreSlim(0);

            try
            {
                var task = new Task(async () =>
                {
                    var file    = await GetAssestFileAsync(fileString, CreationCollisionOption.OpenIfExists);
                    retText     = await FileIO.ReadTextAsync(file);
                    semaphoreSlim.Release();
                });
                task.Start();
                semaphoreSlim.Wait();
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
                Logger.Log(LogLevel.ErrorLog, "Laden aus einer Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(fileString));
            }

            return retText;
        }
        public static List<string> GetFilesDictionary(string dictionaryString, out Error error)
        {
            var ret = new List<string>();
            error   = null;

            try
            {
                Error innererror    = null;
                var semaphoreSlim   = new SemaphoreSlim(0);
                var task = new Task(async () =>
                {
                    try
                    {
                        var files = await GetFilesinFolderAsync(dictionaryString);
                        foreach (var item in files)
                        {
                            ret.Add(item.Name);
                        }
                    }
                    catch(Exception e)
                    {
                        innererror = new Error
                        {
                            ErrorCode = ErrorCode.Error,
                            Message = e.Message
                        };
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                });
                task.Start();
                semaphoreSlim.Wait();
                error = innererror;
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
                Logger.Log(LogLevel.ErrorLog, "Laden aus einer Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(GetFilesDictionary));
            }

            return ret;
        }
        public static List<string> GetAssestFilesDictionary(string dictionaryString, out Error error)
        {
            var ret = new List<string>();
            error = null;

            try
            {
                var semaphoreSlim = new SemaphoreSlim(0);
                var task = new Task(async () =>
                {
                    try
                    {
                        var files = await GetAssestFileAsync(dictionaryString);
                        foreach (var item in files)
                        {
                            ret.Add(item.Name);
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(LogLevel.ErrorLog, "Laden aus einer Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(GetAssestFilesDictionary));
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                });
                task.Start();
                semaphoreSlim.Wait();
            }
            catch(Exception ex)
            {
                error = new Error
                {
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
                Logger.Log(LogLevel.ErrorLog, "Laden aus einer Datei fehlgeschlagen " + ex.Message, nameof(FileManagment), nameof(GetAssestFilesDictionary));
            }

            return ret;
        }

        private static async Task<StorageFile> GetFileAsync(string file, CreationCollisionOption option)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var sfile       = await localFolder.CreateFileAsync(file, option);

            return sfile;
        }
        private static async Task<StorageFile> GetAssestFileAsync(string file, CreationCollisionOption option)
        {

            var localFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var sfile       = await localFolder.CreateFileAsync(file, option);

            return sfile;
        }

        public static void DeleteTextFile(string fileString)
        {
            var retText = string.Empty;
            var semaphoreSlim = new SemaphoreSlim(0);

            var task = new Task(async () =>
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var file = await localFolder.GetFileAsync(fileString);

                if(file != null)
                {
                    await file.DeleteAsync();
                }
                
                semaphoreSlim.Release();
            });
            task.Start();
            semaphoreSlim.Wait();
        }


        private static async Task<IReadOnlyList<StorageFile>> GetFilesinFolderAsync(string folder)
        {
            var localFolder = ApplicationData.Current.LocalFolder.Path;
            var sFolder     = await StorageFolder.GetFolderFromPathAsync(Path.Combine(localFolder, folder));
            var files       = await sFolder.GetFilesAsync();

            return files;
        }
        private static async Task<IReadOnlyList<StorageFile>> GetAssestFileAsync(string folder)
        {
            //var localizationDirectory = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Localization")

            var path        = Path.Combine("Assets", folder);
            var localFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(path);
            var files       = await localFolder.GetFilesAsync();

            return files;
        }
    }
}
