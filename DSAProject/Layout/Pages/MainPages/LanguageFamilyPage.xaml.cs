using DSAProject.Classes.Game;
using DSAProject.util;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DSAProject.Layout.Pages.MainPages
{
    public sealed partial class LanguageFamilyPage : Page
    {
        private LanguageFamilyPageViewModel viewModel = new LanguageFamilyPageViewModel();
        public LanguageFamilyPage()
        {
            this.InitializeComponent();
            CreatePage();
            
        }
        private void CreatePage()
        {
            ObservableCollection<LanguagePage2> list = new ObservableCollection<LanguagePage2>();
            foreach (var family in Game.LanguageFamilies)
            {
                LanguagePage2 page = new LanguagePage2();
                page.SetLanguageFamilyAsync(family);
                list.Add(page);

                //page = new LanguagePage2();
                //if (counter == 0)
                //{
                //    var header = page.GetLanguagePageHeaderListView();
                //    XAML_MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //    XAML_MainGrid.Children.Add(header);

                //    Grid.SetRow(header, counter);
                //    counter++;
                //}


                //page.SetLanguageFamilyAsync(family);

                //XAML_MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //XAML_MainGrid.Children.Add(page);
                //Grid.SetRow(page, counter);


                //counter++;
            }
            viewModel.List = list;
        }
        private class LanguageFamilyPageViewModel : AbstractPropertyChanged
        {
            public ObservableCollection<LanguagePage2> List
            {
                get => Get<ObservableCollection<LanguagePage2>>();
                set => Set(value);
            }
        }
    }
}
