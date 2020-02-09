using DSALib;
using DSALib.Charakter.Other;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Game;
using DSAProject.Converter;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages;
using DSAProject.Layout.Pages.BasePages;
using DSAProject.Layout.Pages.MainPages;
using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace DSAProject
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        #region Pages
        private string currentPage = string.Empty;
        private GameContentItem currentItem;
        #endregion
        private GamePageViewModel viewModel = new GamePageViewModel();
        public GamePage()
        {
            this.InitializeComponent();
            var standartSize = new Size(1400, 1000);

            ApplicationView.PreferredLaunchViewSize = standartSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(standartSize);
            
            currentPage = "HeroLetter";
            //ContentFrame.Navigate(typeof(HeroLetterPage));
            ContentFrame.NavigationStopped += Frame_NavigationStopped;
            
            Game.NavRequested += (sender, args) =>
            {
                switch (args.Side)
                {
                    case NavEnum.StartPage:
                        Navigate(viewModel.HeroLetter, args.Parameter);
                        break;
                    case NavEnum.CreateTraitPage:
                        Navigate(viewModel.CreateTrait, args.Parameter);
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
            };
        }
        private void Frame_NavigationStopped(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.GetType() == typeof(string))
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
            sender.SelectedItem = null;
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            var tag = (GameContentItem)item.Tag;
            Navigate(tag, null);
        }
        private void Navigate(GameContentItem navItem, object parameter)
        {
            if (currentItem != navItem)
            {
                if (navItem.Type == typeof(HeroLetterPage))
                {
                    ColorConverter.SolidColorBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                }
                else if (navItem.Type == typeof(TraitPage))
                {
                    ColorConverter.SolidColorBrush = new SolidColorBrush(Windows.UI.Colors.White);
                }

                if (navItem.Type != null)
                {
                    ContentFrame.Navigate(navItem.Type);
                    currentItem = navItem;
                }
                if (navItem.Type == typeof(TalentPage) && ContentFrame.Content.GetType() == typeof(TalentPage))
                {
                    TalentPage page = (TalentPage)ContentFrame.Content;

                    var selectionType = (Type)navItem.SelectionType;
                    var k = Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == selectionType);

                    if (k.Count() == 0)
                    {
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => selectionType.IsAssignableFrom(x.GetType())).ToList());
                    }
                    else
                    {
                        page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == selectionType).ToList());
                    }
                }
                else if (navItem.Type == typeof(CreateTrait) && parameter != null)
                {
                    CreateTrait page = (CreateTrait)ContentFrame.Content;

                    if (parameter.GetType() == typeof(Trait))
                    {
                        page.SetTrait((Trait)parameter);
                    }
                    else
                    {
                        page.SetTraitType((DSALib.TraitType)parameter);
                    }
                }
                else if (navItem.Type == typeof(TraitPage))
                {
                    var currentItem = (TraitPage)ContentFrame.Content;

                    if (navItem.SelectionType != null)
                    {
                        var type = (TraitType)navItem.SelectionType;
                        currentItem.TraitFilter = type;
                    }
                }
                if (navItem == viewModel.Save)
                {
                    Game.CharakterSave(out DSAError error);
                    SaveDialog.ShowDialog(error);
                }
                return;
            }
        }
        private class GamePageViewModel
        {
            public GameContentItem HeroLetter { get; private set; } = new GameContentItem { Content = "Heldenbrief", Type = typeof(HeroLetterPage) };
            public GameContentItem GeneralTalent { get; private set; } = new GameContentItem { Content = "Alle Allgemeinen", Type = typeof(TalentPage), SelectionType = typeof(AbstractTalentGeneral) };
            public GameContentItem FightingTalent { get; private set; } = new GameContentItem { Content = "Alle Kampf", Type = typeof(TalentPage), SelectionType = typeof(AbstractTalentFighting) };
            public GameContentItem Weaponless { get; private set; } = new GameContentItem { Content = "Waffenlose Kampftechniken", Type = typeof(TalentPage), SelectionType = typeof(TalentWeaponless) };
            public GameContentItem Close { get; private set; } = new GameContentItem { Content = "Bewaffnete Kampftechniken", Type = typeof(TalentPage), SelectionType = typeof(TalentClose) };
            public GameContentItem Range { get; private set; } = new GameContentItem { Content = "Fehrnkampf Kampftechniken", Type = typeof(TalentPage), SelectionType = typeof(TalentRange) };
            public GameContentItem Pyhsical { get; private set; } = new GameContentItem { Content = "Körperliche Talente", Type = typeof(TalentPage), SelectionType = typeof(TalentPhysical) };
            public GameContentItem Social { get; private set; } = new GameContentItem { Content = "Gesellschaftliche Talente", Type = typeof(TalentPage), SelectionType = typeof(TalentSocial) };
            public GameContentItem Nature { get; private set; } = new GameContentItem { Content = "Natur Talente", Type = typeof(TalentPage), SelectionType = typeof(TalentNature) };
            public GameContentItem Knowldage { get; private set; } = new GameContentItem { Content = "Wissenstalente", Type = typeof(TalentPage), SelectionType = typeof(TalentKnowldage) };
            public GameContentItem Crafting { get; private set; } = new GameContentItem { Content = "Handwerkstalente", Type = typeof(TalentPage), SelectionType = typeof(TalentCrafting) };
            public GameContentItem Advanced { get; private set; } = new GameContentItem { Content = "Vorteile", Type = typeof(TraitPage), SelectionType = TraitType.Vorteil };
            public GameContentItem DisAdvanced { get; private set; } = new GameContentItem { Content = "Nachteile", Type = typeof(TraitPage), SelectionType = TraitType.Nachteil };
            public GameContentItem Event { get; private set; } = new GameContentItem { Content = "Event", Type = typeof(TraitPage), SelectionType = TraitType.Event };
            public GameContentItem Birthday { get; private set; } = new GameContentItem { Content = "Geburstag", Type = typeof(TraitPage), SelectionType = TraitType.Geburstag };
            public GameContentItem Quest { get; private set; } = new GameContentItem { Content = "Quest", Type = typeof(TraitPage), SelectionType = TraitType.Quest };
            public GameContentItem Belohnung { get; private set; } = new GameContentItem { Content = "Belohnung", Type = typeof(TraitPage), SelectionType = TraitType.Belohnung };
            public GameContentItem Trait { get; private set; } = new GameContentItem { Content = "Alle Eigenschaften", Type = typeof(TraitPage) };
            public GameContentItem Save { get; private set; } = new GameContentItem { Content = "Charakter Speichern" };
            public GameContentItem Load { get; private set; } = new GameContentItem { Content = "Charakter Laden", Type = typeof(LoadPage) };
            public GameContentItem CreateCharkter { get; private set; } = new GameContentItem { Content = "Charakter erstellen/editieren", Type = typeof(CharakterCreation) };
            public GameContentItem CreateTrait { get; private set; } = new GameContentItem { Content = "Create Trait", Type = typeof(CreateTrait) };
            public GameContentItem InfoPage { get; private set; } = new GameContentItem { Content = "Info", Type = typeof(InfoPage) };
            public GameContentItem LanguagePage { get; private set; } = new GameContentItem { Content = "Sprachen", Type = typeof(LanguagePage) };
        }
        private class GameContentItem
        {
            public string Content { get; set; }
            public Type Type { get; set; }
            public object SelectionType { get; set; }
        }

        private void NavigationView_ContextCanceled(UIElement sender, RoutedEventArgs args)
        {

        }
    }
}
