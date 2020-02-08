using DSAProject.Classes.Game;
using DSAProject.Layout.MessageDialoge;
using DSAProject.util;
using System;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.BasePages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MoneyPage : Page
    {
        private MoneyPageViewModel viewModel = new MoneyPageViewModel();

        public MoneyPage()
        {
            this.InitializeComponent();
            SetMoney();
        }
        private async void PlusButton_Clicked(object sender, object e)
        {
            var dialog = new AddMoneyDialog();
            var money = Game.Charakter.Money;

            dialog.DCurrent = money.D;
            dialog.SCurrent = money.S;
            dialog.HCurrent = money.H;
            dialog.KCurrent = money.K;
            dialog.BankCurrent = money.Bank;

            var result = await dialog.ShowAsync();
            if(result  == ContentDialogResult.Secondary)
            {
                money.D = dialog.DResult;
                money.S = dialog.SResult;
                money.H = dialog.HResult;
                money.K = dialog.KResukt;
                money.Bank = dialog.BankResult;

                SetMoney();
            }
        }
        private void SetMoney()
        {
            var money = Game.Charakter.Money;

            viewModel.D = money.D.ToString();
            viewModel.S = money.S.ToString();
            viewModel.H = money.H.ToString();
            viewModel.K = money.K.ToString();
            viewModel.Bank = money.Bank.ToString();
        }
        private class MoneyPageViewModel : AbstractPropertyChanged
        {
            public string D
            {
                get => Get<string>();
                set 
                {
                    if (value == D) return;
                    var newValue = value + " D";
                    Set(newValue);
                }
            }
            public string S
            {
                get => Get<string>();
                set
                {
                    if (value == S) return;
                    var newValue = value + " S";
                    Set(newValue);
                }
            }
            public string H
            {
                get => Get<string>();
                set
                {
                    if (value == H) return;
                    var newValue = value + " H";
                    Set(newValue);
                }
            }
            public string K
            {
                get => Get<string>();
                set
                {
                    if (value == K) return;
                    var newValue = value + " K";
                    Set(newValue);
                }
            }
            public string Bank
            {
                get => Get<string>();
                set
                {
                    if (value == Bank) return;
                    var newValue = value + " D";
                    Set(newValue);
                }
            }
        }
    }
}
