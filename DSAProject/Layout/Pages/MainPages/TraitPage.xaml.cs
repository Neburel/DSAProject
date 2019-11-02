using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.util;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TraitPage : Page
    {
        private TraitPageViewModel viewModel = new TraitPageViewModel();
        private Trait createNewTrait = new Trait { Description = "CreateNew" };
        private TraitType? trailFilter = null;

        public SolidColorBrush BackgroundColor
        {
            set => viewModel.BackgroundColor = value;
        }
        public TraitType TraitFilter
        {
            set
            {
                var traits = Game.Charakter.Traits.GetTraits().Where(x => x.TraitType == value).ToList();
                viewModel.Traits = new ObservableCollection<Trait>(traits);
                viewModel.Traits.Add(createNewTrait);
                trailFilter = value;
            }
        }
        public TraitPage()
        {
            this.InitializeComponent();
            viewModel.Traits = new ObservableCollection<Trait>(Game.Charakter.Traits.GetTraits());
            viewModel.Traits.Add(createNewTrait);
        }
        private class TraitPageViewModel : AbstractPropertyChanged
        {
            public SolidColorBrush BackgroundColor = new SolidColorBrush();
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

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem == createNewTrait)
            {
                //Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = null });
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = trailFilter });
            }
            else
            {
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = e.ClickedItem });
            }
        }
    }
}
