using DSALib;
using DSALib.Charakter.Other;
using DSALib.Interfaces;
using DSALib.Utils;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using static DSAProject.Layout.Views.AKT_MOD_MAX_ItemPage;

namespace DSAProject.Layout.Pages
{
    public sealed partial class AttributPage : Page
    {
        public enum AttributePageMode
        {
            AttributeNormal = 1,
            AttributeNormalEdit = 2,
            AttributeTrait = 3,
            ValueStandart = 4,
            ValueTrait = 5,
            ResourceStandart = 6,
            ResourceTrait = 7
        }
        private AttributePageMode mode = AttributePageMode.AttributeNormalEdit;
        private Trait trait = new Trait();
        public AttributePageMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                BuildPage();
            }
        }
        public Trait Trait
        {
            get => trait;
            set
            {
                trait = value;
                BuildPage();
            }
        }

        public AttributPage()
        {
            this.InitializeComponent();
            BuildPage();
        }

        #region Helper
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
            newView.MinValueAsInt = aktValue;
            newView.ValueTwo = modValue;

            if (!string.IsNullOrEmpty(toolTip))
            {
                newView.SetTooltip(toolTip);
            }

            return newView;
        }

        private void BuildPage()
        {
            var mainGrid = XAML_Grid;
            mainGrid.Children.Clear();
            switch (Mode)
            {
                case AttributePageMode.AttributeNormal:
                case AttributePageMode.AttributeNormalEdit:
                    BuildAttributPage(mainGrid, mode);
                    break;
                case AttributePageMode.AttributeTrait:
                    BuildAttributTraitPage(mainGrid, Game.Charakter.Attribute.UsedAttributs, trait);
                    break;
                case AttributePageMode.ValueStandart:
                    BuildValuePage(mainGrid);
                    break;
                case AttributePageMode.ValueTrait:
                    BuildValueTraitPage(mainGrid, Game.Charakter.Values.UsedValues, trait);
                    break;
                case AttributePageMode.ResourceStandart:
                    BuildResourcePage(mainGrid);
                    break;
                case AttributePageMode.ResourceTrait:
                    BuildResourceTraitPage(mainGrid, Game.Charakter.Resources.UsedValues, trait);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
        private static void BuildAttributPage(Grid mainGrid, AttributePageMode mode, int width = 110)
        {
            var attribute = Game.Charakter.Attribute;
            mainGrid.RowDefinitions.Add(new RowDefinition());

            AKTMODMAXMode titleMode = AKTMODMAXMode.AKTModMax;
            AKTMODMAXMode normalMode = AKTMODMAXMode.AKTModMax;

            if (mode == AttributePageMode.AttributeNormalEdit)
            {
                titleMode = AKTMODMAXMode.AKtModMaxEditTitle;
                normalMode = AKTMODMAXMode.AKTMODMAXEdit;
            }
            else if (mode == AttributePageMode.AttributeTrait)
            {
                titleMode = AKTMODMAXMode.AKTModMaxTrait;
                normalMode = AKTMODMAXMode.AKTModMaxTrait;
            }
            else
            {
                throw new System.NotImplementedException();
            }

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
                    mode: normalMode,
                    IsValueEditable: true,
                    aktValue: attribute.GetAttributAKTValue(item, out Error error),
                    modValue: attribute.GetAttributMODValue(item, out error),
                    name: item.ToString());
                if (i == 0)
                {
                    newView.Mode = titleMode;
                }
                attribute.ChangedAKT += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = attribute.GetAttributAKTValue(item, out error);
                        newView.MinValueAsInt = value;
                        sumVieW.MinValueAsInt = attribute.GetSumValueAttributeAKT;
                    }
                };
                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAKTValue(item, currentValue + 1, out error);
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = attribute.GetAttributAKTValue(item, out error);
                    attribute.SetAKTValue(item, currentValue - 1, out error);
                };
                i++;
            }
            #region Set Summe Row
            if (mode == AttributePageMode.AttributeNormal || mode == AttributePageMode.AttributeNormalEdit)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                mainGrid.Children.Add(sumVieW);
                Grid.SetRow(sumVieW, i);
            }
            #endregion
        }
        private static void BuildAttributTraitPage(Grid mainGrid, List<CharakterAttribut> attribute, Trait trait, int width = 110)
        {
            int i = 0;
            foreach (var item in attribute)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    mainGrid: mainGrid,
                    pos: i,
                    width: width,
                    mode: AKTMODMAXMode.AKTModMaxTrait,
                    IsValueEditable: true,
                    aktValue: trait.GetValue(item),
                    modValue: 0,
                    name: item.ToString());

                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = trait.GetValue(item) + 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = trait.GetValue(item) - 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                i++;
            }
        }
        private static void BuildValuePage(Grid mainGrid)
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
                        newView.MinValueAsInt = values.GetAKTValue(args, out error);
                        if (error != null)
                        {
                            Logger.Log(LogLevel.ErrorLog, error);
                        }
                    }
                };
            }
        }
        private static void BuildValueTraitPage(Grid mainGrid, List<IValue> values, Trait trait)
        {
            var i = 0;
            foreach (var item in values)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    width: 130,
                    mode: AKTMODMAXMode.AKTModMaxTrait,
                    IsValueEditable: false,
                    aktValue: trait.GetValue(item),
                    modValue: 0,
                    name: item.Name,
                    toolTip: item.InfoText);

                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);

                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = trait.GetValue(item) + 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = trait.GetValue(item) - 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                i++;
            }
        }
        private static void BuildResourcePage(Grid mainGrid)
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
                        newView.MinValueAsInt = values.GetAKTValue(args, out error);
                    }
                };
            }
        }
        private static void BuildResourceTraitPage(Grid mainGrid, List<IResource> values, Trait trait)
        {
            var i = 0;
            foreach (var item in values)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                var newView = CreateNewView(
                    width: 130,
                    mode: AKTMODMAXMode.AKTModMaxTrait,
                    IsValueEditable: false,
                    aktValue: trait.GetValue(item),
                    modValue: 0,
                    name: item.Name,
                    toolTip: item.InfoText);

                mainGrid.Children.Add(newView);
                Grid.SetRow(newView, i);

                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = trait.GetValue(item) + 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = trait.GetValue(item) - 1;
                    trait.SetValue(item, currentValue);
                    newView.MinValueAsInt = currentValue;
                };
                i++;
            }
        }
        #endregion
    }
}
