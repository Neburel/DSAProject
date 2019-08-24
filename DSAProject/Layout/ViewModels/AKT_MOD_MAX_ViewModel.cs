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
    public class AKT_MOD_MAX_ViewModel : AbstractPropertyChanged
    {
        #region Variables
        private bool isModVisible = true;
        private bool isValueEditable = true;
        private string name;
        private int aktValue;
        private int modValue;
        #endregion
        #region Properties
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
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
        public bool IsValueEditable
        {
            get => isValueEditable;
            set
            {
                isValueEditable = value;
                OnPropertyChanged(nameof(IsValueEditable));
            }
        }
        public bool IsModVisible
        {
            get => isModVisible;
            set
            {
                isModVisible = value;
                OnPropertyChanged(nameof(IsModVisible));
            }
        }
        #endregion

    }
}
