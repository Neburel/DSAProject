using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
using DSAProject.util;


namespace DSAProject.Layout.ViewModels
{
    public class LanguageViewModel : AbstractPropertyChanged
    {
        public bool LanguageM
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool WritingTalentBorderVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool LanguageTalentBorderVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
        public int WritingTAW
        {
            get => Get<int>();
            set => Set(value);
        }
        public int LanguageTAW
        {
            get => Get<int>();
            set => Set(value);
        }
        public string WritingProbe
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LanguageProbe
        {
            get => Get<string>();
            set => Set(value);
        }
        public TalentSpeaking LanguageTalent
        {
            get => Get<TalentSpeaking>();
            set => Set(value);
        }
        public TalentWriting WritingTalent
        {
            get => Get<TalentWriting>();
            set => Set(value);
        }
        public LanguageViewModel()
        {
            this.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == nameof(LanguageTalent))
                {
                    LanguageTalentBorderVisible = true;
                    LanguageTAW                 = Game.Charakter.Talente.GetMaxTaw(LanguageTalent);
                    LanguageProbe               = Game.Charakter.Talente.GetProbeString(LanguageTalent);
                    LanguageM                   = Game.Charakter.Talente.GetMother(LanguageTalent);
                }
                else if(args.PropertyName == nameof(WritingTalent))
                {
                    WritingTalentBorderVisible  = true;
                    WritingTAW                  = Game.Charakter.Talente.GetMaxTaw(WritingTalent);
                    WritingProbe                = Game.Charakter.Talente.GetProbeString(WritingTalent);
                }
                else if(args.PropertyName == nameof(LanguageTAW))
                {
                    SetTaw(LanguageTalent, LanguageTAW);
                }
                else if(args.PropertyName == nameof(WritingTAW))
                {
                    SetTaw(WritingTalent, WritingTAW);
                }
            };
        }
        private void SetTaw(AbstractTalentLanguage talent, int setValue)
        {
            var newTaW = setValue - Game.Charakter.Talente.GetModTaW(talent);
            Game.Charakter.Talente.SetTAW(talent, newTaW);
        }
    }
}
