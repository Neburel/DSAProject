using DSALib;
using DSALib.Classes.JSON;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using DSAProject.util.FileManagment;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LoadPage : Page
    {
        public ObservableCollection<JSON_CharakterMetaData> Items { get; } = new ObservableCollection<JSON_CharakterMetaData>();
        public LoadPage()
        {
            this.InitializeComponent();

            var fileList = FileManagment.GetFilesDictionary(Game.CharakterMetaFolder, out Error error);
            var items = new ObservableCollection<JSON_CharakterMetaData>();

            foreach(var item in fileList)
            {
                var fileString  = Path.Combine(Game.CharakterMetaFolder, item);

                if (error == null)
                {
                    var fileContent = FileManagment.LoadTextFile(fileString, out error);
                    if(error == null)
                    {
                        var metaInformationen = JSON_CharakterMetaData.DeSerializeJson(fileContent, out string serror);
                        if (serror == null)
                        {
                            items.Add(metaInformationen);
                        }
                    }
                }
            }
            items.OrderBy(x => x.Name).ThenBy(x => x.SaveTime);
            Items = items;
        }
        private void XAML_LoadListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Error error             = null;
            var metaData            = (JSON_CharakterMetaData)e.ClickedItem;
            
            Game.LoadCharakter(metaData, out error);

            


            //if (error != null)
            //{
            //    Logger.Log(LogLevel.ErrorLog, error.Message, nameof(LoadPage), nameof(XAML_LoadListView));
            //} 
            //else
            //{
            //    Game.Charakter = charakter;
            //    Game.GoStartPage();
            //}

        }
    }
}
