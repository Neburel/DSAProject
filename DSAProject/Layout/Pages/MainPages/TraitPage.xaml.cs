using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.Converter;
using DSAProject.Layout.Pages.BasePages;
using DSAProject.util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TraitPage : Page
    {
        private Frame currentFrame;
        private TraitPageViewModel viewModel = new TraitPageViewModel();
        private Trait createNewTrait = new Trait { Description = "CreateNew" };
        private TraitType? trailFilter = null;

        private bool Advantages { get; set; }
        private bool DisAdvantages { get; set; }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentFrame = Frame;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        public TraitType TraitFilter
        {
            set
            {
                var traits = Game.Charakter.Traits.GetTraits().Where(x => x.TraitType == value).ToList();
                viewModel.Traits = new ObservableCollection<Trait>(traits)
                {
                    createNewTrait
                };
                trailFilter = value;
            }
        }
        private List<TraitType> TraitFilters
        {
            set
            {
                var list = value.Distinct();
                var traitList = new List<Trait>();
                foreach(var item in list)
                {
                    var traits = Game.Charakter.Traits.GetTraits().Where(x => x.TraitType == item).ToList();
                    traitList.AddRange(traits);
                }
                viewModel.Traits = new ObservableCollection<Trait>(traitList)
                {
                    createNewTrait
                };
            }
        }

        public TraitPage()
        {
            this.Resources.Remove("PrimaryBrush");
            Resources.Add("PrimaryBrush",  ColorConverter.SolidColorBrush);

            this.InitializeComponent();

            viewModel.Traits = new ObservableCollection<Trait>(Game.Charakter.Traits.GetTraits())
            {
                createNewTrait
            };
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem == createNewTrait)
            {
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = trailFilter });
            }
            else
            {
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = e.ClickedItem });
            }
        }
        private void CheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var list = new List<TraitType>();
            if (Advantages) list.Add(TraitType.Vorteil);
            if (DisAdvantages) list.Add(TraitType.Nachteil);

            TraitFilters = list;
        }
        private class TraitPageViewModel : AbstractPropertyChanged
        {
            private ObservableCollection<Trait> traits;
            public ObservableCollection<Trait> Traits
            {
                get => traits;
                set
                {
                    traits = value;
                    OnPropertyChanged(nameof(Traits));
                }
            }
        }
    }
}
