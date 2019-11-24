using DSALib;
using DSALib.Utils;
using DSAProject.Classes;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Charakter.Talente.TalentDeductions;
using DSAProject.Classes.Charakter.Talente.TalentFighting;
using DSAProject.Classes.Charakter.Talente.TalentGeneral;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentRequirement;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.ViewModels;
using DSAProject.util.ErrrorManagment;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.ToolPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CreateTalent : Page
    {
        #region Variables
        private int currentBE = 0;
        private string currentContent;
        private Brush borderColorStandart   = new SolidColorBrush(Windows.UI.Colors.LightGray);
        private Brush borderColorWarning    = new SolidColorBrush(Windows.UI.Colors.Red);
        private GameType gameType           = GameType.DSA;
        private ITalent selectedTalent      = null;
        #endregion
        #region Properties Seiten Einstellugen
        public int TextFontSize { get; } = 26;
        private string TalentType_DSA { get => "DSA"; }
        private string TalentType_PNP { get => "PNP"; }
        private string TalentChoice_Weaponless { get => nameof(TalentWeaponless); }
        private string TalentChoice_Close { get => nameof(TalentClose); }
        private string TalentChoice_Range { get => nameof(TalentRange); }
        private string TalentChoice_Physical { get => nameof(TalentPhysical); }
        private string TalentChoice_Social { get => nameof(TalentSocial); }
        private string TalentChoice_Nature { get => nameof(TalentNature); }
        private string TalentChoice_Knwoldage { get => nameof(TalentKnowldage); }
        private string TalentChoice_Crafting { get => nameof(TalentCrafting); }
        private string TalentChoice_Language { get => nameof(TalentLanguage); }
        #endregion
        private CreateTalentViewModel ViewModel = new CreateTalentViewModel();
        public CreateTalent()
        {
            this.InitializeComponent();
            //wir Starten mit DSA

            //Davon ausgehen das Weaponless gechekt ist
            ViewModel.DeductionValue = 5;
            ViewModel.IsProbeSelectionVisibile = Visibility.Collapsed;
            ViewModel.IsRequirementSelectionVisible = Visibility.Collapsed;

            ViewModel.Talents = new ObservableCollection<ITalent>(Game.TalenteDSA.OrderBy(x => x.ToString()).ToList());
        }
        private void XAML_ComboBoxEditTalent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAll(true);

            var item = (ITalent)XAML_ComboBoxEditTalent.SelectedItem;
            if (item != null)
            {
                #region SetTalentType
                if (item.GetType() == typeof(TalentWeaponless))
                {
                    XAML_RadioButton_Weaponless.IsChecked = true;
                }
                else if (item.GetType() == typeof(TalentClose))
                {
                    XAML_RadioButton_Weapon.IsChecked = true;
                }
                else if (item.GetType() == typeof(TalentRange))
                {
                    XAML_RadioButton_Range.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentCrafting))
                {
                    XAML_RadioButton_Crafting.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentKnowldage))
                {
                    XAML_RadioButton_Knowldage.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentNature))
                {
                    XAML_RadioButton_Nature.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentPhysical))
                {
                    XAML_RadioButton_Pysical.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentSocial))
                {
                    XAML_RadioButton_Sozial.IsChecked = true;
                } 
                else if (item.GetType() == typeof(TalentLanguage))
                {
                    XAML_RadioButton_Language.IsChecked = true;
                } 
                #endregion

                selectedTalent                  = item;
                ViewModel.ID                    = item.ID;                                    
                ViewModel.BEString              = item.BE;
                ViewModel.TalentName            = item.Name;
                ViewModel.TalentNameExtension   = item.NameExtension;

                #region Deduction
                foreach(var deduction in item.Deductions)
                {
                    ViewModel.Deductions.Add(deduction);
                    ViewModel.DeductionString = GetString(deduction.GetDeductionString(), ViewModel.DeductionString);
                }
                #endregion

                if (typeof(AbstractTalentGeneral).IsAssignableFrom(item.GetType()))
                {
                    var talent = (AbstractTalentGeneral)item;

                    ViewModel.Probes        = new ObservableCollection<CharakterAttribut>(talent.Attributs);
                    ViewModel.ProbeString   = talent.GetProbeText();


                    foreach(var reqITem in talent.Requirements)
                    {
                        ViewModel.Req.Add(reqITem);
                        if(ViewModel.RequirementString == string.Empty)
                        {
                            ViewModel.RequirementString = reqITem.RequirementString();
                        }
                        else
                        {
                            ViewModel.RequirementString = ViewModel.RequirementString + ", " + reqITem.RequirementString();
                        }
                    }
                }
            } 
            else 
            {
                ClearAll();
            }
        }
        private void XAML_RadioButtonGameTypValueDSA_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Talents = new ObservableCollection<ITalent>(Game.TalenteDSA.OrderBy(x => x.Name).ToList());
            gameType = GameType.DSA;

            ViewModel.Talents = new ObservableCollection<ITalent>(Game.TalenteDSA);
            ViewModel.FatherTalents = new ObservableCollection<ITalent>(Game.TalenteDSA.Where(x => (typeof(AbstractTalentGeneral).IsAssignableFrom(x.GetType()))).ToList());
        }
        private void XAML_RadioButtonGameTypValuePNP_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Talents = new ObservableCollection<ITalent>(Game.TalentePNP.OrderBy(x => x.Name).ToList());
            gameType = GameType.PNP;

            ViewModel.Talents = new ObservableCollection<ITalent>(Game.TalentePNP);
            ViewModel.FatherTalents = new ObservableCollection<ITalent>(Game.TalentePNP.Where(x => (typeof(AbstractTalentGeneral).IsAssignableFrom(x.GetType()))).ToList());
        }
        private void RadioButton_TalentChoice_Checked(object sender, RoutedEventArgs e)
        {
            ClearAll();

            if (sender is RadioButton button)
            {
                if (button.Content != null)
                {
                    string content = button.Content.ToString();
                    currentContent = content;

                    if (content == TalentChoice_Weaponless || content == TalentChoice_Close || content == TalentChoice_Range)
                    {
                        ViewModel.DeductionValue                    = 5;
                        ViewModel.IsProbeSelectionVisibile          = Visibility.Collapsed;
                        ViewModel.IsRequirementSelectionVisible     = Visibility.Collapsed;
                        ViewModel.IsFatherTalentsVisible            = Visibility.Collapsed;
                    }
                    else if(content == TalentChoice_Physical || content == TalentChoice_Social || content == TalentChoice_Nature || content == TalentChoice_Knwoldage || content == TalentChoice_Crafting)
                    {
                        ViewModel.DeductionValue                = 10;
                        ViewModel.IsProbeSelectionVisibile      = Visibility.Visible;
                        ViewModel.IsRequirementSelectionVisible = Visibility.Visible;
                        ViewModel.IsFatherTalentsVisible        = Visibility.Visible;
                    }   
                    else
                    {
                        content = String.Empty;
                    }
                }
            }
        }
        private void ClearAll(bool ignoreTalentEditSelection = false)
        {
            selectedTalent                  = null;

            ViewModel.ID                    = Guid.Empty;
            ViewModel.BEString              = "";
            ViewModel.TalentName            = "";
            ViewModel.TalentNameExtension   = "";

            if (!ignoreTalentEditSelection)
            {
                XAML_ComboBoxEditTalent.SelectedValue = null;
            }

            XAML_ComboBoxRequirementTalent.SelectedValue    = null;
            XAML_ComboBoxFatherTalent.SelectedValue         = null;


            ViewModel.ProbeString = string.Empty;
            ViewModel.Probes.Clear();
            ViewModel.Req.Clear();

            ViewModel.Req.Clear();
            ViewModel.RequirementString = string.Empty;

            ViewModel.Deductions.Clear();
            ViewModel.DeductionString = string.Empty;
        }
        private void ClearBorder()
        {
            ViewModel.TalentTypeBorderColor = borderColorStandart;
            ViewModel.TalentNameBorderColor = borderColorStandart;
        }
        #region BE
        private void XAML_ButtonBEPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BEString = CreateBeString(true);
        }
        private void XAML_ButtonBEMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BEString = CreateBeString(false);
        }
        private string CreateBeString(bool plus)
        {
            /*
             * Werte
             * ...
             * BE-2
             * BE-1
             * -
             * 
             * BE
             * BEx2
             * BEx3
             * BEx4
             * ...
             */

            var retString = string.Empty;
            currentBE = plus ? currentBE + 1 : currentBE - 1; ;

            if (currentBE == 0)
            {
                retString = "";
            } else if (currentBE == 1)
            {
                retString = "BE";
            } else if (currentBE >= 2)
            {
                retString = "BEx" + currentBE.ToString();
            } else if (currentBE == -1)
            {
                retString = "-";
            } else if (currentBE <= -2)
            {
                retString = "BE" + (currentBE + 1).ToString();
            }
            return retString;
        }
        #endregion
        #region Deduction
        private void XAML_ButonDeductionMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeductionValue = ViewModel.DeductionValue - 1;
        }
        private void XAML_ButonDeductionPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeductionValue = ViewModel.DeductionValue + 1;
        }
        private void XAML_ButtonAddDeduction_Click(object sender, RoutedEventArgs e)
        {
            var talent          = (ITalent) XAML_ComboBoxDeductionChoice.SelectedValue;
            var currentValue    = ViewModel.DeductionValue;

            var newDeduction = new TalentDeductionTalent(talent, currentValue, talent.BaseDeduction);
            ViewModel.Deductions.Add(newDeduction);
            ViewModel.DeductionString = GetString(newDeduction.GetDeductionString(), ViewModel.DeductionString);
        }
        private void XAML_ButtonRemoveDeduction_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Deductions.Clear();
            ViewModel.DeductionString = string.Empty;
        }
        private void XAML_ButtonDeductioNFreeText_Click(object sender, RoutedEventArgs e)
        {
            var newDeduction = new TalentDeductionFreeText(ViewModel.DeductionFreeText);
            ViewModel.Deductions.Add(newDeduction);
            ViewModel.DeductionString = GetString(newDeduction.GetDeductionString(), ViewModel.DeductionString);
        }
        #endregion
        #region Create
        private void XAML_ButtonCreateTalent_Click(object sender, RoutedEventArgs e)
        {
            ITalent currentTalent = selectedTalent;
            Error error = null;

            if (selectedTalent == null)
            {
                if (!TalentHelper.IsTalentTypeAvaivible(currentContent))
                {
                    ViewModel.TalentTypeBorderColor = borderColorWarning;
                    error = new Error
                    {
                        ErrorCode = ErrorCode.InvalidValue,
                        Message = "Es muss ein gültiger Talent Typ ausgewählt"
                    };
                }
                else
                {
                    currentTalent = TalentHelper.CreateTalent(
                        contentType: currentContent,
                        guid:           new Guid(),
                        probe:          ViewModel.Probes.ToList(),
                        be:             ViewModel.BEString.Trim(),
                        name:           ViewModel.TalentName.Trim(),
                        nameExtension:  ViewModel.TalentNameExtension.Trim());
                }
            }
            currentTalent = TalentHelper.EditTalent(
                talent: currentTalent,
                deductions: ViewModel.Deductions.ToList(),
                requirements: ViewModel.Req.ToList(),
                fatherTalent: (AbstractTalentGeneral)XAML_ComboBoxFatherTalent.SelectedValue);

            #region Save
            if (ViewModel.TalentName != null && ViewModel.TalentName != string.Empty)
            {
                Game.SaveTalent(currentTalent, gameType, out error);
            }
            else
            {
                ViewModel.TalentNameBorderColor = borderColorWarning;

                error = new Error
                {
                    ErrorCode = ErrorCode.InvalidValue,
                    Message = "Es wird ein Talent Name Benötigt"
                };
            }
            #endregion
            #region Dialog
            var dialog = new ContentDialog
            {
                Title = "Talent Erstellung Resultat",
                Content = "Das erstellen des Talentes war erfolgreich",
                CloseButtonText = "OK"
            };

            if (error != null)
            {
                dialog.Content = "Das erstellen des Talentes ist Fehlgeschlagen. Fehler: " + error.Message;
            } 
            else
            {
                ClearBorder();
                ClearAll();
            }
            #endregion
#pragma warning disable CS4014 // Da dieser Aufruf nicht abgewartet wird, wird die Ausführung der aktuellen Methode fortgesetzt, bevor der Aufruf abgeschlossen ist
            dialog.ShowAsync();
#pragma warning restore CS4014 // Da dieser Aufruf nicht abgewartet wird, wird die Ausführung der aktuellen Methode fortgesetzt, bevor der Aufruf abgeschlossen ist
        }
        #endregion
        #region Probe
        private void XAML_ProbeMut_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Mut);
        }
        private void XAML_ProbeKlugheit_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Klugheit);
        }
        private void XAML_ProbeIntuition_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Intuition);
        }
        private void XAML_ProbeCharisma_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Charisma);
        }
        private void XAML_ProbeFingerfertigkeit_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Fingerfertigkeit);
        }
        private void XAML_ProbeGewandtheit_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Gewandheit);
        }
        private void XAML_ProbeKonstitution_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Konstitution);
        }
        private void XAML_ProbeKörperkraft_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Körperkraft);
        }
        private void XAML_ProbeSozialstatus_Click(object sender, RoutedEventArgs e)
        {
            AddProbe(CharakterAttribut.Sozialstatus);
        }
        private void XAML_ProbeClear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ProbeString = string.Empty;
            ViewModel.Probes.Clear();
        }
        private void AddProbe(CharakterAttribut attribut)
        {
            ViewModel.Probes.Add(attribut);
            if(ViewModel.ProbeString == string.Empty)
            {
                ViewModel.ProbeString = Helper.GetShort(attribut);
            } 
            else
            {
                ViewModel.ProbeString = ViewModel.ProbeString + "/" + Helper.GetShort(attribut);
            }
        }
        #endregion
        #region Requirements
        private void XAML_RequirementMut_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Mut);
        }
        private void XAML_RequirementKlugheit_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Klugheit);
        }
        private void XAML_RequirementIntuition_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Intuition);
        }
        private void XAML_RequirementCharisma_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Charisma);
        }
        private void XAML_RequirementFingerfertigkeit_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Fingerfertigkeit);
        }
        private void XAML_RequirementGewandtheit_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Gewandheit);
        }
        private void XAML_RequirementKonstitution_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Konstitution);
        }
        private void XAML_RequirementKörperkraft_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Körperkraft);
        }
        private void XAML_RequirementSozialstatus_Click(object sender, RoutedEventArgs e)
        {
            AddReq(CharakterAttribut.Sozialstatus);
        }
        private void XAML_ButtonRequirementPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequirementOffValue = ViewModel.RequirementOffValue + 1;
        }
        private void XAML_ButtonRequirementMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequirementOffValue = ViewModel.RequirementOffValue - 1;
        }
        private void XAML_ButtonRequirementVonPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequirementNeedValue = ViewModel.RequirementNeedValue + 1;
        }
        private void XAML_ButtonRequirementVonMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequirementNeedValue = ViewModel.RequirementNeedValue - 1;
        }
        private void XAML_ButtonRequirementTalentAdd_Click(object sender, RoutedEventArgs e)
        {
            var talent      = (ITalent) XAML_ComboBoxRequirementTalent.SelectedValue;
            var offValue    = ViewModel.RequirementOffValue;
            var needValue   = ViewModel.RequirementNeedValue;

            var requirement = new TalentRequirementTalent(talent, needValue, offValue);
            ViewModel.Req.Add(requirement);

            if(ViewModel.RequirementString == string.Empty)
            {
                ViewModel.RequirementString = requirement.RequirementString();
            } 
            else
            {
                ViewModel.RequirementString = ViewModel.RequirementString + requirement.RequirementString();
            }

        }
        private void XAML_ButtonRequriementStringAdd_Click(object sender, RoutedEventArgs e)
        {
            var requirement = new TalentRequirementFreeText(XAML_ReqTextFreeText.Text);

            ViewModel.Req.Add(requirement);

            if (ViewModel.RequirementString == string.Empty)
            {
                ViewModel.RequirementString = requirement.RequirementString();
            }
            else
            {
                ViewModel.RequirementString = ViewModel.RequirementString + ", " + requirement.RequirementString(); 
            }
        }
        private void XAML_RequirementtributeClear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Req.Clear();
            ViewModel.RequirementString = string.Empty;
        }
        private void AddReq(CharakterAttribut attribut)
        {
        }

        #endregion
        #region Hilfsmethoden
        private string GetString(string newValue, string currentText, string secondControll = "")
        {
            if (currentText == string.Empty && secondControll == string.Empty)
            {
                return newValue;
            }
            else
            {
                return currentText + ", " + newValue;
            }
        }
        #endregion

        
    }
}
