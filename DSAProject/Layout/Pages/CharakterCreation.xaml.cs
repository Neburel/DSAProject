using DSAProject.Classes.Game;
using DSAProject.Layout.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CharakterCreation : Page
    {
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
            DisplayTech = false;
        }
        #region Funktions All
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton rb)
            {
                string gameTypeName = rb.Tag.ToString();
                switch (gameTypeName)
                {
                    case "DSA":
                        ViewModel.CurrentDate = Game.CurrentYearDSA;
                        SetCulture = "Kulutr-/en:";
                        DisplayTech = false;
                        break;
                    case "PNP":
                        ViewModel.CurrentDate = Game.CurrentYearPNP;
                        SetCulture = "Vorgeschichte:";
                        DisplayTech = true;
                        break;
                }
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
            ViewModel.Gender = e.ToString();
        }
        private void XAML_FamilyStatuscomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Familistatus = e.ToString();
        }
        #endregion
        #region Funktions PNP
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
    }
}
