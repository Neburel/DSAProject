using DSALib.Classes.JSON;
using DSALib.Utils;
using DSAProject.Classes.Game;
using DSAProject.Layout.MessageDialoge;
using DSAProject.util.FileManagment;

using System;
using System.Collections.Generic;
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
        private const string CHARNAME = "Namenlos";
        Dictionary<JSONCharakter, bool> charGivenName = new Dictionary<JSONCharakter, bool>();
        public ObservableCollection<JSONCharakter> Items { get; } = new ObservableCollection<JSONCharakter>();
        public LoadPage()
        {
            this.InitializeComponent();

            var items = new ObservableCollection<JSONCharakter>();
            var cFileList = FileManagment.GetFilesDictionary(Game.CharakterSaveFolder, out Error error);
            foreach(var item in cFileList)
            {
                var file            = Path.Combine(Game.CharakterSaveFolder, item);
                var fileContent     = FileManagment.LoadTextFile(file, out error);
                var json_charakter  = JSONCharakter.DeSerializeJson(fileContent, out string errorstring);
                if (string.IsNullOrEmpty(json_charakter.Name))
                {
                    json_charakter.Name = CHARNAME;
                    charGivenName.Add(json_charakter, true);
                }
                else
                {
                    charGivenName.Add(json_charakter, false);
                }

                items.Add(json_charakter);
            }
            items.OrderBy(x => x.Name).ThenBy(x => x.SaveTime);
            Items = items;
        }
        private void XAML_LoadListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Error error             = null;
            var charakter            = (JSONCharakter)e.ClickedItem;
            charGivenName.TryGetValue(charakter, out bool givenName);

            if (givenName)
            {
                charakter.Name = string.Empty;
            }

            Game.LoadCharakter(charakter, out error);
            Game.RequestNav(new DSAProject.util.EventNavRequest { Side = NavEnum.StartPage });
        }

        private async void PlusButton_Clicked(object sender, object e)
        {
            var guid = (Guid)((ItemPages.PlusButton)sender).Tag;
            var jChar = Items.Where(x => x.ID == guid).FirstOrDefault();
            if(jChar != null)
            {
                var result = await DeleteCharakterDialog.ShowDialog(jChar);
                if (result)
                {
                    Items.Remove(jChar);
                    var pFolder = Path.Combine(Game.CharakterSaveFolder, jChar.ID.ToString() + ".save");
                    FileManagment.DeleteTextFile(pFolder);
                }
            }
        }
    }
}
