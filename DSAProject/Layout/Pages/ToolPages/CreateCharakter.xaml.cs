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
    enum CreateNewChar
    {
        DSA = 1,
        PNP = 2,
        Abbruch = 3
    }
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CharakterCreation : Page
    {
        #region Variables
        private string DSA_CultureName = "Kultur-/en";
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
            ViewModel.PlayerName.PropertyChanged += (sender, args) =>
            {
                Game.Charakter.Name = ViewModel.PlayerName.DescriptionText;
            };
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
        }
        /// <summary>
        /// Der Charakter muss vor dem Aufruf angepasst worden sein
        /// </summary>
        /// <param name="charakter"></param>
        private void CharakterChange()
        {
            var existingDescriptorList = Game.Charakter.Descriptions.Descriptions;
                       
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

                charakter.Descriptions.AddDescripton(siteDescriptor);
                ret = siteDescriptor;
            }
            return ret;
        }
        #region Buttons
        private async void XAML_ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var result = await DisplayCreateNewDialog();
            if (result == CreateNewChar.DSA)
            {
                Game.Charakter = new CharakterDSA(Game.GenerateNextCharakterGUID());
                #region setDSALayout
                ViewModel.CurrentDate.DescriptionText = Game.CurrentYearDSA;
                ViewModel.Culture.DescriptionTitle = DSA_CultureName;
                DisplayTech = false;
                #endregion

            } else if (result == CreateNewChar.PNP)
            {
                Game.Charakter = new CharakterPNP(Game.GenerateNextCharakterGUID());
                #region setPNPLayout
                ViewModel.CurrentDate.DescriptionText = Game.CurrentYearPNP;
                ViewModel.Culture.DescriptionTitle = "Vorgeschichte-/en:";
                DisplayTech = true;
                #endregion
            }

            if(result != CreateNewChar.Abbruch)
            {
                #region Clear
                ViewModel.Age.DescriptionText = string.Empty;
                ViewModel.Race.DescriptionText = string.Empty;
                ViewModel.Faith.DescriptionText = string.Empty;
                ViewModel.Gender.DescriptionText = string.Empty;
                ViewModel.Culture.DescriptionText = string.Empty;
                ViewModel.EyeColor.DescriptionText = string.Empty;
                ViewModel.HairColor.DescriptionText = string.Empty;
                ViewModel.SkinColor.DescriptionText = string.Empty;
                ViewModel.BirthDate.DescriptionText = string.Empty;
                ViewModel.PlayerName.DescriptionText = string.Empty;
                ViewModel.Profession.DescriptionText = string.Empty;
                ViewModel.Familistatus.DescriptionText = string.Empty;
                ViewModel.PlayerHeight.DescriptionText = string.Empty;
                ViewModel.PlayerWeight.DescriptionText = string.Empty;
                ViewModel.PlayerAdressName.DescriptionText = string.Empty;
                #endregion
                CharakterChange();
            }


        }
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

        private async Task<CreateNewChar> DisplayCreateNewDialog()
        {
            ContentDialog newDialog = new ContentDialog
            {
                Title = "Charakter Erstellen",
                Content = "Sie sind nun Dabei einen neuen Charakter zu erstellen",
                CloseButtonText = "Abbrechen",
                PrimaryButtonText = "DSA",
                SecondaryButtonText = "PNP",

                FontFamily =  new Windows.UI.Xaml.Media.FontFamily("French Script MT"),
                FontSize = TextFontSize
            };
            var result = await newDialog.ShowAsync();
            CreateNewChar ret = CreateNewChar.Abbruch;

            if(result == ContentDialogResult.Primary)
            {
                ret = CreateNewChar.DSA;
            }
            else if(result == ContentDialogResult.Secondary)
            {
                ret = CreateNewChar.PNP;
            }

            return ret;
        }

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
