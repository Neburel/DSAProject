using DSAProject.Classes.Charakter;
using DSAProject.Classes.Interfaces;
using DSAProject.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Game
{
    /*
        Heldenbrief ->Helm(Spartahelm)
        Waffentalente(Kampftalente) ->Gekreuzte Schwerter, Bogen
        Allgemeinte Talente ->Hammer und Sichel
        Sprachen(Schriftrolle)
        Gaben(Göttersymbolik) (Altar), Gefaltete Hände

        Körperliche Talente(Arm)
        Natur(Baum)
        Wissstalente(Buch)
        */


    public static class Game 
    {
        public static event EventHandler StartPage;
        public static event EventHandler CharakterChanged;
        #region Variables
        private static ICharakter charakter = new CharakterDSA();
        #endregion
        #region Properties
        public static ICharakter Charakter
        {
            get => charakter;
            set
            {
                charakter = value;
                CharakterChanged?.Invoke(null, null);
            }
        } 
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
