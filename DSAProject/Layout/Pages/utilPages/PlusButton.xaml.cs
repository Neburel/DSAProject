using DSAProject.util;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ItemPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PlusButton : Page
    {
        public event EventHandler<object> Clicked;
        public bool IsInversed
        {
            get => viewModel.IsInversed;
            set => viewModel.IsInversed = value;
        }
        public SolidColorBrush TextColor
        {
            set
            {
                viewModel.TextColor = value;
            }
        }
        private PlusButton_ViewModel viewModel = new PlusButton_ViewModel();

        public PlusButton()
        {
            this.InitializeComponent();
        }

        private class PlusButton_ViewModel : AbstractPropertyChanged
        {
            private bool isInversed = false;
            private string symbol = "+";
            private SolidColorBrush borderColor = new SolidColorBrush(Windows.UI.Colors.Green);
            private SolidColorBrush textColor = new SolidColorBrush(Windows.UI.Colors.Black);
            public bool IsInversed
            {
                get => isInversed;
                set
                {
                    isInversed = value;
                    if (value)
                    {
                        Symbol = "-";
                        BorderColor = new SolidColorBrush(Windows.UI.Colors.Red);
                    }
                    else
                    {
                        Symbol = "+";
                        BorderColor = new SolidColorBrush(Windows.UI.Colors.Green);                        
                    }

                    OnPropertyChanged(nameof(IsInversed));
                }
            }
            public string Symbol
            {
                get => symbol;
                set
                {
                    symbol = value;
                    OnPropertyChanged(nameof(Symbol));
                }
            }
            public SolidColorBrush BorderColor
            {
                get => borderColor;
                set
                {
                    borderColor = value;
                    OnPropertyChanged(nameof(BorderColor));
                }
            }
            public SolidColorBrush TextColor
            {
                get => textColor;
                set
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }

        private void XML_ButtonHigherValue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
    }
}
