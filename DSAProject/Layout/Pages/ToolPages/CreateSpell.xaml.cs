using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ToolPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CreateSpell : Page
    {
        private const string D0 = "";
        private const string D1 = "A";
        private const string D2 = "B";
        private const string D3 = "C";
        private const string D4 = "D";
        private const string D5 = "E";
        private const string D6 = "F";


        private CreateSpellViewModel viewModel = new CreateSpellViewModel();

        public CreateSpell()
        {
            this.InitializeComponent();
            viewModel.ChooseList    = new List<CharakterAttribut>();
            viewModel.SetList       = new ObservableCollection<CharakterAttribut>();
            foreach(var item in Game.Charakter.Attribute.UsedAttributs)
            {
                viewModel.ChooseList.Add(item);
            }
            viewModel.Difficult = D0;
        }
        private Spell CreateASpell()
        {
            if (string.IsNullOrEmpty(viewModel.Name)) return null;

            return new Spell(viewModel.Name, new List<CharakterAttribut>(viewModel.SetList))
            {
                Level = viewModel.Level,
                Effect = viewModel.Effect,
                Komplex1 = viewModel.Komplex1,
                Komplex2 = viewModel.Komplex2,
                Characteristics = viewModel.Characteristics,
                Difficult = viewModel.Difficult,
            };
        }
        public class CreateSpellViewModel : AbstractPropertyChanged
        {
            public int Level
            {
                get => Get<int>();
                set => Set(value);
            }
            public string Name
            {
                get => Get<string>();
                set => Set(value);
            }
            public string Effect
            {
                get => Get<string>();
                set => Set(value);
            }
            public string Komplex1
            {
                get => Get<string>();
                set => Set(value);
            }
            public string Komplex2
            {
                get => Get<string>();
                set => Set(value);
            }
            public string Characteristics
            {
                get => Get<string>();
                set => Set(value);
            }
            public string AttributeProbeText
            {
                get => Get<string>();
                set => Set(value);
            }
            public string Difficult
            {
                get => Get<string>();
                set => Set(value);
            }
            public List<CharakterAttribut> ChooseList
            {
                get => Get<List<CharakterAttribut>>();
                set => Set(value);
            }
            public ObservableCollection<CharakterAttribut> SetList
            {
                get => Get<ObservableCollection<CharakterAttribut>>();
                set => Set(value);
            }
        }

        private void Button_ClickAddAttribute(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var item = XAML_ChooseComboBox.SelectedItem;
            if(item != null)
            {
                if(viewModel.SetList.Count <= 8)
                {
                    viewModel.SetList.Add((CharakterAttribut)item);
                }
            }
            CreateText();
        }
        private void Button_ClickRemoveAttribute(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var item = XAML_SetListComboBox.SelectedItem;
            if (item != null)
            {
                viewModel.SetList.Remove((CharakterAttribut)item);
            }
            CreateText();
        }
        private void CreateText()
        {
            var text = string.Empty;
            foreach(var item in viewModel.SetList)
            {
                text = text + " " + item.ToString();
            }
            viewModel.AttributeProbeText = text;
        }

        private void XAML_PlusButton_Clicked(object sender, object e)
        {
            ChangeDifficult(true);
        }
        private void XAML_MinusButton_Clicked(object sender, object e)
        {
            ChangeDifficult(false);
        }
        public void ChangeDifficult(bool plus)
        {
            if (IsDifficult(D0))
            {
                if (plus) viewModel.Difficult = D1;
                else viewModel.Difficult = D6;
            }
            else if (IsDifficult(D1))
            {
                if (plus) viewModel.Difficult = D2;
                else viewModel.Difficult = D0;
            }
            else if (IsDifficult(D2))
            {
                if (plus) viewModel.Difficult = D3;
                else viewModel.Difficult = D1;
            }
            else if (IsDifficult(D3))
            {
                if (plus) viewModel.Difficult = D4;
                else viewModel.Difficult = D2;
            }
            else if (IsDifficult(D4))
            {
                if (plus) viewModel.Difficult = D5;
                else viewModel.Difficult = D3;
            }
            else if (IsDifficult(D5))
            {
                if (plus) viewModel.Difficult = D6;
                else viewModel.Difficult = D4;
            }
            else if (IsDifficult(D6))
            {
                if (plus) viewModel.Difficult = D0;
                else viewModel.Difficult = D5;
            }
            else
            {
                viewModel.Difficult = D0;
            }
        }
        private bool IsDifficult(string df)
        {
            if (viewModel.Difficult == df)
            {
                return true;
            }
            return false;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
