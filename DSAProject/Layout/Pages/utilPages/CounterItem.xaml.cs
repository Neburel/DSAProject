using DSAProject.util;
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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CounterItem : Page
    {
        private CounterItemViewModel viewModel = new CounterItemViewModel();
        public CounterItem()
        {
            this.InitializeComponent();
        }
        private void XAML_ValueClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Value = string.Empty;
        }
        private void XAML_ValueXButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Value = "X";
        }
        private void XAML_PlusButton_Clicked(object sender, object e)
        {
            if(Int32.TryParse(viewModel.Value, out int value))
            {
                viewModel.Value = (value + 1).ToString();
            }
            else
            {
                viewModel.Value = "0";
            }
        }
        private void XAML_MinusButton_Clicked(object sender, object e)
        {
            if (Int32.TryParse(viewModel.Value, out int value))
            {
                viewModel.Value = (value - 1).ToString();
            }
            else
            {
                viewModel.Value = "0";
            }
        }
        public class CounterItemViewModel : AbstractPropertyChanged
        {
            public string Value
            {
                get => Get<string>();
                set => Set(value);
            }
        }
    }
}
