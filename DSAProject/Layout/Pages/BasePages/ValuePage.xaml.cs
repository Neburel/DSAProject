using DSALib.Utils;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            Utils.PageHelpBuilder.BuildValuePage(XAML_Grid);
        }
    }
}
