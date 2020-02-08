using DSAProject.Classes.Game;
using DSAProject.util;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Inhaltsdialogfeld" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.MessageDialoge
{
    public enum InvestApDialogMode
    {
        Invest,
        Add, 
        AddOnly,
        InvestOnly
    }
    public sealed partial class InvestAPDialog : ContentDialog
    {
        private InvestAPDialogViewModel viewModel = new InvestAPDialogViewModel();
        
        public InvestApDialogMode Mode
        {
            set
            {
                if(value == InvestApDialogMode.Add || value == InvestApDialogMode.AddOnly)
                {
                    viewModel.Title     = "Aktuelle Abenteuerpunkte";
                    viewModel.Message   = "Füge weitere Abenteuerpunkte hinzu";

                    if(value == InvestApDialogMode.Add)
                    {
                        viewModel.InfoAkt = "Benutzer gesetze AP: ";
                        viewModel.InfoMod = "Andere AP: ";
                        viewModel.InfoAktValue = Game.Charakter.Other.APEarned.ToString();
                        viewModel.InfoModValue = Game.Charakter.Other.APEarnedMod.ToString();

                        viewModel.ChoiceVisibility = Visibility.Visible;
                        viewModel.InfoBarVisibility = Visibility.Visible;
                    }
                    else if(value == InvestApDialogMode.AddOnly)
                    {
                        viewModel.ChoiceVisibility = Visibility.Collapsed;
                        viewModel.InfoBarVisibility = Visibility.Collapsed;
                    }
                }
                else if(value == InvestApDialogMode.Invest || value == InvestApDialogMode.InvestOnly)
                {
                    viewModel.Title = "Investierte Abenteuerpunte";
                    viewModel.Message = "Investiere deine Abenteuerpunkte!";

                    if (value == InvestApDialogMode.Invest)
                    {
                        viewModel.InfoAkt = "Benutzer investierte AP: ";
                        viewModel.InfoMod = "Andere investierte AP: ";
                        viewModel.InfoAktValue = Game.Charakter.Other.APInvested.ToString();
                        viewModel.InfoModValue = Game.Charakter.Other.APInvestedMod.ToString();

                        viewModel.ChoiceVisibility = Visibility.Visible;
                        viewModel.InfoBarVisibility = Visibility.Visible;
                    }
                    else if (value == InvestApDialogMode.InvestOnly)
                    {
                        viewModel.ChoiceVisibility = Visibility.Collapsed;
                        viewModel.InfoBarVisibility = Visibility.Collapsed;
                    }
                }
            }
        }
        public bool Add { get; set; }
        public int Value
        {
            get => viewModel.Value;
        }

        public InvestAPDialog()
        {
            this.InitializeComponent();
            Mode = InvestApDialogMode.Add;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }

        private class InvestAPDialogViewModel : AbstractPropertyChanged
        {
            private int value = 0;
            private string title = string.Empty;
            private string message = string.Empty;
            private string infoAkt = string.Empty;
            private string infoMod = string.Empty;
            private string infoAktValue = string.Empty;
            private string infoModValue = string.Empty;
            private Visibility choiceVisibility = Visibility.Collapsed;
            private Visibility infoBarVisibility = Visibility.Collapsed;

            public int Value
            {
                get => value;
                set
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
            public string Title
            {
                get => title;
                set
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
            public string Message
            {
                get => message;
                set
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
            public string InfoAkt
            {
                get => infoAkt;
                set
                {
                    infoAkt = value;
                    OnPropertyChanged(nameof(InfoAkt));
                }
            }
            public string InfoMod
            {
                get => infoMod;
                set
                {
                    infoMod = value;
                    OnPropertyChanged(nameof(InfoMod));
                }
            }
            public string InfoAktValue
            {
                get => infoAktValue;
                set
                {
                    infoAktValue = value;
                    OnPropertyChanged(nameof(InfoAktValue));
                }
            }
            public string InfoModValue
            {
                get => infoModValue;
                set
                {
                    infoModValue = value;
                    OnPropertyChanged(nameof(InfoModValue));
                }
            }


            public Visibility ChoiceVisibility
            {
                get => choiceVisibility;
                set
                {
                    choiceVisibility = value;
                    OnPropertyChanged(nameof(ChoiceVisibility));
                }
            }
            public Visibility InfoBarVisibility
            {
                get => infoBarVisibility;
                set
                {
                    infoBarVisibility = value;
                    OnPropertyChanged(nameof(InfoBarVisibility));
                }
            }
        }

        private void XAML_CheckedAdd_Checked(object sender, RoutedEventArgs e)
        {
            Add = true;
        }
        private void XAML_CheckedRemove_Checked(object sender, RoutedEventArgs e)
        {
            Add = false;
        }
    }
}
