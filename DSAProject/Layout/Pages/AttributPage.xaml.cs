using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
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

namespace DSAProject.Layout.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AttributView : Page
    {
        #region Variables
        #endregion
        #region Properties
        public ICharakterAttribut Attribute { get; set; }
        #endregion

        public AttributView()
        {
            this.InitializeComponent();

            var chara = new CharakterDSA();
            Attribute = chara.Attribute;
            XAML_Grid.RowDefinitions.Add(new RowDefinition());

            XAML_Grid.RowDefinitions.Add(new RowDefinition());
            var y = new AttributSingleView();
            Grid.SetRow(y, 1);


            var i = 1;
            foreach(var item in Attribute.UsedAttributs)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition());
                var newView = new AttributSingleView
                {
                    Attribute = item,
                    AttributeAKTValue = Attribute.GetAttributAKTValue(item, out Error error)
                };
                Attribute.ChangedAttributAKTEvent += (sender, args) =>
                {
                    if (args == item)
                    {
                        var value = Attribute.GetAttributAKTValue(item, out error);
                        newView.AttributeAKTValue = value;
                        newView.Attribute = CharakterAttribut.Charisma;
                    }
                };
                newView.Event_ValueHigher += (sender, args) =>
                {
                    var currentValue = Attribute.GetAttributAKTValue(item, out error);
                    Attribute.SetAttributAKTValue(item, currentValue + 1, out error);
                };
                newView.Event_ValueLower += (sender, agrs) =>
                {
                    var currentValue = Attribute.GetAttributAKTValue(item, out error);
                    Attribute.SetAttributAKTValue(item, currentValue - 1, out error);
                };
                

                XAML_Grid.Children.Add(newView);
                Grid.SetRow(newView, i);
                i++;
            }
        }
    }
}
