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
        public bool LanguageVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool FamilyNameVisible
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
        public string DiceResult
        {
            get => Get<string>();
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
        public string WritingTalentBE
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LanguageTalentBE
        {
            get => Get<string>();
            set => Set(value);
        }
        public string WritingTalentName
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LanguageTalentName
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LanguageFamilyName
        {
            get => Get<string>();
            set => Set(value);
        }
        public TalentSpeaking SpeakingTalent
        {
            get => Get<TalentSpeaking>();
            set => Set(value);
        }
        public TalentWriting WritingTalent
        {
            get => Get<TalentWriting>();
            set => Set(value);
        }
        public LanguageViewModel(DiceChanger helper = null)
        {
            FamilyNameVisible = true;

            if (helper != null)
            {
                helper.PropertyChanged += (sender, args) =>
                {
                    var value = helper.DiceResult;

                    var speakingValue = Game.Charakter.Talente.GetProbeValue(SpeakingTalent) - value;
                    var writingVlaue = Game.Charakter.Talente.GetProbeValue(WritingTalent) - value;

                    if(SpeakingTalent != null && WritingTalent != null)
                    {
                        DiceResult = speakingValue.ToString() + "/" + writingVlaue.ToString();
                    }
                    else if (SpeakingTalent == null && WritingTalent != null)
                    {
                        DiceResult = writingVlaue.ToString();
                    }
                    else if (SpeakingTalent != null && WritingTalent == null)
                    {
                        DiceResult = speakingValue.ToString();
                    }
                };
            }
            this.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == nameof(SpeakingTalent))
                {
                    FamilyNameVisible           = false;
                    LanguageTalentBorderVisible = true;
                    LanguageTalentBE            = SpeakingTalent.BE;
                    LanguageTalentName          = SpeakingTalent.Name;
                    LanguageTAW                 = Game.Charakter.Talente.GetMaxTaw(SpeakingTalent);
                    LanguageProbe               = Game.Charakter.Talente.GetProbeString(SpeakingTalent);
                    LanguageM                   = Game.Charakter.Talente.GetMother(SpeakingTalent);
                }
                else if(args.PropertyName == nameof(WritingTalent))
                {
                    FamilyNameVisible           = false;
                    WritingTalentBorderVisible  = true;
                    WritingTalentBE             = WritingTalent.BE;
                    WritingTalentName           = WritingTalent.Name;
                    WritingTAW                  = Game.Charakter.Talente.GetMaxTaw(WritingTalent);
                    WritingProbe                = Game.Charakter.Talente.GetProbeString(WritingTalent);
                }
                else if(args.PropertyName == nameof(LanguageTAW))
                {
                    SetTaw(SpeakingTalent, LanguageTAW);
                }
                else if(args.PropertyName == nameof(WritingTAW))
                {
                    SetTaw(WritingTalent, WritingTAW);
                }
                else if(args.PropertyName == nameof(LanguageM))
                {
                    var speaking = Game.Charakter.Talente.GetMother(SpeakingTalent);
                    if(LanguageM != speaking)
                    {
                        Game.Charakter.Talente.SetMother(SpeakingTalent, LanguageM);
                    }
                }
                else if(args.PropertyName == nameof(FamilyNameVisible))
                {
                    LanguageVisible = !FamilyNameVisible;
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
