using DSALib.Utils;
using DSAProject.Classes.Game;
using DSAProject.Layout.Views;
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

            #region Create Summe
            XAML_Grid.RowDefinitions.Add(new RowDefinition());
            var newSumView = new AKT_MOD_MAX_ItemPage(110);
            newSumView.ViewModel.Name = "Gesamt";
            newSumView.ViewModel.IsModVisible = false;
            newSumView.ViewModel.IsValueEditable = false;
            newSumView.ViewModel.AKTValue = attribute.GetSumValueAttributeAKT;
            newSumView.ViewModel.MODValue = attribute.GetSumValueAttributMod;
            #endregion

            var i           = 0;
            foreach(var item in attribute.UsedAttributs)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition());
                #region NewView
                var newView = new AKT_MOD_MAX_ItemPage(110);
                newView.ViewModel.AKTValue  = attribute.GetAttributAKTValue(item, out Error error);
                newView.ViewModel.Name      = item.ToString();
                if (i == 0) { newView.IsTitleVisible = true; }
                #endregion
                attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.ViewModel.AKTValue = value;
                        newSumView.ViewModel.AKTValue = attribute.GetSumValueAttributeAKT; 
                    }
                };


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
            #region Set Summe Row
            XAML_Grid.Children.Add(newSumView);
            Grid.SetRow(newSumView, i);
            #endregion
        }
    }
}
