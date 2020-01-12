using DSAProject.Classes.Game;
using DSAProject.Layout.MessageDialoge;
using DSAProject.util;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.BasePages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AdventurePointPage : Page
    {
        private AdventurePointPageViewModel viewModel = new AdventurePointPageViewModel();
        public AdventurePointPage()
        {
            this.InitializeComponent();
            Game.Charakter.Other.ChangedEarnedAP += (sender, args) =>
            {
                viewModel.AP = args;
            };
            Game.Charakter.Other.ChangedInvestAP += (sender, args) =>
            {
                viewModel.InvestedAP = args;
            };
            Game.Charakter.Other.ChangedRestAP += (sender, args) =>
            {
                viewModel.RestAP = args;
            };
            Game.Charakter.Other.LevelChanged += (sender, args) =>
            {
                viewModel.Level = args;
            };

            viewModel.AP = Game.Charakter.Other.APEarnedMax;
            viewModel.InvestedAP = Game.Charakter.Other.APInvestedMax;
            viewModel.Level = Game.Charakter.Other.Level;
            viewModel.RestAP = Game.Charakter.Other.RestAP;
        }

        private async void XAML_CurrentAPAdd_Clicked(object sender, object e)
        {
            var dialog = new InvestAPDialog();
            dialog.Mode = InvestApDialogMode.Add;
            var result = await dialog.ShowAsync();

            if(result == ContentDialogResult.Secondary)
            {
                var currentValue = Game.Charakter.Other.APEarned;
                if (dialog.Add)
                {
                    currentValue = currentValue + dialog.Value;
                }
                else
                {
                    currentValue = currentValue - dialog.Value;
                }
                Game.Charakter.Other.APEarned = currentValue;
            }
        }

        private async void XAML_CurrentAPInvest_Clicked(object sender, object e)
        {
            var dialog = new InvestAPDialog();
            dialog.Mode = InvestApDialogMode.Invest;
            var result = await dialog.ShowAsync();

            if(result == ContentDialogResult.Secondary)
            {
                var currentValue = Game.Charakter.Other.APInvested;
                if (dialog.Add)
                {
                    currentValue = currentValue + dialog.Value;
                }
                else
                {
                    currentValue = currentValue - dialog.Value;
                }
                Game.Charakter.Other.APInvested = currentValue;
            }
        }

        private class AdventurePointPageViewModel : AbstractPropertyChanged
        {
            private SolidColorBrush textColor = new SolidColorBrush(Windows.UI.Colors.Black);
            private int ap = 0;
            private int level = 1;
            private int investedap = 0;
            private int restAP = 0;


            public SolidColorBrush TextColor
            {
                get => textColor;
                set
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
            public int AP
            {
                get => ap;
                set
                {
                    ap = value;
                    OnPropertyChanged(nameof(AP));
                }
            }
            public int Level
            {
                get => level;
                set
                {
                    level = value;
                    OnPropertyChanged(nameof(Level));
                }
            }
            public int RestAP
            {
                get => restAP;
                set
                {
                    restAP = value;
                    OnPropertyChanged(nameof(RestAP));
                }
            }
            public int InvestedAP
            {
                get => investedap;
                set
                {
                    investedap = value;
                    OnPropertyChanged(nameof(InvestedAP));
                }
            }
        }

    }
}
