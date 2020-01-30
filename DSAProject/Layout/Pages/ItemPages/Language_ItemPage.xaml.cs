using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
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
        private LanguageViewModel ViewModel { get; set; } = new LanguageViewModel();

        public Language_ItemPage()
        {
            this.InitializeComponent();

            Game.Charakter.Talente.TaWChanged += (sender, args) =>
            {
                if(args == ViewModel.LanguageTalent)
                {
                    ViewModel.LanguageTAW = Game.Charakter.Talente.GetMaxTaw(args);
                }
                else if(args == ViewModel.WritingTalent)
                {
                    ViewModel.WritingTAW = Game.Charakter.Talente.GetMaxTaw(args);
                }
            };
        }
        public void SetLanguageTalent(TalentSpeaking item)
        {
            ViewModel.LanguageTalent = item;
        }
        public void SetWritingTalent(TalentWriting item)
        {
            ViewModel.WritingTalent = item;            
        }
        private void CheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var box = (CheckBox)sender;
            var value = (bool)box.IsChecked;
            Game.Charakter.Talente.SetMother(ViewModel.LanguageTalent, value);
        }

        //private class Language_ItemPageViewModel : AbstractPropertyChanged
        //{
        //    #region Variables
        //    private bool languageTalentBorderVisible    = false;
        //    private bool writingTalentBorderVisible     = false;
        //    private bool motherLanguage                 = false;
        //    private int languageTaw                     = 0;
        //    private int writingTaW                      = 0;
        //    private string languageProbeString          = string.Empty;
        //    private string writingProbeString           = string.Empty;
        //    private TalentSpeaking languageTalent;
        //    private AbstractTalent writingTalent;
        //    #endregion
        //    public TalentSpeaking LanguageTalent
        //    {
        //        get => languageTalent;
        //        set
        //        {
        //            languageTalent  = value;
        //            LanguageTalentBorderVisible = true;
        //            LanguageTAW     = Game.Charakter.Talente.GetMaxTaw(LanguageTalent);
        //            LanguageProbe   = Game.Charakter.Talente.GetProbeString(LanguageTalent);
        //            LanguageM       = Game.Charakter.Talente.GetMother(LanguageTalent);
        //            OnPropertyChanged(nameof(LanguageTalent));
        //        }
        //    }
        //    public AbstractTalent WritingTalent 
        //    {
        //        get => writingTalent;
        //        set
        //        {
        //            writingTalent = value;
        //            WritingTalentBorderVisible = true;
        //            WritingTAW      = Game.Charakter.Talente.GetMaxTaw(WritingTalent);
        //            WritingProbe    = Game.Charakter.Talente.GetProbeString(WritingTalent);
        //            OnPropertyChanged(nameof(WritingTalent));
        //        }
        //    }

        //    public bool LanguageTalentBorderVisible
        //    {
        //        get
        //        {
        //            return languageTalentBorderVisible;
        //        }
        //        set
        //        {
        //            languageTalentBorderVisible = value;
        //            OnPropertyChanged(nameof(LanguageTalentBorderVisible));
        //        }
        //    }
        //    public int LanguageTAW
        //    {
        //        get => languageTaw;
        //        set 
        //        {
        //            languageTaw = value;
        //            var talent = LanguageTalent;
        //            var newTaW = value - Game.Charakter.Talente.GetModTaW(talent);
        //            Game.Charakter.Talente.SetTAW(talent, newTaW);
        //            OnPropertyChanged(nameof(LanguageTAW));
        //        }
        //    }
        //    public string LanguageProbe
        //    {
        //        get => languageProbeString;
        //        set
        //        {
        //            languageProbeString = value;
        //            OnPropertyChanged(nameof(LanguageProbe));
        //        }
        //    }
        //    public bool LanguageM
        //    {
        //        get => motherLanguage;
        //        set
        //        {
        //            motherLanguage = value;
        //            OnPropertyChanged(nameof(LanguageM));
        //        }
        //    }

        //    public bool WritingTalentBorderVisible
        //    {
        //        get
        //        {
        //            return writingTalentBorderVisible;
        //        }
        //        set
        //        {
        //            writingTalentBorderVisible = value;
        //            OnPropertyChanged(nameof(WritingTalentBorderVisible));
        //        }
        //    }
        //    public int WritingTAW
        //    {
        //        get => writingTaW;
        //        set
        //        {
        //            writingTaW = value;
        //            var talent = WritingTalent;
        //            var newTaW = value - Game.Charakter.Talente.GetModTaW(talent);
        //            Game.Charakter.Talente.SetTAW(talent, newTaW);
        //            OnPropertyChanged(nameof(WritingTAW));
        //        }
        //    }
        //    public string WritingProbe 
        //    {
        //        get => writingProbeString;
        //        set
        //        {
        //            writingProbeString = value;
        //            OnPropertyChanged(nameof(WritingProbe));
        //        }
        //    }
        //}
    }
}
