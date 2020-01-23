using DSAProject.Classes.Game;
using DSAProject.Layout.Wrapper;
using DSAProject.util;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.MainPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TalentPage2 : Page
    {
        private TalentPageViewModel viewModel = new TalentPageViewModel();

        public TalentPage2()
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

            ObservableCollection<TalentWrapper> talentList = new ObservableCollection<TalentWrapper>();

            foreach (var item in Game.TalentList)
            {
                var innerItem = new TalentWrapper
                {
                    Talent = item
                };
                talentList.Add(innerItem);
            }
            viewModel.TalentList = talentList;
        }
    }
    internal class TalentPageHeader
    {
    }
    internal class TalentPageViewModel : AbstractPropertyChanged
    {
        public TalentPageHeader Header { get; set; } = new TalentPageHeader();
        private ObservableCollection<TalentWrapper> talentList;
        public ObservableCollection<TalentWrapper> TalentList
        {
            get => talentList;
            set
            {
                talentList = value;
                OnPropertyChanged(nameof(TalentList));
            }
        }
    }
}
