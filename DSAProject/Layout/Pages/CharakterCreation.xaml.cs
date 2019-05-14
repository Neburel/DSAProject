using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using static System.WindowsRuntimeSystemExtensions;

namespace DSAProject.Layout.Pages
{
    enum CreationOrder
    {
        playerName          = 1,
        PlayerHight         = 2,
        playerWight         = 3,
        age                 = 4,
        gender              = 5,
        familyStatus        = 7,
        playerAdressName    = 8,
        eyeColor            = 9,
        skinColor           = 10,
        hairColor           = 11,
        culture             = 12,
        profession          = 13,
        race                = 14,
        faith               = 15
        
    }
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CharakterCreation : Page
    {
        #region Variables
        private string DSA_CultureName = "Kultur-/en";

        private bool checkboxDisplayLayoutOnly;
        private string lastRadioButtonTag;
        #endregion
        #region Properties Seiten Einstellugen
        public int TextFontSize { get; } = 26;
        #endregion
        #region Properties Funktions
        private string SetCulture
        {
            set
            {
                if(XAML_CultureTitle != null)
                {
                    XAML_CultureTitle.Text = value;
                }
            }
        }
        private bool DisplayTech
        {
            set
            {
                if(XAML_TechMaxValue != null && XAML_StackPanelTechMax != null && XAML_TechMinValue != null && XAML_StackPanelTechMin != null)
                {
                    if (value)
                    {
                        XAML_TechMaxValue.Visibility = Visibility.Visible;
                        XAML_StackPanelTechMax.Visibility = Visibility.Visible;
                        XAML_TechMinValue.Visibility = Visibility.Visible;
                        XAML_StackPanelTechMin.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        XAML_TechMaxValue.Visibility = Visibility.Collapsed;
                        XAML_StackPanelTechMax.Visibility = Visibility.Collapsed;
                        XAML_TechMinValue.Visibility = Visibility.Collapsed;
                        XAML_StackPanelTechMin.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        #endregion
        public CharakterCreationViewModel ViewModel { get; set; } = new CharakterCreationViewModel();
        public CharakterCreation()
        {
            this.InitializeComponent();
            DataContext = this;

            #region SetDescriptiors
            ViewModel.Age.DescriptionTitle              = "Alter:";
            ViewModel.Race.DescriptionTitle             = "Rasse-/en:";
            ViewModel.Faith.DescriptionTitle            = "Glaube:";
            ViewModel.Gender.DescriptionTitle           = "Geschlecht:";
            ViewModel.Culture.DescriptionTitle          = DSA_CultureName;
            ViewModel.EyeColor.DescriptionTitle         = "Augenfarbe:";
            ViewModel.HairColor.DescriptionTitle        = "Haarfarbe:";
            ViewModel.SkinColor.DescriptionTitle        = "Hautfarbe:";
            ViewModel.BirthDate.DescriptionTitle        = "Geburtsdatum:";
            ViewModel.PlayerName.DescriptionTitle       = "Name:";
            ViewModel.Profession.DescriptionTitle       = "Profession:";
            ViewModel.CurrentDate.DescriptionTitle      = "Aktuelles Datum:";
            ViewModel.Familistatus.DescriptionTitle     = "Familienstatus:";
            ViewModel.PlayerHeight.DescriptionTitle     = "Größe:";
            ViewModel.PlayerWeight.DescriptionTitle     = "Gewicht:";
            ViewModel.PlayerAdressName.DescriptionTitle = "Anrede:";
            #endregion
            #region Priority
            ViewModel.Age.Priority              = (int)CreationOrder.age;
            ViewModel.Race.Priority             = (int)CreationOrder.race;
            ViewModel.Faith.Priority            = (int)CreationOrder.faith; 
            ViewModel.Gender.Priority           = (int)CreationOrder.gender; 
            ViewModel.Culture.Priority          = (int)CreationOrder.culture;
            ViewModel.EyeColor.Priority         = (int)CreationOrder.eyeColor;
            ViewModel.HairColor.Priority        = (int)CreationOrder.hairColor;
            ViewModel.SkinColor.Priority        = (int)CreationOrder.skinColor;
            ViewModel.PlayerName.Priority       = (int)CreationOrder.playerName;
            ViewModel.Profession.Priority       = (int)CreationOrder.profession;
            ViewModel.Familistatus.Priority     = (int)CreationOrder.familyStatus;
            ViewModel.PlayerHeight.Priority     = (int)CreationOrder.PlayerHight;
            ViewModel.PlayerWeight.Priority     = (int)CreationOrder.playerWight;
            ViewModel.PlayerAdressName.Priority = (int)CreationOrder.playerAdressName;
            #endregion
            #region ExampleDate
            ViewModel.Age.DescriptionText               = "25";
            ViewModel.Race.DescriptionText              = "Tiefling";
            ViewModel.Faith.DescriptionText             = "Firun";
            ViewModel.Gender.DescriptionText            = "♂";
            ViewModel.Culture.DescriptionText           = "Sonnenanbeter12345678910111213141516171819";
            ViewModel.EyeColor.DescriptionText          = "grün";
            ViewModel.HairColor.DescriptionText         = "schwarz";
            ViewModel.SkinColor.DescriptionText         = "hell";
            ViewModel.BirthDate.DescriptionText         = "12.05.1991";
            ViewModel.PlayerName.DescriptionText        = "Kazarik";
            ViewModel.Profession.DescriptionText        = "Jäger";
            ViewModel.Familistatus.DescriptionText      = "ledig";
            ViewModel.PlayerHeight.DescriptionText      = "168";
            ViewModel.PlayerWeight.DescriptionText      = "50";
            ViewModel.PlayerAdressName.DescriptionText  = "Kaza";

            #endregion



            CharakterChange();
            checkboxDisplayLayoutOnly = true;
            XAML_RadioButtonGameTypValueDSA.IsChecked = true;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            checkboxDisplayLayoutOnly = true;
            var ctype = Game.Charakter.GetType();
            if(ctype == typeof(CharakterDSA))
            {
                XAML_RadioButtonGameTypValueDSA.IsChecked = true;
            } 
            else if(ctype == typeof(CharakterPNP))
            {
                XAML_RadioButtonGameTypValuePNP.IsChecked = true;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------------------------------------------------------------");
            foreach (var item in Game.Charakter.CharakterDescriptions.Descriptions)
            {
                System.Diagnostics.Debug.WriteLine(item.DescriptionTitle + " " + item.DescriptionText);
            }
            System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
            System.Diagnostics.Debug.WriteLine(ViewModel.EyeColor.DescriptionTitle + " " + ViewModel.EyeColor.DescriptionText);
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Der Charakter muss vor dem Aufruf angepasst worden sein
        /// </summary>
        /// <param name="charakter"></param>
        private void CharakterChange()
        {
            var existingDescriptorList = Game.Charakter.CharakterDescriptions.Descriptions;
                       
            ViewModel.Age               = CheckDescriptor(existingDescriptorList, ViewModel.Age);
            ViewModel.Race              = CheckDescriptor(existingDescriptorList, ViewModel.Race);
            ViewModel.Faith             = CheckDescriptor(existingDescriptorList, ViewModel.Faith);
            ViewModel.Gender            = CheckDescriptor(existingDescriptorList, ViewModel.Gender);
            ViewModel.Culture           = CheckDescriptor(existingDescriptorList, ViewModel.Culture);
            ViewModel.EyeColor          = CheckDescriptor(existingDescriptorList, ViewModel.EyeColor);
            ViewModel.HairColor         = CheckDescriptor(existingDescriptorList, ViewModel.HairColor);
            ViewModel.SkinColor         = CheckDescriptor(existingDescriptorList, ViewModel.SkinColor);
            ViewModel.BirthDate         = CheckDescriptor(existingDescriptorList, ViewModel.BirthDate);
            ViewModel.PlayerName        = CheckDescriptor(existingDescriptorList, ViewModel.PlayerName);
            ViewModel.Profession        = CheckDescriptor(existingDescriptorList, ViewModel.Profession);
            ViewModel.Familistatus      = CheckDescriptor(existingDescriptorList, ViewModel.Familistatus);
            ViewModel.PlayerHeight      = CheckDescriptor(existingDescriptorList, ViewModel.PlayerHeight);
            ViewModel.PlayerWeight      = CheckDescriptor(existingDescriptorList, ViewModel.PlayerWeight);
            ViewModel.PlayerAdressName  = CheckDescriptor(existingDescriptorList, ViewModel.PlayerAdressName);
        }
        private Descriptor CheckDescriptor(List<Descriptor> charakterDescriptors, Descriptor siteDescriptor)
        {
            var ret = siteDescriptor;
            var items = charakterDescriptors.Where(x => x.DescriptionTitle == siteDescriptor.DescriptionTitle).ToList();
            if (items.Count > 0)
            {
                ret = items[0];
            }
            else
            {
                var charakter = Game.Charakter;
                System.Console.WriteLine(Game.Charakter);

                charakter.CharakterDescriptions.AddDescripton(siteDescriptor);
                ret = siteDescriptor;
            }
            return ret;
        }
        private async void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var createdNewChar = false;

            if (sender is RadioButton rb)
            {
                string gameTypeName = rb.Tag.ToString();
                switch (gameTypeName)
                {
                    case "DSA":
                        if (!checkboxDisplayLayoutOnly)
                        {
                            if (await DisplayWarningDialog())
                            {
                                Game.Charakter = new CharakterDSA();
                                createdNewChar = true;
                            }
                        }
                        if(createdNewChar || checkboxDisplayLayoutOnly)
                        {
                            #region setDSALayout
                            ViewModel.CurrentDate.DescriptionText   = Game.CurrentYearDSA;
                            ViewModel.Culture.DescriptionTitle      = DSA_CultureName;
                            DisplayTech = false;
                            #endregion
                        }
                        break;
                    case "PNP":
                        if (!checkboxDisplayLayoutOnly)
                        {
                            if (await DisplayWarningDialog())
                            {
                                Game.Charakter = new CharakterPNP();
                                createdNewChar = true;
                            }
                        }
                        if(createdNewChar || checkboxDisplayLayoutOnly)
                        {
                            #region setDSALayout
                            ViewModel.CurrentDate.DescriptionText = Game.CurrentYearPNP;
                            ViewModel.Culture.DescriptionTitle = "Vorgeschichte-/en:";
                            DisplayTech = true;
                            #endregion
                        }
                        break;
                }
                if (createdNewChar)
                {
                    lastRadioButtonTag = gameTypeName;
                    CharakterChange();
                } 
                else if(checkboxDisplayLayoutOnly) 
                {
                    checkboxDisplayLayoutOnly = false;
                    lastRadioButtonTag = gameTypeName;
                } 
                else
                {
                    checkboxDisplayLayoutOnly = true;
                    if (lastRadioButtonTag == "DSA")
                    {
                        XAML_RadioButtonGameTypValueDSA.IsChecked = true;
                    } 
                    else if (lastRadioButtonTag == "PNP")
                    {
                        XAML_RadioButtonGameTypValuePNP.IsChecked = true;
                    }
                }
            }
        }
        #region Buttons
        private void XAML_AirSpeedMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AirSpeed = ViewModel.AirSpeed - 1;
        }
        private void XAML_AirSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AirSpeed = ViewModel.AirSpeed + 1;
        }
        private void XAML_GroundWaterMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WaterSpeed = ViewModel.WaterSpeed - 1;
        }
        private void XAML_WaterSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WaterSpeed = ViewModel.WaterSpeed + 1;
        }
        private void XAML_GroundSpeedMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GroundSpeed = ViewModel.GroundSpeed - 1;
        }
        private void XAML_GroundSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GroundSpeed = ViewModel.GroundSpeed + 1;
          
        }
        private void XAML_GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Gender.DescriptionText = XAML_GenderComboBox.SelectedValue.ToString();
        }
        private void XAML_FamilyStatuscomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Familistatus.DescriptionText = XAML_FamilyStatuscomboBox.SelectedValue.ToString();
        }
        private void XAML_TechMinValuePlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TechstufeMin = ViewModel.TechstufeMin + 1;
        }
        private void XAML_TechMinValueMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TechstufeMin = ViewModel.TechstufeMin - 1;
        }
        private void XAML_TechMaxValuePlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TechstufeMax = ViewModel.TechstufeMax + 1;
        }
        private void XAML_TechMaxValueMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TechstufeMax = ViewModel.TechstufeMax - 1;
        }
        #endregion
        #region Dialog
        private async Task<bool> DisplayWarningDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Charakter erstellung",
                Content = "Durch unterschiede zwischen DSA und PNP wird durch diese Aktion ein neuer Charakter erstellt und die bisherigen eigegeben Informationen auserhalb dieser Seite erstellt. Wenn Jemand ein Funktion schreiben möchte die die Funktionen außerhalb dieser seite Übernehmen kann möge er sich melden",
                PrimaryButtonText = "Ok",
                CloseButtonText = "Abbrechen"
            };

            var result = await noWifiDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        #endregion
    }
}
