using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.MainPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TalentPage2 : Page
    {
        #region Variables
        private TalentPageViewModel viewModel = new TalentPageViewModel();
        private List<TalentViewModel> talentList;
        #endregion
        public TalentPage2()
        {
            this.InitializeComponent();
        }
        public void SetTalentType(Type type)
        {
            viewModel.IsLoading     = true;
            viewModel.DiceChanger   = new DiceChanger();

            new Task(() =>
            {
                var talentList = Game.GetTalentForCurrentCharakter().Where(x => x.GetType() == type).ToList();
                GenerateTalentListAsync(talentList);
            }).Start();
        }
        private async void GenerateTalentListAsync(List<ITalent> list)
        {
            if (list == null || list.Count == 0) return;
            if (Game.Settings.OrderTalentAlphabetic)
            {
                list = list.OrderBy(x => x.ToString()).ToList();
            }
            else
            {
                list = list.OrderBy(x => x.OrginalPosition).ToList();
            }

            DiceChanger helper = viewModel.DiceChanger;
            talentList = new List<TalentViewModel>();
            var obList = new ObservableCollection<TalentViewModel>();

            //Dieser Code ist verbesserbar, aktuell nur funktional wenn die liste nur einen typen enthält
            if (typeof(AbstractTalentFighting).IsAssignableFrom(list[0].GetType()))
            {
                viewModel.Header.ATPAVisibility = true;
                viewModel.Header.ProbeTextVisibility = false;
            }
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(list[0].GetType()))
            {
                viewModel.Header.ATPAVisibility = false;
                viewModel.Header.ProbeTextVisibility = true;
            }

            foreach (var item in list)
            {
                var innerItem = new TalentViewModel(helper)
                {
                    Talent = item
                };
                talentList.Add(innerItem);
                obList.Add(innerItem);
            }

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                viewModel.TalentList = obList;
                viewModel.IsLoading = false;
            });
        }
        #region PageHandler
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var newList = talentList.Where(x => x.Talent.Name.ToLower().Contains(sender.Text.ToLower())).ToList();

            viewModel.TalentList.Clear();
            foreach(var item in newList)
            {
                viewModel.TalentList.Add(item);
            }
        }
        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)sender).Text;
            viewModel.DiceText = text;

            if(Int32.TryParse(text, out int value))
            {
                viewModel.DiceChanger.DiceResult = value;
            }
        }
        #endregion
    }
    internal class TalentPageHeader : AbstractPropertyChanged
    {
        public bool ATPAVisibility 
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool ProbeTextVisibility
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
    internal class TalentPageViewModel : AbstractPropertyChanged
    {
        public bool IsLoading
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string DiceText
        {
            get => Get<string>();
            set => Set(value);
        }
        public TalentPageHeader Header
        {
            get => Get<TalentPageHeader>();
            set => Set(value);
        }
        public DiceChanger DiceChanger
        {
            get => Get<DiceChanger>();
            set => Set(value);
        }
        public ObservableCollection<TalentViewModel> TalentList
        {
            get => Get<ObservableCollection<TalentViewModel>>();
            set => Set(value);
        }
        public TalentPageViewModel()
        {
            Header = new TalentPageHeader();

        }
    }
}
