using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.util;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ItemPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Language_ItemPage : Page
    {
        private Language_ItemPageViewModel ViewModel { get; set; } = new Language_ItemPageViewModel();

        public Language_ItemPage()
        {
            this.InitializeComponent();
        }
        public void SetLanguageTalent(TalentLanguage item)
        {
            ViewModel.LanguageTalent = item;
        }
        public void SetWritingTalent(TalentWriting item)
        {
            ViewModel.WritingTalent = item;
            
        }



        private class Language_ItemPageViewModel : AbstractPropertyChanged
        {
            private AbstractTalent languageTalent;


            public AbstractTalent LanguageTalent
            {
                get => languageTalent;
                set
                {
                    languageTalent = value;
                    OnPropertyChanged(nameof(languageTalent));
                }
            }
            public AbstractTalent WritingTalent { get; set; }

            public string LanguageTAW { get; set; }
            public string LanguageProbe { get; set; }
            public string LanguageM { get; set; }

            public string WritingTaw { get; set; }
            public string WritingProbe { get; set; }
        }
    }
}
