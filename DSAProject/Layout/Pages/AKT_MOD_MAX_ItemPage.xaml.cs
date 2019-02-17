using DSAProject.Classes.Charakter;
using DSAProject.Layout.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
        #region Properties
        public AttributeSinglePageViewModel ViewModel = new AttributeSinglePageViewModel();
        public GridLength GetWidthName { get; } = new GridLength(110);
        public GridLength GetBoxLength { get; } = new GridLength(30);
        public string ItemName 
        {
            get => ViewModel.Name;
            set { ViewModel.Name = value; }
        }
        public int AKTValue
        {
            get => ViewModel.AKTValue;
            set
            {
                ViewModel.AKTValue = value;
            }
        }
        public int MODValue
        {
            get => ViewModel.MODValue;
            set => ViewModel.MODValue = value;
        }
        #endregion
        public AKT_MOD_MAX_ItemPage()
        {
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
    }
}
