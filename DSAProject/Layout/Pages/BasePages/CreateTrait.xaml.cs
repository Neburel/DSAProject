using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
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
            XAML_TraitValue.AKTValue    = string.Empty;
            XAML_TraitGP.AKTValue       = string.Empty;
        }
        #region Value
        private void XAML_TraitValue_Event_ValueHigher(object sender, EventArgs e)
        {
            viewModel.Value = nextValue(XAML_TraitValue.AKTValue, true);
        }
        private void XAML_TraitValue_Event_ValueLower(object sender, EventArgs e)
        {
            viewModel.Value = nextValue(XAML_TraitValue.AKTValue, false);
        }
        private void XAML_ValueXButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Value = "X";
        }
        private void XAML_ValueClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Value = string.Empty;
        }
        #endregion
        #region GP
        private void XAML_TraitGP_Event_ValueHigher(object sender, EventArgs e)
        {
            viewModel.GP = nextValue(XAML_TraitGP.AKTValue, true);
        }
        private void XAML_TraitGP_Event_ValueLower(object sender, EventArgs e)
        {
            viewModel.GP = nextValue(XAML_TraitGP.AKTValue, false);
        }
        private void XAML_GPXButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GP = "X";
        }
        private void XAML_GPClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GP = string.Empty;
        }
        #endregion
        private void XAML_ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var trait = viewModel.Trait;
            Game.Charakter.Traits.AddTrait(trait);
        }
        private string nextValue(string current, bool plus)
        {
            if(int.TryParse(current, out int value))
            {
                if (plus) return (value + 1).ToString();
                return (value - 1).ToString();
            }
            else
            {
                return (0).ToString();
            }
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
                    OnPropertyChanged(nameof(Title));
                    OnPropertyChanged(nameof(Description));
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(GP));
                }
            }
            public string GP
            {
                get => trait.GP;
                set
                {
                    trait.GP = value;
                    OnPropertyChanged(nameof(GP));
                }
            }
            public string Value
            {
                get => trait.Value;
                set
                {
                    trait.Value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
            public string Title
            {
                get => trait.Title;
                set
                {
                    trait.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
            public string Description
            {
                get => trait.Description;
                set
                {
                    trait.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
       
        }        
    }
}
