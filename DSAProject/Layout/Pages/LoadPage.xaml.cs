using DSAProject.Classes.Charakter;
using DSAProject.Classes.Game;
using DSAProject.util.FileManagment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LoadPage : Page
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();
        public LoadPage()
        {
            this.InitializeComponent();

            var fileList = FileManagment.GetFilesDictionary(Game.CharakterSaveFolder, out util.ErrrorManagment.Error error);
            foreach(var item in fileList)
            {
                Items.Add(item);
            }
        }
        private void XAML_LoadListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var x = (string)e.ClickedItem;

            Game.Charakter = new CharakterDSA();
            Game.Charakter.Load(x, out util.ErrrorManagment.Error error);

            if(error != null)
            {
                Game.GoStartPage();
            }
        }
    }
}
