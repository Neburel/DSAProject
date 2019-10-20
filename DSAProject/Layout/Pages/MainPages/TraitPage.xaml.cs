using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.util;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TraitPage : Page
    {
        private TraitPageViewModel viewModel = new TraitPageViewModel();

        public TraitPage()
        {
            this.InitializeComponent();
            viewModel.Traits = new ObservableCollection<Trait>(Game.Charakter.Traits.GetTraits());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game.RequestNav(NavEnum.CreateTraitPage);
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
