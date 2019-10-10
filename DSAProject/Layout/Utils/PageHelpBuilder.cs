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
using static DSAProject.Layout.Views.AKT_MOD_MAX_ItemPage;

namespace DSAProject.Layout.Utils
{
    public static class PageHelpBuilder
    {
        private static AKT_MOD_MAX_ItemPage CreateNewView(Grid mainGrid, int pos, AKTMODMAXMode mode, bool IsValueEditable, int width, int aktValue, int modValue, string name, string toolTip = "")
        {
            mainGrid.RowDefinitions.Add(new RowDefinition());
            var newView = CreateNewView(
                width: width,
                mode: mode,
                IsValueEditable: IsValueEditable,
                aktValue: aktValue,
                modValue: modValue,
                name: name,
                toolTip: toolTip);
            mainGrid.Children.Add(newView);
            Grid.SetRow(newView, pos);
            return newView;
        }
        private static AKT_MOD_MAX_ItemPage CreateNewView(AKTMODMAXMode mode, bool IsValueEditable, int width, int aktValue, int modValue, string name, string toolTip = "")
        {
            var newView = new AKT_MOD_MAX_ItemPage(width, name);
            newView.Mode = mode;
            newView.ValueOne = aktValue;
            newView.ValueTwo = modValue;

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
                mode: AKTMODMAXMode.AKTMODMAXEdit,
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
                    mode: AKTMODMAXMode.AKTMODMAXEdit,
                    IsValueEditable: true,
                    aktValue: attribute.GetAttributAKTValue(item, out Error error),
                    modValue: attribute.GetAttributMODValue(item, out error),
                    name: item.ToString());
                if (i == 0)
                {
                    newView.Mode = AKTMODMAXMode.AKtModMaxEditTitle;
                }
                attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.ValueOne = value;
                        sumVieW.ValueOne = attribute.GetSumValueAttributeAKT;
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
                    mode: AKTMODMAXMode.AKTModMax,
                    IsValueEditable: false,
                    aktValue: values.GetAKTValue(item, out Error error),
                    modValue: values.GetMODValue(item, out error),
                    name: item.Name,
                    toolTip: item.InfoText);
                if (i == 0)
                {
                    newView.Mode = AKTMODMAXMode.AKTModMaxTitle;
                    newView.MaxString = "Ergebnis";
                }
                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;

                values.ChangedAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        newView.ValueOne = values.GetAKTValue(args, out error);
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
                    mode: AKTMODMAXMode.AKTModMax,
                    IsValueEditable: false,
                    aktValue: values.GetAKTValue(item, out Error error),
                    modValue: values.GetMODValue(item, out error),
                    name: item.Name,
                    toolTip: item.InfoText);
                if (i == 0)
                {
                    newView.Mode = AKTMODMAXMode.AKTModMaxTitle;
                    newView.MaxString = "Ergebnis";
                }
                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;

                values.ChangedAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        newView.ValueOne = values.GetAKTValue(args, out error);
                    }
                };
            }
        }
    }
}
