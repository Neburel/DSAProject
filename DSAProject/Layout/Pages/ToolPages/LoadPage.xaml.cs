﻿using DSALib;
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
        public ObservableCollection<JSON_Charakter> Items { get; } = new ObservableCollection<JSON_Charakter>();
        public LoadPage()
        {
            this.InitializeComponent();

            var items = new ObservableCollection<JSON_Charakter>();
            var cFileList = FileManagment.GetFilesDictionary(Game.CharakterSaveFolder, out Error error);
            foreach(var item in cFileList)
            {
                var file    = Path.Combine(Game.CharakterSaveFolder, item);
                var fileContent     = FileManagment.LoadTextFile(file, out error);
                var json_charakter = JSON_Charakter.DeSerializeJson(fileContent, out string errorstring);
                items.Add(json_charakter);
            }
            items.OrderBy(x => x.Name).ThenBy(x => x.SaveTime);
            Items = items;
        }
        private void XAML_LoadListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Error error             = null;
            var charakter            = (JSON_Charakter)e.ClickedItem;
            Game.LoadCharakter(charakter, out error);

            Game.RequestNav(new util.EventNavRequest { Side = NavEnum.StartPage });
        }
    }
}