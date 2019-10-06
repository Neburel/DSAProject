using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.ViewModels;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ItemPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Talent_ItemPage : Page
    {
        private Talent_ItemPageViewModel ViewModel { get; set; } = new Talent_ItemPageViewModel();
        public ITalent Talent { get; private set; }

        public Talent_ItemPage(ITalent talent)
        {
            this.InitializeComponent();

            Talent                      = talent;
            ViewModel.TalentName        = talent.ToString();
            ViewModel.BE                = talent.BE;
            ViewModel.TAW               = Game.Charakter.Talente.GetTAW(talent).ToString();
            ViewModel.Probe             = Game.Charakter.Talente.GetProbeString(Talent);

            ViewModel.DeductionStringTalent = string.Empty;

            var freeText = string.Empty;
            var talentText = string.Empty;


            foreach(var deduction in talent.Deductions)
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
                ViewModel.PA = Game.Charakter.Talente.GetPA((AbstractTalentFighting)talent).ToString();
                ViewModel.AT = Game.Charakter.Talente.GetAT((AbstractTalentFighting)talent).ToString();
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

            Game.Charakter.Talente.TaWChanged += (sender, args) =>
            {
                ViewModel.TAW = Game.Charakter.Talente.GetTAW(Talent).ToString();
            };
        }
        private void XAML_PlusTAWButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTAW = Game.Charakter.Talente.GetTAW(Talent);
            Game.Charakter.Talente.SetTAW(Talent, currentTAW + 1);
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
        }
        private void XAML_MinusTAWButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTAW = Game.Charakter.Talente.GetTAW(Talent);
            Game.Charakter.Talente.SetTAW(Talent, currentTAW - 1);
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
        }
        private void XAML_MinusPAButton_Click(object sender, RoutedEventArgs e)
        {
            var talent = (AbstractTalentFighting)Talent;
            var currentPA = Game.Charakter.Talente.GetPA(talent);
            Game.Charakter.Talente.SetPA(talent, currentPA - 1);
            ViewModel.PA = Game.Charakter.Talente.GetPA(talent).ToString();
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
        }
        private void XAML_PlusPAButton_Click(object sender, RoutedEventArgs e)
        {
            var talent = (AbstractTalentFighting)Talent;
            var currentPA = Game.Charakter.Talente.GetPA(talent);
            Game.Charakter.Talente.SetPA(talent, currentPA + 1);
            ViewModel.PA = Game.Charakter.Talente.GetPA(talent).ToString();
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
        }
        private void XAML_MinusATButton_Click(object sender, RoutedEventArgs e)
        {
            var talent = (AbstractTalentFighting)Talent;
            var currentAT = Game.Charakter.Talente.GetAT(talent);
            Game.Charakter.Talente.SetAT(talent, currentAT - 1);
            ViewModel.AT = Game.Charakter.Talente.GetAT(talent).ToString();
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
        }
        private void XAML_PlusATButton_Click(object sender, RoutedEventArgs e)
        {
            var talent = (AbstractTalentFighting)Talent;
            var currentAT = Game.Charakter.Talente.GetAT(talent);
            Game.Charakter.Talente.SetAT(talent, currentAT + 1);
            ViewModel.AT = Game.Charakter.Talente.GetAT(talent).ToString();
            ViewModel.Probe = Game.Charakter.Talente.GetProbeString(Talent);
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
