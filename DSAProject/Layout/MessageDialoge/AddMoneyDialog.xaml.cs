using DSAProject.util;
using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Inhaltsdialogfeld" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.MessageDialoge
{
    public sealed partial class AddMoneyDialog : ContentDialog
    {
        private AddMoneyDialogViewModel viewModel = new AddMoneyDialogViewModel();

        public int DCurrent
        {
            set => viewModel.DCurrent = value;
        }
        public int SCurrent
        {
            set => viewModel.SCurrent = value;
        }
        public int HCurrent
        {
            set => viewModel.HCurrent = value;
        }
        public int KCurrent
        {
            set => viewModel.KCurrent = value;
        }
        public int BankCurrent
        {
            set => viewModel.BankCurrent = value;
        }

        public int DResult
        {
            get => viewModel.DResult;
        }
        public int SResult
        {
            get => viewModel.SResult;
        }
        public int HResult
        {
            get => viewModel.HResult;
        }
        public int KResukt
        {
            get => viewModel.KResukt;
        }
        public int BankResult
        {
            get => viewModel.BankResult;
        }

        public AddMoneyDialog()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new string(sender.Text.Where(char.IsDigit).ToArray());
        }
        private class AddMoneyDialogViewModel : AbstractPropertyChanged
        {
            public string Title
            {
                get => Get<string>();
                set => Set(value);
            }
            public int DCurrent
            {
                get => Get<int>();
                set => Set(value);
            }
            public int SCurrent
            {
                get => Get<int>();
                set => Set(value);
            }
            public int HCurrent
            {
                get => Get<int>();
                set => Set(value);
            }
            public int KCurrent
            {
                get => Get<int>();
                set => Set(value);
            }
            public int BankCurrent
            {
                get => Get<int>();
                set => Set(value);
            }

            public int DPlus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int SPlus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int HPlus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int KPlus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int BankPlus
            {
                get => Get<int>();
                set => Set(value);
            }

            public int DMinus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int SMinus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int HMinus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int KMinus
            {
                get => Get<int>();
                set => Set(value);
            }
            public int BankMinus
            {
                get => Get<int>();
                set => Set(value);
            }

            public int DResult
            {
                get => Get<int>();
                set => Set(value);
            }
            public int SResult
            {
                get => Get<int>();
                set => Set(value);
            }
            public int HResult
            {
                get => Get<int>();
                set => Set(value);
            }
            public int KResukt
            {
                get => Get<int>();
                set => Set(value);
            }
            public int BankResult
            {
                get => Get<int>();
                set => Set(value);
            }

            public AddMoneyDialogViewModel()
            {
                this.PropertyChanged += (sender, args) =>
                {
                    if(args.PropertyName == nameof(DCurrent) || args.PropertyName == nameof(DPlus) || args.PropertyName == nameof(DMinus))
                    {
                        DResult = DCurrent + DPlus - DMinus;
                    }
                    else if(args.PropertyName == nameof(SCurrent) || args.PropertyName == nameof(SPlus) || args.PropertyName == nameof(SMinus))
                    {
                        SResult = SCurrent + SPlus - SMinus;
                    }
                    else if (args.PropertyName == nameof(HCurrent) || args.PropertyName == nameof(HPlus) || args.PropertyName == nameof(HMinus))
                    {
                        HResult = HCurrent + HPlus - HMinus;
                    }
                    else if (args.PropertyName == nameof(KCurrent) || args.PropertyName == nameof(KPlus) || args.PropertyName == nameof(KMinus))
                    {
                        KResukt = KCurrent + KPlus - KMinus;
                    }
                    else if (args.PropertyName == nameof(BankCurrent) || args.PropertyName == nameof(BankPlus) || args.PropertyName == nameof(BankMinus))
                    {
                        BankResult = BankCurrent + BankPlus - BankMinus;
                    }

                };
            }
        }
    }
}
