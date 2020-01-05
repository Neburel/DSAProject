using DSALib;
using DSALib.Charakter.Other;
using DSAProject.Classes.Game;
using DSAProject.Layout.Wrapper;
using DSAProject.util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly TraitPageViewModel viewModel = new TraitPageViewModel();
        private readonly TraitWrapper createNewTrait = new TraitWrapper() { Trait = new Trait { Description = "CreateNew" } };
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
                    Resources.TryGetValue("DataTemplateHeaderMin", out header_Template);
                    Resources.TryGetValue("DataTemplateItemMin", out item_Template);
                }
                else if(value == TraitStyleEn.Max)
                {
                    Resources.TryGetValue("DataTemplateHeaderMax", out header_Template);
                    Resources.TryGetValue("DataTemplateItemMax", out item_Template);
                }

                XAML_TraitPageListView.HeaderTemplate = (Windows.UI.Xaml.DataTemplate)header_Template;
                XAML_TraitPageListView.ItemTemplate = (Windows.UI.Xaml.DataTemplate)item_Template;
            }
        }
        #endregion

        public TraitType TraitFilter
        {
            set
            {
                trailFilter = value;
                List<Trait> traits;
                if (value != TraitType.Keiner)
                {
                    traits = Game.Charakter.Traits.GetTraits().Where(x => x.TraitType == value).ToList();
                }
                else
                {
                    traits = Game.Charakter.Traits.GetTraits();
                }
                CreateNewItemList(traits);
                trailFilter = value;
            }
        }
        public TraitPage()
        {
            this.InitializeComponent();
        }
        private void CreateNewItemList(List<Trait> traits)
        {
            var list = new List<TraitWrapper>();

            foreach (var item in traits)
            {
                var newWrapper = new TraitWrapper
                {
                    TextColor = viewModel.Header.TextColor,
                    Trait = item
                };
                list.Add(newWrapper);
            }

            viewModel.Traits = new ObservableCollection<TraitWrapper>(list)
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
                var traitwrapper = (TraitWrapper)e.ClickedItem;
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

            private ObservableCollection<TraitWrapper> traits;
            public ObservableCollection<TraitWrapper> Traits
            {
                get => traits;
                set
                {
                    traits = value;
                    OnPropertyChanged(nameof(Traits));
                }
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
