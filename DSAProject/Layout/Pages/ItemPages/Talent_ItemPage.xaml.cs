using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ItemPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Talent_ItemPage : Page
    {
        private int bonusTAW;
        private int bonusAT;
        private int bonusPA;

        private Talent_ItemPageViewModel ViewModel { get; set; } = new Talent_ItemPageViewModel();
        public ITalent Talent { get; private set; }

        public Talent_ItemPage(ITalent talent)
        {
            this.InitializeComponent();

            Talent                      = talent;
            ViewModel.TalentName        = talent.ToString();
            ViewModel.BE                = talent.BE;
            
            bonusTAW    = Game.Charakter.Traits.GetTawBonus(talent);
            SetValue(TraitTalentBonusSelectionPage_Mode.All, 0);

            ViewModel.DeductionStringTalent = string.Empty;

            var freeText = string.Empty;
            var talentText = string.Empty;

            foreach (var deduction in talent.Deductions)
            {
                if (typeof(TalentDeductionFreeText).IsAssignableFrom(deduction.GetType()))
                {
                    freeText = GetString(deduction.GetDeductionString(), freeText);                    
                }
                else if (typeof(TalentDeductionTalent).IsAssignableFrom(deduction.GetType()))
                {
                    talentText = GetString(deduction.GetDeductionString(), talentText, string.Empty);
                }
            }
            if (!string.IsNullOrEmpty(talentText))
            {
                freeText = GetString(string.Empty, freeText, talentText);
            }
            ViewModel.DeductionStringFreeText = freeText;
            ViewModel.DeductionStringTalent = talentText;

            if (typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
            {
                bonusAT = Game.Charakter.Traits.GetATBonus((AbstractTalentFighting)talent);
                bonusPA = Game.Charakter.Traits.GetPABonus((AbstractTalentFighting)talent);

                SetValue(TraitTalentBonusSelectionPage_Mode.PA, 0);
                SetValue(TraitTalentBonusSelectionPage_Mode.AT, 0);

                ViewModel.IsATPAVisibility = Visibility.Visible;
            }
            if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent.GetType()))
            {
                var atg = (AbstractTalentGeneral)talent;
                ViewModel.ProbeText = atg.GetProbeText();

                freeText = string.Empty;
                var restString = string.Empty;

                foreach (var item in atg.Requirements.Where(x => x.GetType() == typeof(TalentRequirementFreeText)))
                {
                    freeText = GetString(item.RequirementString(), freeText);
                }
                foreach (var item in atg.Requirements.Where(x => x.GetType() != typeof(TalentRequirementFreeText)))
                {
                    restString = GetString(item.RequirementString(), restString, freeText);
                }
                ViewModel.RequirementStringFreeText = freeText;
                ViewModel.RequirementStringRest = restString;


                ViewModel.IsProbeTextVisibility = Visibility.Visible;
            }

            //Sollte nicht benötigt werden, da kein Wechsel des Talent Wertes hier stattfindet
            //Game.Charakter.Talente.TaWChanged += (sender, args) =>
            //{
            //    ViewModel.TAW = Game.Charakter.Talente.GetTAW(Talent).ToString();
            //};
        }
        private void XAML_PlusTAWButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.All, 1);
        }
        private void XAML_MinusTAWButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.All, -1);
        }
        private void XAML_MinusPAButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.PA, -1);
        }
        private void XAML_PlusPAButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.PA, 1);
        }
        private void XAML_MinusATButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.AT, -1);
        }
        private void XAML_PlusATButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(TraitTalentBonusSelectionPage_Mode.AT, 1);
        }

        public void SetValue(TraitTalentBonusSelectionPage_Mode mode, int addValue)
        {
            var value = 0;
            var bonusValue = 0;
            TextBlock view = null ;

            if (mode == TraitTalentBonusSelectionPage_Mode.All)
            {
                value = Game.Charakter.Talente.GetTAW(Talent);
                bonusValue = bonusTAW;

                Game.Charakter.Talente.SetTAW(Talent, value + addValue);

                ViewModel.TAW = (Game.Charakter.Talente.GetTAW(Talent) + bonusTAW).ToString();
                ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent, bonusTAW);

                view = XAML_TaW;
            }
            else if(mode == TraitTalentBonusSelectionPage_Mode.AT)
            {
                var talent = (AbstractTalentFighting)Talent;

                value = Game.Charakter.Talente.GetAT(talent);
                bonusValue = bonusAT;

                Game.Charakter.Talente.SetAT(talent, value + addValue);
                ViewModel.AT = (Game.Charakter.Talente.GetAT(talent) + bonusAT).ToString();
                ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent, bonusTAW, bonusAT, bonusPA);

                view = XAML_AT;
            }
            else if(mode == TraitTalentBonusSelectionPage_Mode.PA)
            {
                var talent = (AbstractTalentFighting)Talent;

                value = Game.Charakter.Talente.GetPA(talent);
                bonusValue = bonusPA;

                Game.Charakter.Talente.SetPA(talent, value + addValue);
                ViewModel.PA = (Game.Charakter.Talente.GetPA(talent) + bonusPA).ToString();
                ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent, bonusTAW, bonusAT, bonusPA);

                view = XAML_PA;
            }

            SetTooltip(view, value, bonusValue);
        }
        public void SetTooltip(TextBlock view, int value, int bonusValue)
        {
            string toolTipText = value.ToString();
            if(bonusValue != 0)
            {
                toolTipText = toolTipText + "(" + bonusValue.ToString() + ")";
            }

            ToolTip toolTip = new ToolTip();
            toolTip.Content = toolTipText;
            ToolTipService.SetToolTip(view, toolTip);
        }


        private string GetString(string newValue, string currentText, string secondControll = null)
        {
            if ((currentText == string.Empty || currentText == null) && (secondControll == string.Empty || secondControll == null))
            {
                return newValue;
            }
            else if(string.IsNullOrEmpty(currentText) && string.IsNullOrEmpty(newValue))
            {
                return newValue;
            }
            else
            {
                return currentText.Trim() + ", " + newValue.Trim();
            }
        }
    }
}
