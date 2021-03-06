﻿using DSALib;
using DSALib.Charakter.Other;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Game;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages.BasePages;
using DSAProject.Layout.Pages.MainPages;
using DSAProject.Layout.Pages.ToolPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
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
        private readonly List<DSANavItem> navHistory     = new List<DSANavItem>();
        private readonly NavigationViewItem startItem    = new NavigationViewItem { Content = "Heldenbrief", Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(HeroLetterPage) } };
        private List<NavigationViewItemBase> NavItems { get; set; }
        private List<NavigationViewItemBase> NavigationViewItem { get; set; }

        public GameNavPage()
        {
            var standartSize = new Size(1500, 1000);
            ApplicationView.PreferredLaunchViewSize = standartSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(standartSize);

            NavItems = new List<NavigationViewItemBase>
            {
                new NavigationViewItemHeader    { Content = "Briefe" },
                startItem,
                new NavigationViewItem          { Content = "Sprachenbrief",                Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(LanguagePage2) } },
                new NavigationViewItem          { Content = "Eigenschaftsbrief",            Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TraitPage), Parameter = TraitType.Keiner } },
                //new NavigationViewItem          { Content = "MagieBrief",                   Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(SpellBookPage) } },

                new NavigationViewItemHeader    { Content = "Kampf Talente" },
                new NavigationViewItem          { Content = "Waffenlose Kampftechniken",    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentWeaponless) } },
                new NavigationViewItem          { Content = "Nahkampf Kampftechniken",      Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentClose) } },
                new NavigationViewItem          { Content = "Fernkampf Kampftechniken",     Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentRange) } },

                new NavigationViewItemHeader    { Content = "Allgemeine Talente" },
                new NavigationViewItem          { Content = "Körperliche Talente",          Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentPhysical)} },
                new NavigationViewItem          { Content = "Gesellschaftliche Talente",    Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentSocial) } },
                new NavigationViewItem          { Content = "Natur Talente",                Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentNature) } },
                new NavigationViewItem          { Content = "Wissenstalente",               Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentKnowldage) } },
                new NavigationViewItem          { Content = "Handwerkstalente",             Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(TalentPage2), Parameter = typeof(TalentCrafting) } },
           
                new NavigationViewItemHeader    { Content = "Werkzeuge" },
                new NavigationViewItem          { Content = "Charakter erstellen/bearbeiten",   Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(CharakterCreation) } },
                new NavigationViewItem          { Content = "Speichern",                        Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(SavePage) } },
                new NavigationViewItem          { Content = "Laden",                            Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(LoadPage) } },
                new NavigationViewItem          { Content = "Wiki",                             Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(WikiPage) } },
                //new NavigationViewItem          { Content = "CreateSpell",                      Icon = new BitmapIcon(), Tag = new DSANavItem { NavType = typeof(CreateSpell) } },
            };

            NavigationViewItem = NavItems.Where(x => x.Tag != null).ToList();
            Game.NavRequested += Game_NavRequested;

            this.InitializeComponent();

            XAML_NavigationView.SelectedItem = startItem;
            Game.RequestNav(new util.EventNavRequest { Side = NavEnum.StartPage });
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
                case NavEnum.CreateSpellPage:
                    XAML_ContentFrame.Navigate(typeof(CreateSpell), e.Parameter);
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

            if(e.SourcePageType == typeof(TraitPage))
            {
                var currentItem = (TraitPage)XAML_ContentFrame.Content;
                currentItem.TextColor = new SolidColorBrush(Windows.UI.Colors.White);

                if(parameter != null)
                {
                    if (parameter.GetType() == typeof(TraitType))
                    {
                        var type = (TraitType)parameter;
                        currentItem.TraitFilter = type;
                    }
                }
                else
                {
                    currentItem.TraitFilter = TraitType.Keiner;
                }
            }
            else if (parameter != null)
            {
                if (e.SourcePageType == typeof(TalentPage2))
                {
                    var page            = (TalentPage2)XAML_ContentFrame.Content;
                    var selectionType   = (Type)((DSANavItem)navItem.Tag).Parameter;
                    page.SetTalentType(selectionType);
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
            }
        }
        private void Frame_NavigationStopped(object sender, NavigationEventArgs e)
        {
            var lastNav = navHistory.LastOrDefault();
            var item    = NavigationViewItem.Where(x => ((DSANavItem)x.Tag).NavType == lastNav.NavType).FirstOrDefault();

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
            if (args.IsSettingsInvoked)
            {
                XAML_ContentFrame.Navigate(typeof(SettingsPage), null);
            }
            else
            {
                var navItem = (DSANavItem)((NavigationViewItem)sender.SelectedItem).Tag;
                var lastItem = navHistory.LastOrDefault();

                if (navItem.NavType == typeof(SavePage))
                {
                    Game.CharakterSave(out DSAError error);
                    SaveDialog.ShowDialog(error);
                    XAML_NavigationView.SelectedItem = startItem;

                    if (lastItem.NavType != typeof(HeroLetterPage))
                    {
                        XAML_ContentFrame.Navigate(typeof(HeroLetterPage));
                    }
                }
                else if (lastItem.NavType != navItem.NavType || lastItem.Parameter != navItem.Parameter)
                {
                    XAML_ContentFrame.Navigate(navItem.NavType, navItem.Parameter);
                }
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

        private void XAML_NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }
    }
}
