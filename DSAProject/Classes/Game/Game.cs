using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Game
{
    public static class Game
    {
        public static event EventHandler StartPage;
        #region Variables
        #endregion
        #region Properties
        public static ICharakter Charakter { get; set; } = new CharakterDSA();
        public static string CharakterSaveFolder { get; } = "CharakterSaves";
        public static string CurrentYearDSA { get; } = "? nach Bosporos Fall";
        public static string CurrentYearPNP { get; } = "3135";
        #endregion
        public static void GoStartPage()
        {
            StartPage?.Invoke(null, null);


        }
    }
}
