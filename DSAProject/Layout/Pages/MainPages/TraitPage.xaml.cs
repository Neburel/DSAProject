using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TraitPage : Page
    {
        public enum TraitStyleEn
        {
            Min = 1,
            Max = 2
        }

        #region Variables
        private string filterText = "";
        private List<Trait> traitList = new List<Trait>();
        private readonly TraitPageViewModel viewModel = new TraitPageViewModel();
        private readonly TraitViewModel createNewTrait = new TraitViewModel() { Trait = new Trait { Description = "CreateNew" } };
        private TraitType? trailFilter = null;
        #endregion
        #region Properties
        public SolidColorBrush TextColor
        {
            set
            {
                viewModel.Header.TextColor  = value;
                createNewTrait.TextColor    = value;
            }
            get => TextColor;
        }
        public TraitStyleEn TraitPageStyle
        {
            set
            {
                object header_Template = null;
                object item_Template = null;
                if (value == TraitStyleEn.Min)
                {
                    viewModel.IsSeachVisible = Visibility.Collapsed;
                    Resources.TryGetValue("DataTemplateHeaderMin", out header_Template);
                    Resources.TryGetValue("DataTemplateItemMin", out item_Template);
                }
                else if(value == TraitStyleEn.Max)
                {
                    viewModel.IsSeachVisible = Visibility.Visible;
                    Resources.TryGetValue("DataTemplateHeaderMax", out header_Template);
                    Resources.TryGetValue("DataTemplateItemMax", out item_Template);
                }

                XAML_TraitPageListView.HeaderTemplate = (Windows.UI.Xaml.DataTemplate)header_Template;
                XAML_TraitPageListView.ItemTemplate = (Windows.UI.Xaml.DataTemplate)item_Template;
            }
        }
        private List<TraitType> SelectionList { get; set; } = new List<TraitType>();
        public TraitType TraitFilter
        {
            set
            {
                trailFilter = value;
                if (value != TraitType.Keiner)
                {
                    traitList = Game.Charakter.Traits.GetTraits().Where(x => x.TraitType == value).ToList();
                }
                else
                {
                    traitList = Game.Charakter.Traits.GetTraits();
                }
                CreateNewItemList(traitList);
                trailFilter = value;
            }
        }
        #endregion
        public TraitPage()
        {
            this.InitializeComponent();
            foreach (TraitType traitType in (TraitType[])Enum.GetValues(typeof(TraitType))) 
            {
                SelectionList.Add(traitType);
            }
        }
        #region PageHandler
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TraitFilter = (TraitType) ((ComboBox)sender).SelectedItem;
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            filterText = sender.Text.ToLower();
            CreateNewItemList(traitList);
        }
        #endregion
        private void CreateNewItemList(List<Trait> traits)
        {
            var list = new List<TraitViewModel>();
            var textFilterTraitList = traits.Where(x => x.Title == null || x.Title.ToLower().Contains(filterText.ToLower())).ToList();

            foreach (var item in textFilterTraitList)
            {
                var newWrapper = new TraitViewModel
                {
                    TextColor = viewModel.Header.TextColor,
                    Trait = item
                };
                list.Add(newWrapper);
            }
            
            viewModel.Traits = new ObservableCollection<TraitViewModel>(list)
            {
                createNewTrait
            };
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem == createNewTrait)
            {
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = trailFilter });
            }
            else
            {
                var traitwrapper = (TraitViewModel)e.ClickedItem;
                Game.RequestNav(new EventNavRequest { Side = NavEnum.CreateTraitPage, Parameter = traitwrapper.Trait });
            }
        }
        private class TraitPageViewModel : AbstractPropertyChanged
        {
            public TraitPageHeader Header
            {
                get;
                set;
            } = new TraitPageHeader();

            public Visibility IsSeachVisible
            {
                get => Get<Visibility>();
                set => Set(value);
            }          
            public ObservableCollection<TraitViewModel> Traits
            {
                get => Get<ObservableCollection<TraitViewModel>>();
                set => Set(value);
            }
        }
    }
    public class TraitPageHeader : AbstractPropertyChanged
    {
        private SolidColorBrush textColor = new SolidColorBrush(Windows.UI.Colors.Black);

        public SolidColorBrush TextColor
        {
            get => textColor;
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }
    }
}
