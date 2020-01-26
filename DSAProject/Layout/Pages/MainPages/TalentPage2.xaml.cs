using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Wrapper;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        #region Variables
        private TalentPageViewModel viewModel = new TalentPageViewModel();
        private List<TalentWrapper> talentList;
        #endregion
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

            SetTalents(Game.TalentList);
        }

        public void SetTalents(List<ITalent> list)
        {
            if (list == null || list.Count == 0) return;

            talentList = new List<TalentWrapper>();
            var obList = new ObservableCollection<TalentWrapper>();

            foreach (var item in list)
            {
                var innerItem = new TalentWrapper
                {
                    Talent = item
                };
                talentList.Add(innerItem);
                obList.Add(innerItem);
            }

            //Dieser Code ist verbesserbar, aktuell nur funktional wenn die liste nur einen typen enthält
            if (typeof(AbstractTalentFighting).IsAssignableFrom(list[0].GetType()))
            {
                viewModel.Header.ATPAVisibility = true;
            }
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(list[0].GetType()))
            {
                viewModel.Header.ATPAVisibility = false;
            }

            viewModel.TalentList = obList;
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
        #endregion

    }
    internal class TalentPageHeader : AbstractPropertyChanged
    {
        public bool ATPAVisibility 
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
    internal class TalentPageViewModel : AbstractPropertyChanged
    {
        public TalentPageHeader Header
        {
            get => Get<TalentPageHeader>();
            set => Set(value);
        }
        public ObservableCollection<TalentWrapper> TalentList
        {
            get => Get<ObservableCollection<TalentWrapper>>();
            set => Set(value);
        }

        public TalentPageViewModel()
        {
            Header = new TalentPageHeader();
        }
    }
}
