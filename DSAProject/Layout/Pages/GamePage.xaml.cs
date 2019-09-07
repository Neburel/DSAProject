using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages;
using DSAProject.Layout.Pages.ToolPages;
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
            var y = args.InvokedItem;
            
            if(args.InvokedItem.GetType() == typeof(string))
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
            else
            {
                NavView_Navigate(args.InvokedItem as NavigationViewItem);
            }

        }
        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

        }
        private void NavView_Navigate(NavigationViewItem item)
        {
            TalentPage page;

            if(currentPage != (string)item.Tag)
            {
                switch (item.Tag)
                {
                    case "HeroLetter":
                        ContentFrame.Navigate(typeof(HeroLetterPage));
                        currentPage = "HeroLetter";
                        break;
                    case "TalentWeaponless":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentWeaponless)).ToList());
                        currentPage = "TalentWeaponless";
                        break;
                    case "TalentClose":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentClose)).ToList());
                        currentPage = "TalentClose";
                        break;
                    case "TalentRange":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentRange)).ToList());
                        currentPage = "TalentRange";
                        break;
                    case "PhysicalTalent":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentPhysical)).ToList());
                        currentPage = "PhysicalTalent";
                        break;
                    case "SocialTalent":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentSocial)).ToList());
                        currentPage = "SocialTalent";
                        break;
                    case "NatureTalent":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentNature)).ToList());
                        currentPage = "NatureTalent";
                        break;
                    case "KnowldageTalent":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentKnowldage)).ToList());
                        currentPage = "KnowldageTalent";
                        break;
                    case "CraftingTalent":
                        ContentFrame.Navigate(typeof(TalentPage));
                        page = (TalentPage)ContentFrame.Content;
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == typeof(TalentCrafting)).ToList());
                        currentPage = "CraftingTalent";
                        break;
                    case "create":
                        ContentFrame.Navigate(typeof(CharakterCreation));
                        currentPage = "create";
                        break;
                    case "createTalent":
                        ContentFrame.Navigate(typeof(CreateTalent));
                        currentPage = "createTalent";
                        break;
                    case "save":
                        Game.Charakter.Save(out Error error);
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
