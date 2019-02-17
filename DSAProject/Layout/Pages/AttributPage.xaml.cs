﻿using DSAProject.Classes.Charakter;
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
    public sealed partial class AttributPage : Page
    {
        public AttributPage()
        {
            this.InitializeComponent();
            XAML_Grid.RowDefinitions.Add(new RowDefinition());

            var attribute   = Game.Charakter.Attribute;
            var i           = 1;
            foreach(var item in attribute.UsedAttributs)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition());
                var newView = new AKT_MOD_MAX_ItemPage
                {
                    ItemName = item.ToString(),
                    AKTValue = attribute.GetAttributAKTValue(item, out Error error)
                };
                attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.AKTValue = value;
                    }
                };
                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAttributAKTValue(item, currentValue + 1, out error);
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAttributAKTValue(item, currentValue - 1, out error);
                };
                

                XAML_Grid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;
            }
        }
    }
}
