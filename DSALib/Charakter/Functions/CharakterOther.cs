using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Charakter.Functions
{
    public class CharakterOther
    {
        #region Events
        public event EventHandler<int> ChangedAKTAP;
        public event EventHandler<int> ChangedInvestAP;
        public event EventHandler<int> ChangedRestAP;
        public event EventHandler<int> LevelChanged;
        #endregion
        #region Variables
        private int aktAP;
        private int investAP;
        #endregion
        public int Level { get; set; }
        public int TotalAPPlayer 
        {
            get => aktAP;
            set
            {
                aktAP = value;
                ChangedAKTAP?.Invoke(this, value);
                ChangedRestAP?.Invoke(this, RestAP);
            }
        }
        public int InvestedAPPlayer 
        {
            get => investAP;
            set
            {
                investAP = value;
                ChangedInvestAP?.Invoke(this, value);
                ChangedRestAP?.Invoke(this, RestAP);
            }
        }
        public int RestAP
        {
            get => TotalAPPlayer - InvestedAPPlayer;
        }        

        public CharakterOther()
        {
            ChangedAKTAP += (sender, arg) =>
            {
                Level = CalculateLevel();
                LevelChanged?.Invoke(this, Level);
            };
        }

        private int CalculateLevel()
        {
            var maxAP = TotalAPPlayer;
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
    }
}
