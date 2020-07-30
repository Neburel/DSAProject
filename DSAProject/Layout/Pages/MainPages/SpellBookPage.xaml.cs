using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DSAProject.Layout.Pages.MainPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SpellBookPage : Page
    {
        private SpellBookPageViewModel viewModel;
        public SpellBookPage()
        {
            viewModel = new SpellBookPageViewModel();
            viewModel.SpellList = new ObservableCollection<SpellViewModel>();

            this.InitializeComponent();

            Game.Charakter.CharakterSpellBook.AddSpell(new DSALib.Charakter.Other.Spell("Test", Game.Charakter.Attribute.UsedAttributs) { });
            var spellList = Game.Charakter.CharakterSpellBook.GetSpellList();
            foreach(var item in spellList)
            {
                var model = new SpellViewModel(item);
                viewModel.SpellList.Add(model);
            }
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }
        internal class SpellBookPageViewModel : AbstractPropertyChanged
        {
            public bool IsLoading
            {
                get => Get<bool>();
                set => Set(value);
            }
            public string DiceText
            {
                get => Get<string>();
                set => Set(value);
            }
            public DiceChanger DiceChanger
            {
                get => Get<DiceChanger>();
                set => Set(value);
            }
            public ObservableCollection<SpellViewModel> SpellList
            {
                get => Get<ObservableCollection<SpellViewModel>>();
                set => Set(value);
            }
        }

        private void SpellListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateSpellPage });
        }
    }
    public class SpellBookPageHeader
    {

    }
}
