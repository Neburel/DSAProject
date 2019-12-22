using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
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
            #region Variables
            private bool languageTalentBorderVisible    = false;
            private bool writingTalentBorderVisible     = false;
            private int languageTaw                     = 0;
            private int writingTaW                      = 0;
            private AbstractTalent languageTalent;
            private AbstractTalent writingTalent;
            #endregion
            public AbstractTalent LanguageTalent
            {
                get => languageTalent;
                set
                {
                    languageTalent = value;
                    LanguageTalentBorderVisible = true;
                    LanguageTAW = Game.Charakter.Talente.GetTAW(LanguageTalent);
                    OnPropertyChanged(nameof(LanguageTalent));
                }
            }
            public AbstractTalent WritingTalent 
            {
                get => writingTalent;
                set
                {
                    writingTalent = value;
                    WritingTalentBorderVisible = true;
                    WritingTAW = Game.Charakter.Talente.GetTAW(WritingTalent);
                    OnPropertyChanged(nameof(WritingTalent));
                }
            }

            public bool LanguageTalentBorderVisible
            {
                get
                {
                    return languageTalentBorderVisible;
                }
                set
                {
                    languageTalentBorderVisible = value;
                    OnPropertyChanged(nameof(LanguageTalentBorderVisible));
                }
            }
            public int LanguageTAW
            {
                get => languageTaw;
                set 
                {
                    languageTaw = value;
                    Game.Charakter.Talente.SetTAW(LanguageTalent, value);
                    OnPropertyChanged(nameof(LanguageTAW));
                }
            }
            public string LanguageProbe { get; set; }
            public string LanguageM { get; set; }

            public bool WritingTalentBorderVisible
            {
                get
                {
                    return writingTalentBorderVisible;
                }
                set
                {
                    writingTalentBorderVisible = value;
                    OnPropertyChanged(nameof(WritingTalentBorderVisible));
                }
            }
            public int WritingTAW
            {
                get => writingTaW;
                set
                {
                    writingTaW = value;
                    Game.Charakter.Talente.SetTAW(WritingTalent, value);
                    OnPropertyChanged(nameof(WritingTAW));
                }
            }
            public string WritingProbe { get; set; }
        }
    }
}
