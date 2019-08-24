using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
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
            }
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
    }
}
