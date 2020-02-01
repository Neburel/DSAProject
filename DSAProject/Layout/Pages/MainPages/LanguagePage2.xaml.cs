using DSALib.Charakter.Talente;
using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;


namespace DSAProject.Layout.Pages.MainPages
{
    public sealed partial class LanguagePage2 : Page
    {
        private LanguagePage2ViewModel viewModel = new LanguagePage2ViewModel();
        private List<LanguageViewModel> languageViewModelList = new List<LanguageViewModel>();
        public LanguagePage2()
        {
            this.InitializeComponent();
            SetAllLanguageFamiliesAsync();
        }
        public void SetAllLanguageFamiliesAsync()
        {
            new Task(async () =>
            {
                var diceChanger         = new DiceChanger();
                languageViewModelList   = new List<LanguageViewModel>();
                viewModel.DiceChanger   = diceChanger;

                foreach (var family in Game.LanguageFamilyList)
                {
                    languageViewModelList.Add(new LanguageViewModel { LanguageFamilyName = family.Name });

                    var max = family.GetHighestPosition();
                    for (int i = 0; i <= max; i++)
                    {
                        var x = new LanguageViewModel(diceChanger) { LanguageFamilyName = family.Name };
                        if (family.Languages.TryGetValue(i, out TalentSpeaking language))
                        {
                            x.SpeakingTalent = language;
                        }
                        if (family.Writings.TryGetValue(i, out TalentWriting writing))
                        {
                            x.WritingTalent = writing;
                        }
                        languageViewModelList.Add(x);
                    }

                    languageViewModelList.Add(new LanguageViewModel());
                }

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    viewModel.TalentList = new ObservableCollection<LanguageViewModel>(languageViewModelList);
                    viewModel.IsLoading = false;
                });

            }).Start();
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<LanguageViewModel> newList = new List<LanguageViewModel>();
            if (string.IsNullOrEmpty(sender.Text))
            {
                newList = languageViewModelList;
            }
            else
            {
                newList = languageViewModelList.Where(x => (x.WritingTalent != null && x.WritingTalent.Name.ToLower().Contains(sender.Text.ToLower())) ||
                                                        (x.SpeakingTalent != null && x.SpeakingTalent.Name.ToLower().Contains(sender.Text.ToLower())) ||
                                                        (x.SpeakingTalent == null && x.WritingTalent == null)).ToList();
            }

            viewModel.TalentList.Clear();
            foreach (var item in newList)
            {
                viewModel.TalentList.Add(item);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)sender).Text;
            viewModel.DiceText = text;

            if (Int32.TryParse(text, out int value))
            {
                viewModel.DiceChanger.DiceResult = value;
            }
        }
        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }

    internal class LanguagePage2ViewModel : AbstractPropertyChanged
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
        public string FamilyName
        {
            get => Get<string>();
            set => Set(value);
        }
        public DiceChanger DiceChanger
        {
            get => Get<DiceChanger>();
            set => Set(value);
        }
        public ObservableCollection<LanguageViewModel> TalentList
        {
            get => Get<ObservableCollection<LanguageViewModel>>();
            set => Set(value);
        }
        public LanguagePage2ViewModel()
        {
            IsLoading = true;
        }
    }
}
