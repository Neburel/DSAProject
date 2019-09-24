using DSALib.Utils;
using DSAProject.Classes.Charakter;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
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

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ValuePage : Page
    {
        private Dictionary<IValue, AKT_MOD_MAX_ItemPage> dic = new Dictionary<IValue, AKT_MOD_MAX_ItemPage>();

        public ValuePage()
        {
            this.InitializeComponent();
            XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Star), MaxHeight = 200 });

            var values  = Game.Charakter.Values;
            var i       = 0;
        
            foreach(var item in values.UsedValues)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Star), MaxHeight = 200 });
                #region NewView
                var newView                 = new AKT_MOD_MAX_ItemPage(130);
                newView.ViewModel.Name      = item.Name;
                newView.ViewModel.AKTValue  = values.GetAKTValue(item, out Error error);
                newView.IsValueEditable     = false;

                if (!string.IsNullOrEmpty(item.InfoText))
                {
                    newView.SetTooltip(item.InfoText);
                }
                if (i == 0) { newView.IsTitleVisible = true; newView.MaxString = "Ergebnis"; }
                #endregion
                dic.Add(item, newView);
                XAML_Grid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;
            }

            values.ChangedAKTEvent += (sender, args) =>
            {
                var ok = dic.TryGetValue(args, out AKT_MOD_MAX_ItemPage value);
                if(ok == true)
                {
                    value.ViewModel.AKTValue = values.GetAKTValue(args, out Error error);
                    Logger.Log(error, nameof(ValuePage), "ChangedAKTEvent");
                }
            };
        }
    }
}
