using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Game;
using DSAProject.Layout.MessageDialoge;
using DSAProject.Layout.Pages;
using DSAProject.Layout.Pages.BasePages;
using DSAProject.Layout.Pages.ToolPages;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        private GameContentItem currentItem;
        #endregion
        private GamePageViewModel viewModel = new GamePageViewModel();
        public GamePage()
        {
            this.InitializeComponent();
            currentPage = "HeroLetter";
            ContentFrame.Navigate(typeof(HeroLetterPage));

            Game.NavRequested += (sender, args) =>
            {
                switch (args)
                {
                    case NavEnum.StartPage:
                        Navigate(viewModel.HeroLetter);
                        break;
                    case NavEnum.CreateTraitPage:
                        Navigate(viewModel.CreateTrait);
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
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
            var tag = (GameContentItem)item.Tag;
            Navigate(tag);
        }
        private void Navigate(GameContentItem navItem)
        {
            if (currentItem != navItem)
            {
                if (navItem.Type != null)
                {
                    ContentFrame.Navigate(navItem.Type);
                    currentItem = navItem;
                }

                if (navItem.Type == typeof(TalentPage))
                {
                    TalentPage page = (TalentPage)ContentFrame.Content;
                    page.SetTalents(Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == navItem.SelectionType).ToList());
                }
                if (navItem == viewModel.Save)
                {
                    Game.CharakterSave(out Error error);
                    SaveDialog.ShowDialog(error);
                }
                return;
            }
        }
        private class GamePageViewModel
        {
            public GameContentItem HeroLetter { get; private set; } = new GameContentItem { Content = "Heldenbrief",                Type = typeof(HeroLetterPage) };
            public GameContentItem Weaponless { get; private set; } = new GameContentItem { Content = "Waffenlose Kampftechniken",  Type = typeof(TalentPage), SelectionType = typeof(TalentWeaponless) };
            public GameContentItem Close { get; private set; }      = new GameContentItem { Content = "Bewaffnete Kampftechniken",  Type = typeof(TalentPage), SelectionType = typeof(TalentClose) };
            public GameContentItem Range { get; private set; }      = new GameContentItem { Content = "Fehrnkampf Kampftechniken",  Type = typeof(TalentPage), SelectionType = typeof(TalentRange)};
            public GameContentItem Pyhsical { get; private set; }   = new GameContentItem { Content = "Körperliche Talente",        Type = typeof(TalentPage), SelectionType = typeof(TalentPhysical) };
            public GameContentItem Social { get; private set; }     = new GameContentItem { Content = "Gesellschaftliche Talente",  Type = typeof(TalentPage), SelectionType = typeof(TalentSocial) };
            public GameContentItem Nature { get; private set; }     = new GameContentItem { Content = "Natur Talente",              Type = typeof(TalentPage), SelectionType = typeof(TalentNature) };
            public GameContentItem Knowldage { get; private set; }  = new GameContentItem { Content = "Wissenstalente",             Type = typeof(TalentPage), SelectionType = typeof(TalentKnowldage) };
            public GameContentItem Crafting { get; private set; }   = new GameContentItem { Content = "Handwerkstalente",           Type = typeof(TalentPage), SelectionType = typeof(TalentCrafting) };
            public GameContentItem Trait { get; private set; }      = new GameContentItem { Content = "Belohnungen",                Type = typeof(TraitPage) };
            public GameContentItem Save { get; private set; }       = new GameContentItem { Content = "Charakter Speichern" };
            public GameContentItem Load { get; private set; }       = new GameContentItem { Content = "Charakter Laden",                Type = typeof(LoadPage) };
            public GameContentItem CreateCharkter { get; private set; } = new GameContentItem { Content = "Charakter erstellen/editieren",  Type = typeof(CharakterCreation) };
            public GameContentItem CreateTrait { get; private set; }    = new GameContentItem { Content = "Create Trait", Type = typeof(CreateTrait) };
        }
        private class GameContentItem
        {
            public string Content { get; set; }
            public Type Type { get; set; }
            public Type SelectionType { get; set; }
        }
    }
}
