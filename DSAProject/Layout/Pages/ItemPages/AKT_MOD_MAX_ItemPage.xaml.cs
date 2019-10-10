using DSAProject.Layout.ViewModels;
using DSAProject.util;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AKT_MOD_MAX_ItemPage : Page
    {
        public enum AKTMODMAXMode
        {
            AKTModMax,
            AKTModMaxTitle,
            AKTMODMAXEdit,
            AKtModMaxEditTitle        
        }

        #region Event
        public event EventHandler Event_ValueHigher;
        public event EventHandler Event_ValueLower;
        #endregion
        #region Variables
        private AKT_MOD_MAX_ViewModel ViewModel = new AKT_MOD_MAX_ViewModel();
        private AKTMODMAXMode mode;
        #endregion
        #region Properties
        public int ValueOne { get => ViewModel.AKTValue; set => ViewModel.AKTValue = value; }
        public int ValueTwo { get => ViewModel.MODValue; set => ViewModel.MODValue = value; }
        public int ValueThree { get => ViewModel.MaxValue; }
        public string MaxString
        {
            get => XAML_TitleResult.Text;
            set => XAML_TitleResult.Text = value;
        }
        public AKTMODMAXMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                switch (value)
                {
                    case AKTMODMAXMode.AKTModMax:
                        ViewModel.IsValueEditable = false;
                        ViewModel.IsModVisible = Visibility.Visible;
                        break;
                    case AKTMODMAXMode.AKTModMaxTitle:
                        ViewModel.IsValueEditable = false;
                        ViewModel.IsModVisible = Visibility.Visible;
                        ViewModel.IsTitle1Visible = Visibility.Visible;
                        ViewModel.IsTitle2Visible = Visibility.Visible;
                        ViewModel.IsTitle3Visible = Visibility.Visible;
                        break;
                    case AKTMODMAXMode.AKTMODMAXEdit:
                        ViewModel.IsValueEditable   = true;
                        ViewModel.IsModVisible      = Visibility.Visible;
                        break;
                    case AKTMODMAXMode.AKtModMaxEditTitle:
                        ViewModel.IsValueEditable   = true;
                        ViewModel.IsModVisible      = Visibility.Visible;
                        ViewModel.IsTitle1Visible   = Visibility.Visible;
                        ViewModel.IsTitle2Visible   = Visibility.Visible;
                        ViewModel.IsTitle3Visible   = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        public AKT_MOD_MAX_ItemPage(double widthName, string name)
        {
            ViewModel.GetWidthName = widthName;
            ViewModel.Name = name;

            InitializeComponent();
        }
        private void XML_ButtonReduceValue_Click(object sender, RoutedEventArgs e)
        {
            Event_ValueLower?.Invoke(this, null);
        }
        private void XML_ButtonHigherValue_Click(object sender, RoutedEventArgs e)
        {
            Event_ValueHigher?.Invoke(this, null);
        }
        public void SetTooltip(string toolTipText)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.Content = toolTipText;
            ToolTipService.SetToolTip(XAML_AttributName, toolTip);
        }

        public class AKT_MOD_MAX_ViewModel : AbstractPropertyChanged
        {
            #region Variables
            private bool isModVisible = true;
            private bool isValueEditable = true;
            private int aktValue;
            private int modValue;
            private string name;
            private double getWidthName = 130;
            private double getBoxLength = 30;
            private Visibility isValue2Visible = Visibility.Collapsed;
            private Visibility isTitle1Visible = Visibility.Collapsed;
            private Visibility isTitle2Visible = Visibility.Collapsed;
            private Visibility isTitle3Visible = Visibility.Collapsed;
            #endregion
            #region Properties
            public int AKTValue
            {
                get => aktValue;
                set
                {
                    aktValue = value;
                    OnPropertyChanged(nameof(AKTValue));
                    OnPropertyChanged(nameof(MaxValue));
                }
            }
            public int MODValue
            {
                get => modValue;
                set
                {
                    modValue = value;
                    OnPropertyChanged(nameof(MODValue));
                    OnPropertyChanged(nameof(MaxValue));
                }
            }
            public int MaxValue
            {
                get => AKTValue + MODValue;
            }
            public bool IsValueEditable
            {
                get => isValueEditable;
                set
                {
                    isValueEditable = value;
                    OnPropertyChanged(nameof(IsValueEditable));
                }
            }
            public Visibility IsModVisible
            {
                get => isValue2Visible;
                set
                {
                    isValue2Visible = value;
                    OnPropertyChanged(nameof(IsTitle1Visible));
                }
            }
            public Visibility IsTitle1Visible
            {
                get => isTitle1Visible;
                set
                {
                    isTitle1Visible = value;
                    OnPropertyChanged(nameof(IsTitle1Visible));
                }
            }
            public Visibility IsTitle2Visible
            {
                get => isTitle2Visible;
                set
                {
                    isTitle2Visible = value;
                    OnPropertyChanged(nameof(IsTitle1Visible));
                }
            }
            public Visibility IsTitle3Visible
            {
                get => isTitle3Visible;
                set
                {
                    isTitle3Visible = value;
                    OnPropertyChanged(nameof(IsTitle3Visible));
                }
            }
            public string Name
            {
                get => name;
                set
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
            public double GetWidthName
            {
                get => getWidthName;
                set
                {
                    getWidthName = value;
                    OnPropertyChanged(nameof(GetWidthName));
                }
            }
            public double GetBoxLength
            {
                get => getBoxLength;
                set
                {
                    getBoxLength = value;
                    OnPropertyChanged(nameof(GetBoxLength));
                }
            }
            #endregion
        }
    }
}
