using DSAProject.Layout.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialogfeld" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.MessageDialoge
{
    public sealed partial class TalentEditDialog : ContentDialog
    {
        private TalentViewModel Talent { get; set; }

        public TalentEditDialog()
        {
            this.InitializeComponent();
        }
        public void SetTalent(TalentViewModel model)
        {
            this.Talent = model;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
