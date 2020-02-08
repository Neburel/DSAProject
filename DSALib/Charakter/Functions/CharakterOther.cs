using System;

namespace DSALib.Charakter.Functions
{
    public class CharakterOther
    {
        #region Events
        public event EventHandler<int> ChangedEarnedAP;
        public event EventHandler<int> ChangedInvestAP;
        public event EventHandler<int> ChangedRestAP;
        public event EventHandler<int> LevelChanged;
        #endregion
        #region Variables
        private int aktAP;
        private int aktAPMod;
        private int investAP;
        private int investAPMod;
        #endregion
        public int Level { get; set; }
        #region AP Verdient
        /// <summary>
        /// Gibt die durch den Spieler manuel einegebenen AP an
        /// </summary>
        public int APEarned 
        {
            get => aktAP;
            set
            {
                aktAP = value;
                if (aktAP < 0) aktAP = 0;
                ChangedAPEarned();
            }
        }
        /// <summary>
        /// Gibt die durch die Traits eingegebene AP an
        /// </summary>
        public int APEarnedMod
        {
            get => aktAPMod;
            set
            {
                aktAPMod = value;
                ChangedAPEarned();
            }
        }
        /// <summary>
        /// Gibt die gesamt Verdienten AP (manuel + traits) an
        /// </summary>
        public int APEarnedMax
        {
            get => APEarned + APEarnedMod;
        }
        #endregion
        #region AP Investiert
        /// <summary>
        /// Gibt die durch den Spieler manuel investierten AP an
        /// </summary>
        public int APInvested 
        {
            get => investAP;
            set
            {
                investAP = value;
                if (investAP < 0) investAP = 0;
                ChangedAPInvested();
            }
        }
        /// <summary>
        /// Gibt die durch die Traits investierten AP an
        /// </summary>
        public int APInvestedMod
        {
            get => investAPMod;
            set
            {
                investAPMod = value;
                ChangedAPInvested();
            }
        }
        /// <summary>
        /// Gibt die gesamt investierten AP (manuel + traits) an
        /// </summary>
        public int APInvestedMax
        {
            get => APInvested + APInvestedMod;
        }
        #endregion
        public int RestAP
        {
            get => APEarnedMax - APInvestedMax;
        }        
        
        public CharakterOther()
        {
            ChangedEarnedAP += (sender, arg) =>
            {
                Level = CalculateLevel();
                LevelChanged?.Invoke(this, Level);
            };
        }

        private int CalculateLevel()
        {
            var maxAP = APEarnedMax;
            var result = 0;

            if(maxAP > 1000)
            {
                result = maxAP / 1000 + 4;
            }
            else
            {
                result = maxAP / 200;
            }

            return 1 + result;
        }

        #region EventHelper
        private void ChangedAPEarned()
        {
            ChangedEarnedAP?.Invoke(this, APEarnedMax);
            ChangedRestAP?.Invoke(this, RestAP);
        }
        private void ChangedAPInvested()
        {
            ChangedInvestAP?.Invoke(this, APInvestedMax);
            ChangedRestAP?.Invoke(this, RestAP);
        }
        #endregion
    }
}
