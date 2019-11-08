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
        public TraitTalentBonusSelectionPage_Mode Mode
        {
            set
            {
                mode = value;
                switch (value)
                {
                    case TraitTalentBonusSelectionPage_Mode.All:
                        viewModel.TalentList = new List<ITalent>(Game.TalenteDSA.OrderBy(x => x.Name));
                        viewModel.Title = "Talent TaW Bonus";
                        break;
                    case TraitTalentBonusSelectionPage_Mode.AT:
                        viewModel.TalentList = new List<ITalent>(Game.TalenteDSA.Where(x => typeof(AbstractTalentFighting).IsAssignableFrom(x.GetType())).OrderBy(x => x.Name));
                        viewModel.Title = "Talent AT Bonus";
                        break;
                    case TraitTalentBonusSelectionPage_Mode.PA:
                        viewModel.TalentList = new List<ITalent>(Game.TalenteDSA.Where(x => typeof(AbstractTalentFighting).IsAssignableFrom(x.GetType()) && x.GetType() != typeof(TalentRange)).OrderBy(x => x.Name));
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

            var k = new List<ITalent>(Game.TalenteDSA.Where(x => x.GetType() == typeof(AbstractTalentFighting)).OrderBy(x => x.Name));
            var q = Game.TalenteDSA.Where(x => x.GetType() == typeof(AbstractTalentFighting));

            foreach (var item in Game.TalenteDSA)
            {
                var type = item.GetType();
                if (typeof(AbstractTalentFighting).IsAssignableFrom(type))
                {

                }
            }
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
                var innerValue = viewModel.TalenteTaw.Where(x => x.Talent == talent).FirstOrDefault();
                var removed = viewModel.TalenteTaw.Remove(innerValue);
                if (add)
                {
                    if (value > 0)
                    {
                        TraitTalentBonus element = null;
                        if (removed)
                        {
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
                else
                {
                    if (removed)
                    {
                        RemoveTrait(this, innerValue);
                    }
                }
            }
        }
        #endregion
        private class CreateTrait_ViewModel : AbstractPropertyChanged
        {
            private string title = "Talent TaW Bonus";
            private ITalent selectedItem;
            private TraitTalentBonus selectedListViewItem;
            private List<ITalent> talentList = new List<ITalent>(Game.TalenteDSA.OrderBy(x => x.Name));

            public bool DeselectItem { get; set; } = true;

            public ITalent SelectedItem
            {
                get => selectedItem;
                set
                {
                    if(value != selectedItem)
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
            public TraitTalentBonus SelectedListViewItem
            {
                get => selectedListViewItem;
                set
                {
                    if(selectedListViewItem != value)
                    {
                        selectedListViewItem = value;
                        OnPropertyChanged(nameof(SelectedListViewItem));
                    }
                }
            }

            public string Title
            {
                get => title;
                set
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
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
            public ObservableCollection<TraitTalentBonus> TalenteTaw { get; set; } = new ObservableCollection<TraitTalentBonus>();
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (TraitTalentBonus)(e.ClickedItem);


            if (viewModel.SelectedItem == viewModel.SelectedItem)
            {
                viewModel.SelectedItem = null;
                viewModel.SelectedListViewItem = null;
            }
            else
            {
                viewModel.DeselectItem = false;
                viewModel.SelectedItem = item.Talent;
                viewModel.DeselectItem = true;
            }
        }
    }
}
