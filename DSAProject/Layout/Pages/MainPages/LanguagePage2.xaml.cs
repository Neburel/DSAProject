using DSALib.Charakter.Talente;
using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace DSAProject.Layout.Pages.MainPages
{
    public sealed partial class LanguagePage2 : Page
    {
        private LanguagePage2ViewModel viewModel = new LanguagePage2ViewModel();
        public LanguagePage2()
        {
            this.InitializeComponent();
        }
        public void SetLanguageFamilyAsync(LanguageFamily family)
        {
            new Task(() =>
            {
                SetLanguageFamily(family);
            }).Start();
        }
        public ListView GetLanguagePageHeaderListView()
        {
            var dataTemplate = (DataTemplate)Resources["LanguageHeaderTemplate"];
            return new ListView { HeaderTemplate = dataTemplate };
        }
        private async void SetLanguageFamily(LanguageFamily family)
        {
            var list = new ObservableCollection<LanguageViewModel>();
            var max = family.GetHighestPosition();
            for (int i = 0; i <= max; i++)
            {
                var x = new LanguageViewModel();
                if (family.Languages.TryGetValue(i, out TalentSpeaking language))
                {
                    x.LanguageTalent = language;
                }
                if (family.Writings.TryGetValue(i, out TalentWriting writing))
                {
                    x.WritingTalent = writing;
                }
                list.Add(x);
            }

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                viewModel.TalentList = list;
                viewModel.IsLoading = false;
            });
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
