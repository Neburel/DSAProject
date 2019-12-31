using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
using DSAProject.Layout.Pages.ItemPages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.MainPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LanguagePage : Page
    {
        private int counter = 0;

        public LanguagePage()
        {
            this.InitializeComponent();

            foreach (var family in Game.LanguageFamilies)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(Windows.UI.Colors.LightSlateGray),
                    BorderThickness = new Thickness(1),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                var title = new TextBlock
                {
                    Text = family.Name,
                    Padding = new Thickness(0, 10, 0, 0),
                    HorizontalTextAlignment = TextAlignment.Center

                };
                border.Child = title;
                
                XAML_Grid.Children.Add(border);
                Grid.SetRow(border, counter);
                counter++;

                var max = family.GetHighestPosition();
                for (int i=0; i<=max; i++)
                {
                    Language_ItemPage x = new Language_ItemPage();
                    if(family.Languages.TryGetValue(i, out TalentSpeaking language))
                    {
                        x.SetLanguageTalent(language);
                    }
                    if (family.Writings.TryGetValue(i, out TalentWriting writing))
                    {
                        x.SetWritingTalent(writing);
                    }
                    AddNewRow(x);
                }
            }
        }
        private void AddNewRow(Language_ItemPage item)
        {
            XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

            XAML_Grid.Children.Add(item);
            Grid.SetRow(item, counter);
            counter++;
        }
    }
}
