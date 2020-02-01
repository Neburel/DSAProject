using DSAProject.Classes.Game;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AttributHeader : Page
    {
        public AttributHeader()
        {
            this.InitializeComponent();
            int counter = 0;
            foreach (var item in Game.Charakter.Attribute.UsedAttributs)
            {
                var textBloc = new TextBlock();
                var attribut = DSALib.Utils.Helper.GetShort(item);
                var value = Game.Charakter.Attribute.GetAttributMAXValue(item);
                textBloc.Text = attribut + " " + value;
                textBloc.HorizontalAlignment = HorizontalAlignment.Center;
                textBloc.VerticalAlignment = VerticalAlignment.Center;

                XAML_TitleRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                XAML_TitleRow.Children.Add(textBloc);
                Grid.SetColumn(textBloc, counter);
                counter++;
            }
        }
    }
}
