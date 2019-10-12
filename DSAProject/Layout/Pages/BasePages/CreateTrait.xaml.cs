using DSALib.Charakter.Trait;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.BasePages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CreateTrait : Page
    {
        private CreateTrait_ViewModel viewModel = new CreateTrait_ViewModel();

        public CreateTrait()
        {
            this.InitializeComponent();
        }

        private class CreateTrait_ViewModel : AbstractPropertyChanged
        {
            private Trait trait = new Trait();
            public Trait Trait
            {
                get => trait;
                set
                {
                    trait = value;
                    OnPropertyChanged(nameof(Trait));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var trait = viewModel.Trait;
        }
    }
}
