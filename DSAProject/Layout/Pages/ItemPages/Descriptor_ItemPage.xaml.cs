using DSAProject.Classes.Charakter.Description;
using System.Drawing;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Descriptor_ItemPage : Page
    {
        private Descriptor descriptor;
        private int widthTitle;
        private int widthText;
        private int fontSize;

        public Descriptor Descriptor
        {
            get => descriptor;
            set
            {
                XAML_Text.Text = value.DescriptionText;
                XAML_Title.Text = value.DescriptionTitle;

                descriptor = value;
            }
        }
        public int WidthTitle
        {
            get => widthTitle;
            set
            {
                XAML_Title.Width = value;
                widthTitle = value;
            }
        }
        public int WIdthText
        {
            get => widthText;
            set
            {
                XAML_Text.Width = value;
                widthText = value;
            }
        }
        public int OwnFontSize
        {
            get => fontSize;
            set
            {
                XAML_Text.FontSize = value;
                XAML_Title.FontSize = value;
                fontSize = value;
            }
        }

        public Descriptor_ItemPage()
        {
            this.InitializeComponent();


            widthText = 150;
            widthTitle = 150;
            OwnFontSize = 20;

        }
    }
}
