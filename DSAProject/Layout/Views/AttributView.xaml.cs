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

namespace DSAProject.Layout.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AttributView : Page
    {
        public List<AttributSingleView> Items;

        public AttributView()
        {
            this.InitializeComponent();

            XAML_Grid.RowDefinitions.Add(new RowDefinition());
            XAML_Grid.RowDefinitions.Add(new RowDefinition());
            XAML_Grid.RowDefinitions.Add(new RowDefinition());

            var x = new AttributSingleView();
            XAML_Grid.Children.Add(x);
            Grid.SetRow(x, 1);
           

            var y = new AttributSingleView();
            XAML_Grid.Children.Add(y);
            Grid.SetRow(y, 2);
        }
    }
}
