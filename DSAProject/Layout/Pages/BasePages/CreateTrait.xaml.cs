using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.util;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            viewModel.Value = NextValue(XAML_TraitValue.AKTValue, true);
        }
        private void XAML_TraitValue_Event_ValueLower(object sender, EventArgs e)
        {
            viewModel.Value = NextValue(XAML_TraitValue.AKTValue, false);
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
            viewModel.GP = NextValue(XAML_TraitGP.AKTValue, true);
        }
        private void XAML_TraitGP_Event_ValueLower(object sender, EventArgs e)
        {
            viewModel.GP = NextValue(XAML_TraitGP.AKTValue, false);
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
        #region Handler
        private void XAML_ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var trait = viewModel.Trait;
            Game.Charakter.Traits.AddTrait(trait);
            Game.RequestNav(new EventNavRequest { Side = NavEnum.StartPage });
        }
        private void XAML_ButtonDelte_Click(object sender, RoutedEventArgs e)
        {
            var trait = viewModel.Trait;
            if(trait != null)
            {
                Game.Charakter.Traits.RemoveTrait(trait);
            }
            Game.RequestNav(new EventNavRequest { Side = NavEnum.StartPage });
        }
        private void XAML_TaWBonus_AddTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            if(e != null)
            {
                viewModel.Trait.SetTaWBonus(e.Talent, e.Value);
            }
        }
        private void XAML_TaWBonus_RemoveTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            if(e != null)
            {
                viewModel.Trait.RemoveTaWBonus(e.Talent);
            }
        }
        private void XAML_ATBonus_AddTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            if(e != null)
            {
                viewModel.Trait.SetATBonus((AbstractTalentFighting)e.Talent, e.Value);
            }
        }
        private void XAML_ATBonus_RemoveTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            viewModel.Trait.RemoveATBonus((AbstractTalentFighting)e.Talent);
        }
        private void XAML_PABonus_AddTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            viewModel.Trait.SetPABonus((AbstractTalentFighting)e.Talent, e.Value);
        }
        private void XAML_PABonus_RemoveTrait(object sender, Hilfsklassen.TraitTalentBonus e)
        {
            viewModel.Trait.RemovePABonus((AbstractTalentFighting)e.Talent);
        }
        #endregion
        #region Hilfsmethoden
        private string NextValue(string current, bool plus)
        {
            if (int.TryParse(current, out int value))
            {
                if (plus) return (value + 1).ToString();
                return (value - 1).ToString();
            }
            else
            {
                return (0).ToString();
            }
        }
        public void SetTrait(Trait trait)
        {
            viewModel.Trait = trait;
            foreach (var item in trait.GetTawBonus())
            {
                XAML_TaWBonus.NewTalent(true, item.Key, item.Value);
            }
            foreach (var item in trait.GetATBonus())
            {
                XAML_ATBonus.NewTalent(true, item.Key, item.Value);
            }
            foreach (var item in trait.GetPABonus())
            {
                XAML_PABonus.NewTalent(true, item.Key, item.Value);
            }
        }
        public void SetTraitType(TraitType trait)
        {
            viewModel.SelectedItem = Enum.GetName(typeof(TraitType), trait);
        }
        #endregion
        private class CreateTrait_ViewModel : AbstractPropertyChanged
        {
            private Trait trait = new Trait();
            private List<string> traitTypes = new List<string>(Enum.GetNames(typeof(TraitType)));
            private string selectedItem;
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
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }

            public List<string> TraitTypes
            {
                get => traitTypes;
                set
                {
                    traitTypes = value;
                    OnPropertyChanged(nameof(TraitTypes));
                }
            }
            public string SelectedItem
            {
                get
                {
                    return Enum.GetName(typeof(TraitType), trait.TraitType);
                }
                set
                {
                    selectedItem = value;

                    if(Enum.TryParse(typeof(TraitType), value, out object type))
                    {
                        if(trait.TraitType != (TraitType)type)
                        {
                            trait.TraitType = (TraitType)type;
                            OnPropertyChanged(nameof(SelectedItem));
                        }
                    }
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
