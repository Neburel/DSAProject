using DSAProject.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Layout.ViewModels
{
    public class AttributeSinglePageViewModel : INotifyPropertyChanged
    {   
        #region Variables
        private string attributeName;
        private int aktValue;
        private int modValue;
        #endregion
        #region Properties
        public string AttributeName
        {
            get => attributeName;
            set
            {
                attributeName = value;
                OnPropertyChanged(nameof(AttributeName));
            }
        }
        public int AKTValue
        {
            get => aktValue;
            set
            {
                aktValue = value;
                OnPropertyChanged(nameof(AKTValue));
                OnPropertyChanged(nameof(MaxValue));
            }
        }
        public int MODValue
        {
            get => modValue;
            set
            {
                modValue = value;
                OnPropertyChanged(nameof(MODValue));
                OnPropertyChanged(nameof(MaxValue));
            }
        }
        public int MaxValue
        {
            get => AKTValue + MODValue;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
