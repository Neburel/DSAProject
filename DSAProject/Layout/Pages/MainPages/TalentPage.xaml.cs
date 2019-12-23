using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Pages.ItemPages;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TalentPage : Page
    {
        private int counter = 0;
        private List<Talent_ItemPage> items = new List<Talent_ItemPage>();

        public TalentPage()
        {
            this.InitializeComponent();

            int counter = 0;
            foreach(var item in Game.Charakter.Attribute.UsedAttributs)
            {
                var textBloc    = new TextBlock();
                var attribut    = Helper.GetShort(item);
                var value       = Game.Charakter.Attribute.GetAttributMAXValue(item, out Error error);
                textBloc.Text = attribut + " " + value;
                textBloc.HorizontalAlignment = HorizontalAlignment.Center;
                textBloc.VerticalAlignment = VerticalAlignment.Center;

                XAML_TitleRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                XAML_TitleRow.Children.Add(textBloc);
                Grid.SetColumn(textBloc, counter);
                counter++;
            }
        }
        public void SetTalents(List<ITalent> talent)
        {
            if (talent != null && talent.Count > 0)
            {
                var collection = talent.OrderBy(x => x.ToString()).ToList();

                if (typeof(AbstractTalentFighting).IsAssignableFrom(talent[0].GetType()))
                {
                    XAML_BorderProbeText.Visibility = Visibility.Collapsed;
                    XAML_BorderATPA.Visibility = Visibility.Visible;
                }
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent[0].GetType()))
                {
                    XAML_BorderProbeText.Visibility = Visibility.Visible;
                    XAML_BorderATPA.Visibility = Visibility.Collapsed;
                }

                foreach (var item in collection)
                {
                    AddNewRow(new Talent_ItemPage(item));
                }
            }
        }
        private void AddNewRow(Talent_ItemPage item)
        {
            XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto)});

            XAML_Grid.Children.Add(item);
            Grid.SetRow(item, counter);
            counter++;

            items.Add(item);
        }

        private void XAML_SwitchTAWEdit_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            foreach(var item in items)
            {
                if (item.Talent.Name.StartsWith(sender.Text))
                {
                    item.Visibility = Visibility.Visible;
                } 
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
