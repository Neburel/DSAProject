using DSALib.Charakter.Values.Settable;
using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Charakter.Values;
using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DSAProject.Layout.Pages
{
    enum CreationOrder
    {
        playerName = 1,
        PlayerHight = 2,
        playerWight = 3,
        age = 4,
        gender = 5,
        familyStatus = 7,
        playerAdressName = 8,
        eyeColor = 9,
        skinColor = 10,
        hairColor = 11,
        culture = 12,
        profession = 13,
        race = 14,
        faith = 15
    }
    enum Speed
    {
        GroundSpeed = 1,
        AirSpeed = 2,
        WaterSpeed = 3
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
        public CharakterCreationViewModel ViewModel { get; set; } = new CharakterCreationViewModel();
        public CharakterCreation()
        {
            this.InitializeComponent();
            DataContext = this;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var stringBirthDate = loader.GetString("Birthday");

            #region SetDescriptiors
            ViewModel.Age.DescriptionTitle = "Alter:";
            ViewModel.Race.DescriptionTitle = "Rasse-/en:";
            ViewModel.Faith.DescriptionTitle = "Glaube:";
            ViewModel.Gender.DescriptionTitle = "Geschlecht:";
            ViewModel.Culture.DescriptionTitle = DSA_CultureName;
            ViewModel.EyeColor.DescriptionTitle = "Augenfarbe:";
            ViewModel.HairColor.DescriptionTitle = "Haarfarbe:";
            ViewModel.SkinColor.DescriptionTitle = "Hautfarbe:";
            ViewModel.BirthDate.DescriptionTitle = stringBirthDate;
            ViewModel.PlayerName.DescriptionTitle = "Name:";
            ViewModel.Profession.DescriptionTitle = "Profession:";
            ViewModel.CurrentDate.DescriptionTitle = "Aktuelles Datum:";
            ViewModel.Familistatus.DescriptionTitle = "Familienstatus:";
            ViewModel.PlayerHeight.DescriptionTitle = "Größe:";
            ViewModel.PlayerWeight.DescriptionTitle = "Gewicht:";
            ViewModel.PlayerAdressName.DescriptionTitle = "Anrede:";
            #endregion
            #region Priority
            ViewModel.Age.Priority = (int)CreationOrder.age;
            ViewModel.Race.Priority = (int)CreationOrder.race;
            ViewModel.Faith.Priority = (int)CreationOrder.faith;
            ViewModel.Gender.Priority = (int)CreationOrder.gender;
            ViewModel.Culture.Priority = (int)CreationOrder.culture;
            ViewModel.EyeColor.Priority = (int)CreationOrder.eyeColor;
            ViewModel.HairColor.Priority = (int)CreationOrder.hairColor;
            ViewModel.SkinColor.Priority = (int)CreationOrder.skinColor;
            ViewModel.PlayerName.Priority = (int)CreationOrder.playerName;
            ViewModel.Profession.Priority = (int)CreationOrder.profession;
            ViewModel.Familistatus.Priority = (int)CreationOrder.familyStatus;
            ViewModel.PlayerHeight.Priority = (int)CreationOrder.PlayerHight;
            ViewModel.PlayerWeight.Priority = (int)CreationOrder.playerWight;
            ViewModel.PlayerAdressName.Priority = (int)CreationOrder.playerAdressName;
            #endregion
            SetChangeHandler();
            #region ExampleDate
            //ViewModel.Age.DescriptionText               = "25";
            //ViewModel.Race.DescriptionText              = "Tiefling";
            //ViewModel.Faith.DescriptionText             = "Firun";
            //ViewModel.Gender.DescriptionText            = "♂";
            //ViewModel.Culture.DescriptionText           = "Sonnenanbeter12345678910111213141516171819";
            //ViewModel.EyeColor.DescriptionText          = "grün";
            //ViewModel.HairColor.DescriptionText         = "schwarz";
            //ViewModel.SkinColor.DescriptionText         = "hell";
            //ViewModel.BirthDate.DescriptionText         = "12.05.1991";
            //ViewModel.PlayerName.DescriptionText        = "Kazarik";
            //ViewModel.Profession.DescriptionText        = "Jäger";
            //ViewModel.Familistatus.DescriptionText      = "ledig";
            //ViewModel.PlayerHeight.DescriptionText      = "168";
            //ViewModel.PlayerWeight.DescriptionText      = "50";
            //ViewModel.PlayerAdressName.DescriptionText  = "Kaza";
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

            ViewModel.Age = CheckDescriptor(existingDescriptorList, ViewModel.Age);
            ViewModel.Race = CheckDescriptor(existingDescriptorList, ViewModel.Race);
            ViewModel.Faith = CheckDescriptor(existingDescriptorList, ViewModel.Faith);
            ViewModel.Gender = CheckDescriptor(existingDescriptorList, ViewModel.Gender);
            ViewModel.Culture = CheckDescriptor(existingDescriptorList, ViewModel.Culture);
            ViewModel.EyeColor = CheckDescriptor(existingDescriptorList, ViewModel.EyeColor);
            ViewModel.HairColor = CheckDescriptor(existingDescriptorList, ViewModel.HairColor);
            ViewModel.SkinColor = CheckDescriptor(existingDescriptorList, ViewModel.SkinColor);
            ViewModel.BirthDate = CheckDescriptor(existingDescriptorList, ViewModel.BirthDate);
            ViewModel.PlayerName = CheckDescriptor(existingDescriptorList, ViewModel.PlayerName);
            ViewModel.Profession = CheckDescriptor(existingDescriptorList, ViewModel.Profession);
            ViewModel.Familistatus = CheckDescriptor(existingDescriptorList, ViewModel.Familistatus);
            ViewModel.PlayerHeight = CheckDescriptor(existingDescriptorList, ViewModel.PlayerHeight);
            ViewModel.PlayerWeight = CheckDescriptor(existingDescriptorList, ViewModel.PlayerWeight);
            ViewModel.PlayerAdressName = CheckDescriptor(existingDescriptorList, ViewModel.PlayerAdressName);

            SetChangeHandler();

            #region Speed
            var landSpeed   = Game.Charakter.Values.UsedValues.Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType())).Where(x => x.GetType() == typeof(SpeedLand)).FirstOrDefault();
            var airSpeed    = Game.Charakter.Values.UsedValues.Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType())).Where(x => x.GetType() == typeof(SpeedLand)).FirstOrDefault();
            var waterSpeed  = Game.Charakter.Values.UsedValues.Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType())).Where(x => x.GetType() == typeof(SpeedLand)).FirstOrDefault();

            if (landSpeed != null)
            {
                ViewModel.GroundSpeed = Game.Charakter.Values.GetAKTValue(landSpeed, out DSAError error);
            }
            if (airSpeed != null)
            {
                ViewModel.GroundSpeed = Game.Charakter.Values.GetAKTValue(airSpeed, out DSAError error);
            }
            if (waterSpeed != null)
            {
                ViewModel.GroundSpeed = Game.Charakter.Values.GetAKTValue(waterSpeed, out DSAError error);
            }
            #endregion

            Game.Charakter.Name = ViewModel.PlayerName.DescriptionText;
        }
        private void SetChangeHandler()
        {
            ViewModel.PlayerName.PropertyChanged += (sender, args) =>
            {
                Game.Charakter.Name = ViewModel.PlayerName.DescriptionText;
            };
        }
        private void ChangeSpeed(Speed speed, int value)
        {
            Type type = null;

            switch (speed)
            {
                case Speed.AirSpeed:
                    type = typeof(SpeedAir);
                    break;
                case Speed.WaterSpeed:
                    type = typeof(SpeedWater);
                    break;
                case Speed.GroundSpeed:
                    type = typeof(SpeedLand);
                    break;
            }
            
            var item = Game.Charakter.Values.UsedValues.Where(x => typeof(AbstractSettableValue).IsAssignableFrom(x.GetType())).Where(x => x.GetType() == type).FirstOrDefault();
            if(item != null)
            {
                Game.Charakter.Values.SetAKTValue((AbstractSettableValue)item, value);
            }

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
        private void XAML_ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            Game.Charakter = new CharakterDSA(Game.GenerateNextCharakterGUID());
            #region setDSALayout
            ViewModel.CurrentDate.DescriptionText = Game.CurrentYearDSA;
            ViewModel.Culture.DescriptionTitle = DSA_CultureName;
            #endregion
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
        private void XAML_AirSpeedMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AirSpeed = ViewModel.AirSpeed - 1;
            ChangeSpeed(Speed.AirSpeed, ViewModel.AirSpeed);
        }
        private void XAML_AirSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AirSpeed = ViewModel.AirSpeed + 1;
            ChangeSpeed(Speed.AirSpeed, ViewModel.AirSpeed);
        }
        private void XAML_GroundWaterMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WaterSpeed = ViewModel.WaterSpeed - 1;
            ChangeSpeed(Speed.WaterSpeed, ViewModel.WaterSpeed);
        }
        private void XAML_WaterSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WaterSpeed = ViewModel.WaterSpeed + 1;
            ChangeSpeed(Speed.WaterSpeed, ViewModel.WaterSpeed);
        }
        private void XAML_GroundSpeedMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GroundSpeed = ViewModel.GroundSpeed - 1;
            ChangeSpeed(Speed.GroundSpeed, ViewModel.GroundSpeed);
        }
        private void XAML_GroundSpeedPlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GroundSpeed = ViewModel.GroundSpeed + 1;
            ChangeSpeed(Speed.GroundSpeed, ViewModel.GroundSpeed);

        }
        private void XAML_GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Gender.DescriptionText = XAML_GenderComboBox.SelectedValue.ToString();
        }
        private void XAML_FamilyStatuscomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Familistatus.DescriptionText = XAML_FamilyStatuscomboBox.SelectedValue.ToString();
        }
        #endregion
        #region Dialog
        //private async Task<CreateNewChar> DisplayCreateNewDialog()
        //{
        //    ContentDialog newDialog = new ContentDialog
        //    {
        //        Title = "Charakter Erstellen",
        //        Content = "Sie sind nun Dabei einen neuen Charakter zu erstellen",
        //        CloseButtonText = "Abbrechen",
        //        PrimaryButtonText = "DSA",
        //        SecondaryButtonText = "PNP",

        //        FontFamily =  new Windows.UI.Xaml.Media.FontFamily("French Script MT"),
        //        FontSize = TextFontSize
        //    };
        //    var result = await newDialog.ShowAsync();
        //    CreateNewChar ret = CreateNewChar.Abbruch;

        //    if(result == ContentDialogResult.Primary)
        //    {
        //        ret = CreateNewChar.DSA;
        //    }
        //    else if(result == ContentDialogResult.Secondary)
        //    {
        //        ret = CreateNewChar.PNP;
        //    }

        //    return ret;
        //}
        //private async Task<bool> DisplayWarningDialog()
        //{
        //    ContentDialog noWifiDialog = new ContentDialog
        //    {
        //        Title = "Charakter erstellung",
        //        Content = "Durch unterschiede zwischen DSA und PNP wird durch diese Aktion ein neuer Charakter erstellt und die bisherigen eigegeben Informationen auserhalb dieser Seite erstellt. Wenn Jemand ein Funktion schreiben möchte die die Funktionen außerhalb dieser seite Übernehmen kann möge er sich melden",
        //        PrimaryButtonText = "Ok",
        //        CloseButtonText = "Abbrechen"
        //    };

        //    var result = await noWifiDialog.ShowAsync();

        //    if (result == ContentDialogResult.Primary)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }
}
