using DSAProject.Layout.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AttributPage : Page
    {
        private bool isModVisible = true;
        private bool isShowingSum = true;

        public bool IsShowingSum
        {
            get => isShowingSum;
            set
            {
                isShowingSum = value;
                CreateNew();
            }
        }
        public bool IsModVisible
        {
            get => isModVisible;
            set
            {
                isModVisible = value;
                CreateNew();
            }
        }

        public AttributPage()
        {
            this.InitializeComponent();
            CreateNew();
        }
        private void CreateNew()
        {
            XAML_Grid.Children.Clear();
            Visibility visibility = Visibility.Collapsed;
            if (isModVisible) visibility = Visibility.Visible;

            PageHelpBuilder.BuildAttributPage(XAML_Grid);
        }
    }
}
