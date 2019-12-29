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
        Add
    }
    public sealed partial class InvestAPDialog : ContentDialog
    {
        private InvestAPDialogViewModel viewModel = new InvestAPDialogViewModel();
        
        public InvestApDialogMode Mode
        {
            set
            {
                if(value == InvestApDialogMode.Add)
                {
                    viewModel.Title = "Aktuelle Abenteuerpunkte";
                    viewModel.Message = "Füge weitere Abenteuerpunkte hinzu";
                }
                else if(value == InvestApDialogMode.Invest)
                {
                    viewModel.Title = "Investierte Abenteuerpunte";
                    viewModel.Message = "Investiere deine Abenteuerpunkte!";
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
            viewModel.Title = "Investierte Abenteuerpunte";
            viewModel.Message = "Investiere deine Abenteuerpunkte!";
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
