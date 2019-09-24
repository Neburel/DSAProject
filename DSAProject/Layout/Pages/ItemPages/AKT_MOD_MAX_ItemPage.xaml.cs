using DSAProject.Layout.ViewModels;
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
        #region Event
        public event EventHandler Event_ValueHigher;
        public event EventHandler Event_ValueLower;
        #endregion
        #region Variables
        private bool isTitleVisible = false;
        #endregion
        #region Properties
        public AKT_MOD_MAX_ViewModel ViewModel = new AKT_MOD_MAX_ViewModel();
        public bool IsValueEditable
        {
            get => ViewModel.IsValueEditable;
            set
            {
                ViewModel.IsValueEditable = value;
            }
        }
        public bool IsModVisible
        {
            get => ViewModel.IsModVisible;
            set
            {
                ViewModel.IsModVisible = value;
                if (IsTitleVisible)
                {
                    XAML_TitleMod.Visibility = Visibility.Visible;
                }
            }
        }
        public bool IsTitleVisible
        {
            get => isTitleVisible;
            set
            {
                isTitleVisible = value;
                if (value)
                {
                    XAML_TitleAKT.Visibility    = Visibility.Visible;
                    if (IsModVisible)
                    {
                        XAML_TitleMod.Visibility = Visibility.Visible;
                    }
                    XAML_TitleResult.Visibility = Visibility.Visible;
                }
                else
                {
                    XAML_TitleAKT.Visibility    = Visibility.Collapsed;
                    XAML_TitleMod.Visibility    = Visibility.Collapsed;
                    XAML_TitleResult.Visibility = Visibility.Collapsed;
                }
            }
        }

        public string MaxString
        {
            get => XAML_TitleResult.Text;
            set => XAML_TitleResult.Text = value;
        }

        public GridLength GetWidthName { get; } = new GridLength(130);
        public GridLength GetBoxLength { get; } = new GridLength(30);
        #endregion
        public AKT_MOD_MAX_ItemPage(double WidthName)
        {
            GetWidthName = new GridLength(WidthName);
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
    }
}
