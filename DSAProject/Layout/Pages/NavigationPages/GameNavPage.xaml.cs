using DSALib;
using DSALib.Charakter.Other;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Game;
using DSAProject.Converter;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages.BasePages;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.NavigationPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GameNavPage : Page
    {
        private List<DSANavItem> navHistory     = new List<DSANavItem>();
        private NavigationViewItem startItem    = new NavigationViewItem { Content = "Heldenbrief", Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(HeroLetterPage) } };
        private List<NavigationViewItemBase> NavItems { get; set; }
        private List<NavigationViewItemBase> NavigationViewItem { get; set; }

        public GameNavPage()
        {
            NavItems = new List<NavigationViewItemBase>
            {
                new NavigationViewItemHeader    { Content = "Briefe" },
                startItem,

                new NavigationViewItemHeader    { Content = "Kampf Talente" },
                new NavigationViewItem          { Content = "Waffenlose Kampftechniken",    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentWeaponless) } },
                new NavigationViewItem          { Content = "Fehrnkampf Kampftechniken",    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentClose) } },
                new NavigationViewItem          { Content = "Nahkampf Kampftechniken",      Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentRange) } },

                new NavigationViewItemHeader    { Content = "Allgemeine Talente" },
                new NavigationViewItem          { Content = "Körperliche Talente",          Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentPhysical) } },
                new NavigationViewItem          { Content = "Gesellschaftliche Talente",    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentSocial) } },
                new NavigationViewItem          { Content = "Natur Talente",                Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentNature) } },
                new NavigationViewItem          { Content = "Wissenstalente",               Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentKnowldage) } },
                new NavigationViewItem          { Content = "Handwerkstalente",             Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(TalentCrafting) } },
                new NavigationViewItem          { Content = "Alle",                         Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage), Parameter = typeof(AbstractTalentGeneral) } },

                new NavigationViewItemHeader    { Content = "Eigenschaften" },
                new NavigationViewItem          { Content = "Vorteile",                     Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Vorteil } },
                new NavigationViewItem          { Content = "Nachteile",                    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Nachteil} },
                new NavigationViewItem          { Content = "Events",                       Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Event} },
                new NavigationViewItem          { Content = "Geburstage",                   Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Geburstag } },
                new NavigationViewItem          { Content = "Quest",                        Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Quest } },
                new NavigationViewItem          { Content = "Belohnungen",                  Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Belohnung } },
                new NavigationViewItem          { Content = "Alle",                         Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage) } },

                new NavigationViewItemHeader    { Content = "Werkzeuge" },
                new NavigationViewItem          { Content = "Charakter erstellen/bearbeiten",   Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(CharakterCreation) } },
                new NavigationViewItem          { Content = "Speichern",                        Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(SavePage) } },
                new NavigationViewItem          { Content = "Laden",                            Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(LoadPage) } },
                new NavigationViewItem          { Content = "Info",                             Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(InfoPage) } }
            };
            NavigationViewItem = NavItems.Where(x => x.Tag != null).ToList();
            Game.NavRequested += Game_NavRequested;

            this.InitializeComponent();
            XAML_ContentFrame.NavigationStopped += Frame_NavigationStoppec;
            XAML_ContentFrame.Navigated += ContentFrame_Navigated;

            XAML_NavigationView.SelectedItem = startItem;
            XAML_ContentFrame.Navigate(typeof(HeroLetterPage));
        }
        #region Handler
        private void Game_NavRequested(object sender, util.EventNavRequest e)
        {
            switch (e.Side)
            {
                case NavEnum.StartPage:
                    XAML_ContentFrame.Navigate(typeof(HeroLetterPage), e.Parameter);
                    break;
                case NavEnum.CreateTraitPage:
                    XAML_ContentFrame.Navigate(typeof(CreateTrait), e.Parameter);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var parameter   = e.Parameter;
            var navItem     = NavigationViewItem.Where(x => ((DSANavItem)x.Tag).NavType == e.SourcePageType && ((DSANavItem)x.Tag).Parameter == parameter).FirstOrDefault();
            navHistory.Add(new DSANavItem { NavType = e.SourcePageType, Parameter = parameter });

            if (navItem != null && NavItems.Contains(navItem))
            {
                XAML_NavigationView.SelectedItem = navItem;
            }
            else
            {
                XAML_NavigationView.SelectedItem = null;
            }

            if (parameter != null)
            {

                if (e.SourcePageType == typeof(TalentPage))
                {
                    var page            = (TalentPage)XAML_ContentFrame.Content;
                    var selectionType   = (Type)((DSANavItem)navItem.Tag).Parameter;
                    var talents         = Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == selectionType).ToList();

                    if (talents.Count() == 0)
                    {
                        page.SetTalents(talents);
                    }
                    else
                    {
                        page.SetTalents(talents);
                    }

                }
                else if (e.SourcePageType == typeof(CreateTrait))
                {
                    var page = (CreateTrait)XAML_ContentFrame.Content;

                    if (parameter.GetType() == typeof(Trait))
                    {
                        page.SetTrait((Trait)parameter);
                    }
                    else
                    {
                        page.SetTraitType((DSALib.TraitType)parameter);
                    }
                }
                else if (e.SourcePageType == typeof(TraitPage))
                {
                    var currentItem = (TraitPage)XAML_ContentFrame.Content;

                    if (parameter.GetType() == typeof(TraitType))
                    {
                        var type = (TraitType)parameter;
                        currentItem.TraitFilter = type;
                    }
                }
            }
        }
        private void Frame_NavigationStoppec(object sender, NavigationEventArgs e)
        {
            var lastNav = navHistory.LastOrDefault();
            var item    = NavItems.Where(x => (Type)x.Tag == lastNav.NavType).FirstOrDefault();

            if(item != null)
            {
                XAML_NavigationView.SelectedItem = item;
            }
            else
            {
                XAML_NavigationView.SelectedItem = null;
            }
        }
        private void XAML_NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            var navItem = (DSANavItem)((NavigationViewItem)sender.SelectedItem).Tag;
            var lastItem = navHistory.LastOrDefault();

            if (navItem.NavType == typeof(SavePage))
            {
                Game.CharakterSave(out Error error);
                SaveDialog.ShowDialog(error);
                XAML_NavigationView.SelectedItem = startItem;

                if (lastItem.NavType != typeof(HeroLetterPage))
                {
                    XAML_ContentFrame.Navigate(typeof(HeroLetterPage));
                }
            }
            else if (lastItem.NavType != navItem.NavType || lastItem.Parameter != navItem.Parameter)
            {
                if (navItem.NavType == typeof(HeroLetterPage))
                {
                    ColorConverter.SolidColorBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                }
                else if (navItem.NavType == typeof(TraitPage))
                {
                    ColorConverter.SolidColorBrush = new SolidColorBrush(Windows.UI.Colors.White);
                }


                XAML_ContentFrame.Navigate(navItem.NavType, navItem.Parameter);
            }
        }
        #endregion
        #region Hilfsklassen
        private class DSANavItem
        {
            public Type NavType { get; set; }
            public object Parameter { get; set; }
        }
        private class SavePage
        {

        }
        #endregion
    }
}
