using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static DSAProject.util.Hilfsklassen;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ItemPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TraitTalentBonusSelectionPage : Page
    {
        #region Event
        public event EventHandler<TraitTalentBonus> AddTrait;
        public event EventHandler<TraitTalentBonus> RemoveTrait;
        #endregion
        #region Variables
        public TraitTalentBonusSelectionPage_Mode mode = TraitTalentBonusSelectionPage_Mode.All;
        #endregion
        #region Properties
        private CreateTrait_ViewModel viewModel = new CreateTrait_ViewModel();
        public SolidColorBrush TextColor
        {
            set
            {
                viewModel.TextColor = value;
            }
        }
        public TraitTalentBonusSelectionPage_Mode Mode
        {
            set
            {
                mode = value;
                switch (value)
                {
                    case TraitTalentBonusSelectionPage_Mode.All:
                        viewModel.TalentList = new List<ITalent>(Game.TalentList.OrderBy(x => x.Name));
                        viewModel.Title = "Talent TaW Bonus";
                        break;
                    case TraitTalentBonusSelectionPage_Mode.AT:
                        viewModel.TalentList = new List<ITalent>(Game.TalentList.Where(x => typeof(AbstractTalentFighting).IsAssignableFrom(x.GetType())).OrderBy(x => x.Name));
                        viewModel.Title = "Talent AT Bonus";
                        break;
                    case TraitTalentBonusSelectionPage_Mode.PA:
                        viewModel.TalentList = new List<ITalent>(Game.TalentList.Where(x => typeof(AbstractTalentFighting).IsAssignableFrom(x.GetType()) && x.GetType() != typeof(TalentRange)).OrderBy(x => x.Name));
                        viewModel.Title = "Talent PA Bonus";
                        break;
                }
            }
            get => mode;
        }
        #endregion
        public TraitTalentBonusSelectionPage()
        {
            this.InitializeComponent();
        }
        #region Handler
        private void XAML_AKTMINMAX_Event_ValueHigher(object sender, EventArgs e)
        {
            XAML_AKTMINMAX.AKTValue = nextValue(XAML_AKTMINMAX.AKTValue, true);
        }
        private void RemoveTalent_Clicked(object sender, object e)
        {
            NewTalent(false);
        }
        private void AddTalent_Clicked(object sender, object e)
        {
            NewTalent(true);
        }
        private void XAML_AKTMINMAX_Event_ValueLower(object sender, EventArgs e)
        {
            XAML_AKTMINMAX.AKTValue = nextValue(XAML_AKTMINMAX.AKTValue, false);
        }
        #endregion
        #region Hilfsmethoden
        private string nextValue(string current, bool plus)
        {
            if (int.TryParse(current, out int value))
            {
                if (plus) return (value + 1).ToString();
                return (value - 1).ToString();
            }
            else
            {
                return (0).ToString();
            }
        }
        private void NewTalent(bool add)
        {
            var value = (ITalent)XAML_ComboBoxTalent.SelectedItem;
            if (Int32.TryParse(XAML_AKTMINMAX.AKTValue, out int intValue))
            {
                NewTalent(add, value, intValue);
            }
            else
            {
                NewTalent(add, value);
            }
        }
        public void NewTalent(bool add, ITalent talent, int value = 0)
        {
            if (talent != null)
            {
                var k = viewModel.TalenteTaw;
                var innerValue = viewModel.TalenteTaw.Where(x => x.Talent == talent).FirstOrDefault();
                if (add == true && (innerValue == null || innerValue.Value != value))
                {
                    var removed = viewModel.TalenteTaw.Remove(innerValue);

                    {
                        if (value != 0)
                        {
                            TraitTalentBonus element = null;
                            if (removed)
                            {
                                element = innerValue;
                                innerValue.Talent = talent;
                                innerValue.Value = value;
                            }
                            else
                            {
                                element = new TraitTalentBonus
                                {
                                    Talent = talent,
                                    Value = value
                                };
                            }
                            viewModel.TalenteTaw.Add(element);
                            AddTrait?.Invoke(this, element);
                        }
                    }
                }
                else if (add == false)
                {
                    var removed = viewModel.TalenteTaw.Remove(innerValue);
                    if (removed)
                    {
                        RemoveTrait(this, innerValue);
                    }
                }
            }
        }
        #endregion
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (TraitTalentBonus)(e.ClickedItem);
            viewModel.DeselectItem = false;
            viewModel.SelectedItem = item.Talent;
            viewModel.DeselectItem = true;
        }
        private class CreateTrait_ViewModel : AbstractPropertyChanged
        {
            #region Variables
            private string title = "Talent TaW Bonus";
            private ITalent selectedItem;
            private TraitTalentBonus selectedListViewItem;
            private List<ITalent> talentList = new List<ITalent>(Game.TalentList.OrderBy(x => x.Name));
            private SolidColorBrush textColor = new SolidColorBrush(Windows.UI.Colors.Black);
            #endregion

            public CreateTrait_ViewModel()
            {
                TalenteTaw.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (TraitTalentBonus item in args.NewItems)
                        {
                            item.TextColor = TextColor;
                        }
                    }
                };
            }

            public bool DeselectItem { get; set; } = true;
            public string Title
            {
                get => title;
                set
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
            public ITalent SelectedItem
            {
                get => selectedItem;
                set
                {
                    if (value != selectedItem)
                    {
                        selectedItem = value;
                        OnPropertyChanged(nameof(SelectedItem));

                        if (DeselectItem)
                        {
                            SelectedListViewItem = null;
                        }
                    }
                }
            }
            public List<ITalent> TalentList
            {
                get => talentList;
                set
                {
                    talentList = value;
                    OnPropertyChanged(nameof(TalentList));
                }
            }
            public TraitTalentBonus SelectedListViewItem
            {
                get => selectedListViewItem;
                set
                {
                    if (selectedListViewItem != value)
                    {
                        selectedListViewItem = value;
                        OnPropertyChanged(nameof(SelectedListViewItem));
                    }
                }
            }
            public SolidColorBrush TextColor
            {
                get => textColor;
                set
                {
                    textColor = value;
                    foreach(var item in TalenteTaw)
                    {
                        item.TextColor = value;
                    }

                    OnPropertyChanged(nameof(TextColor));
                    
                    OnPropertyChanged(nameof(TalenteTaw));
                }
            }
            public ObservableCollection<TraitTalentBonus> TalenteTaw { get; private set; } = new ObservableCollection<TraitTalentBonus>();
        }
    }
}
