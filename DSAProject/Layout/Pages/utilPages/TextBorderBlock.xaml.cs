using DSAProject.util;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TextBorderBlock : Page
    {
        #region Variables
        private TextBorderBlockViewModel viewModel = new TextBorderBlockViewModel();
        #endregion
        #region Properties
        public string Text
        {
            get => viewModel.Text;
            set
            {
                viewModel.Text = value;
            }
        }
        #endregion

        public TextBorderBlock()
        {
            this.InitializeComponent();
        }
        private class TextBorderBlockViewModel : AbstractPropertyChanged
        {
            private string text = string.Empty;
            public string Text
            {
                get => text;
                set
                {
                    if(text != value)
                    {
                        text = value;
                        OnPropertyChanged(nameof(Text));
                    }
                }
            }
        }
    }
}
