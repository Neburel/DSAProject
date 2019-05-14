using DSAProject.Classes.Game;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AttributPage : Page
    {
        public AttributPage()
        {
            this.InitializeComponent();
            XAML_Grid.RowDefinitions.Add(new RowDefinition());

            var attribute   = Game.Charakter.Attribute;
            var i           = 1;
            foreach(var item in attribute.UsedAttributs)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition());
                var newView = new AKT_MOD_MAX_ItemPage
                {
                    Item = item,
                    ItemName = item.ToString(),
                    AKTValue = attribute.GetAttributAKTValue(item, out Error error)
                };

                /*
                attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.AKTValue = value;
                    }
                };
                */


                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAttributAKTValue(item, currentValue + 1, out error);
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAttributAKTValue(item, currentValue - 1, out error);
                };
                

                XAML_Grid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;
            }
        }
    }
}
