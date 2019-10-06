using DSALib.Utils;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Pages;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DSAProject.Layout.Utils
{
    public static class PageHelpBuilder
    {
        private static AKT_MOD_MAX_ItemPage CreateNewView(Grid mainGrid, int pos, bool IsModVisible, bool IsValueEditable, int width, int aktValue, int modValue, string name, string toolTip = "")
        {
            mainGrid.RowDefinitions.Add(new RowDefinition());
            var newView = CreateNewView(
                width: width,
                IsModVisible: IsModVisible,
                IsValueEditable: IsValueEditable,
                aktValue: aktValue,
                modValue: modValue,
                name: name,
                toolTip: toolTip);
            mainGrid.Children.Add(newView);
            Grid.SetRow(newView, pos);
            return newView;
        }
        private static AKT_MOD_MAX_ItemPage CreateNewView(bool IsModVisible, bool IsValueEditable, int width, int aktValue, int modValue, string name, string toolTip = "")
        {
            var newView = new AKT_MOD_MAX_ItemPage(width);
            newView.ViewModel.Name = name;
            newView.ViewModel.IsModVisible = IsModVisible;
            newView.ViewModel.IsValueEditable = IsValueEditable;
            newView.ViewModel.AKTValue = aktValue;
            newView.ViewModel.MODValue = modValue;

            if (!string.IsNullOrEmpty(toolTip))
            {
                newView.SetTooltip(toolTip);
            }

            return newView;
        }

        internal static void BuildAttributPage(Grid mainGrid)
        {
            int width = 110;
            var attribute = Game.Charakter.Attribute;
            mainGrid.RowDefinitions.Add(new RowDefinition());

            #region Create Summe
            var sumVieW = CreateNewView(
                width: width,
                IsModVisible: false,
                IsValueEditable: false,
                aktValue: attribute.GetSumValueAttributeAKT,
                modValue: attribute.GetSumValueAttributMod,
                name: "Gesamt");

            #endregion
            var i = 0;
            foreach (var item in attribute.UsedAttributs)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    mainGrid: mainGrid,
                    pos: i,
                    width: width,
                    IsModVisible: true,
                    IsValueEditable: true,
                    aktValue: attribute.GetAttributAKTValue(item, out Error error),
                    modValue: attribute.GetAttributMODValue(item, out error),
                    name: item.ToString());
                if (i == 0) { newView.IsTitleVisible = true; }
                attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.ViewModel.AKTValue = value;
                        sumVieW.ViewModel.AKTValue = attribute.GetSumValueAttributeAKT;
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
                i++;
            }
            #region Set Summe Row
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.Children.Add(sumVieW);
            Grid.SetRow(sumVieW, i);
            #endregion
        }
        internal static void BuildValuePage(Grid mainGrid)
        {
            var values = Game.Charakter.Values;
            mainGrid.RowDefinitions.Add(new RowDefinition());

            var i = 0;

            foreach (var item in values.UsedValues)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    width: 130,
                    IsModVisible: true,
                    IsValueEditable: false,
                    aktValue: values.GetAKTValue(item, out Error error),
                    modValue: values.GetMODValue(item, out error),
                    name: item.Name,
                    toolTip: item.InfoText);
                if (i == 0) { newView.IsTitleVisible = true; newView.MaxString = "Ergebnis"; }
                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;

                values.ChangedAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        newView.ViewModel.AKTValue = values.GetAKTValue(args, out error);
                        if (error != null)
                        {
                            Logger.Log(error);
                        }
                    }
                }; 
            }
        }
        internal static void BuildResourcePage(Grid mainGrid)
        {
            var values = Game.Charakter.Resources;
            mainGrid.RowDefinitions.Add(new RowDefinition());

            var i = 0;

            foreach (var item in values.UsedValues)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    width: 130,
                    IsModVisible: true,
                    IsValueEditable: false,
                    aktValue: values.GetAKTValue(item, out Error error),
                    modValue: values.GetMODValue(item, out error),
                    name: item.Name,
                    toolTip: item.InfoText);
                if (i == 0) { newView.IsTitleVisible = true; newView.MaxString = "Ergebnis"; }
                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;

                values.ChangedAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        newView.ViewModel.AKTValue = values.GetAKTValue(args, out error);
                    }
                };
            }
        }
    }
}
