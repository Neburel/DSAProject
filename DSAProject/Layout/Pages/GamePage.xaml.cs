using DSAProject.Classes.Charakter;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace DSAProject
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        #region Pages
        private string currentPage      = string.Empty;        
        #endregion
        public GamePage()
        {
            this.InitializeComponent();
            currentPage = "HeroLetter";
            ContentFrame.Navigate(typeof(HeroLetterPage));

            Game.StartPage += (sender, args) =>
            {
                currentPage = "HeroLetter";
                ContentFrame.Navigate(typeof(HeroLetterPage));
            };
        }
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }
        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

        }
        private void NavView_Navigate(NavigationViewItem item)
        {
            if(currentPage != (string)item.Tag)
            {
                switch (item.Tag)
                {
                    case "HeroLetter":
                        ContentFrame.Navigate(typeof(HeroLetterPage));
                        currentPage = "HeroLetter";
                        break;
                    case "create":
                        ContentFrame.Navigate(typeof(CharakterCreation));
                        currentPage = "create";
                        break;
                    case "save":
                        Game.Charakter.Save("Test", out util.ErrrorManagment.Error error);
                        SaveDialog.ShowDialog(error);
                        break;
                    case "load":
                        ContentFrame.Navigate(typeof(LoadPage));
                        currentPage = "load";
                        break;
                }
            }
        }
    }
}
