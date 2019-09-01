using DSALib;
using DSAProject.Classes.Interfaces;
using DSAProject.util;

using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DSAProject.Layout.ViewModels
{
    public class CreateTalentViewModel : AbstractPropertyChanged
    {
        #region Variables
        private int deductionValue                                  = 0;
        private int deductionValueSelected                          = 0;
        private int requirementOffValue                             = 0;
        private int requirementNeedValue                            = 0;
        private string beString                                     = string.Empty;
        private string probeString                                  = string.Empty;
        private string deductionString                              = string.Empty;
        private string talentName                                   = string.Empty;
        private string talentNameExtensíon                          = string.Empty;
        private string requirementString                            = string.Empty;
        private string deductionFreeText                            = string.Empty;
        private Brush talentTypeBorderColor                         = new SolidColorBrush(Windows.UI.Colors.LightGray);
        private Brush talentNameBorderColor                         = new SolidColorBrush(Windows.UI.Colors.LightGray);
        private Visibility isProbeSelectionVisibile                 = Visibility.Visible;
        private Visibility isRequirementSelectionVisible            = Visibility.Visible;
        private Visibility isFatherTalentsVisible                   = Visibility.Visible;
        private ObservableCollection<ITalent> talents               = new ObservableCollection<ITalent>();
        private ObservableCollection<ITalent> fathertalents         = new ObservableCollection<ITalent>();
        private ObservableCollection<ITalentRequirement> req        = new ObservableCollection<ITalentRequirement>();
        private ObservableCollection<CharakterAttribut> probe       = new ObservableCollection<CharakterAttribut>();
        private ObservableCollection<ITalentDeduction> deductions   = new ObservableCollection<ITalentDeduction>();
        #endregion
        #region BorderColor
        public Brush TalentTypeBorderColor
        {
            get => talentTypeBorderColor;
            set
            {
                talentTypeBorderColor = value;
                OnPropertyChanged(nameof(TalentTypeBorderColor));
            }
        }
        public Brush TalentNameBorderColor
        {
            get => talentNameBorderColor;
            set
            {
                talentTypeBorderColor = value;
                OnPropertyChanged(nameof(TalentNameBorderColor));
            }
        }
        #endregion
        #region Visiblity
        public Visibility IsProbeSelectionVisibile
        {
            get => isProbeSelectionVisibile;
            set
            {
                isProbeSelectionVisibile = value;
                OnPropertyChanged(nameof(IsProbeSelectionVisibile));
            }
        }
        public Visibility IsRequirementSelectionVisible
        {
            get => isRequirementSelectionVisible;
            set
            {
                isRequirementSelectionVisible = value;
                OnPropertyChanged(nameof(IsRequirementSelectionVisible));
            }
        }
        public Visibility IsFatherTalentsVisible
        {
            get => isFatherTalentsVisible;
            set
            {
                isFatherTalentsVisible = value;
                OnPropertyChanged(nameof(IsFatherTalentsVisible));
            }
        }
        #endregion
        public Guid ID { get; set; }
        public int DeductionValue
        {
            get => deductionValue;
            set
            {
                deductionValue = value;
                OnPropertyChanged(nameof(DeductionValue));
            }
        }
        public int DeductionValueSelected
        {
            get => deductionValueSelected;
            set
            {
                deductionValueSelected = value;
                OnPropertyChanged(nameof(DeductionValueSelected));
            }
        }
        public int RequirementOffValue
        {
            get => requirementOffValue;
            set
            {
                requirementOffValue = value;
                OnPropertyChanged(nameof(RequirementOffValue));
            }
        }
        public int RequirementNeedValue
        {
            get => requirementNeedValue;
            set
            {
                requirementNeedValue = value;
                OnPropertyChanged(nameof(RequirementNeedValue));
            }
        }
        public string BEString
        {
            get => beString;
            set
            {
                beString = value;
                OnPropertyChanged(nameof(BEString));
            }
        }
        public string TalentName
        {
            get => talentName;
            set
            {
                talentName = value;
                OnPropertyChanged(nameof(TalentName));
            }
        }
        public string TalentNameExtension
        {
            get => talentNameExtensíon;
            set
            {
                talentNameExtensíon = value;
                OnPropertyChanged(nameof(TalentNameExtension));
            }
        }
        public string ProbeString
        {
            set
            {
                probeString = value;
                OnPropertyChanged(nameof(ProbeString));
            }
            get => probeString;
        }
        public string DeductionString
        {
            get => deductionString;
            set
            {
                deductionString = value;
                OnPropertyChanged(nameof(DeductionString));
            }
        }
        public string RequirementString
        {
            set
            {
                requirementString = value;
                OnPropertyChanged(nameof(RequirementString));
            }
            get => requirementString;
        }
        public string DeductionFreeText
        {
            get => deductionFreeText;
            set
            {
                deductionFreeText = value;
                OnPropertyChanged(nameof(DeductionFreeText));
            }
        }
        public ObservableCollection<ITalentRequirement> Req
        {
            get => req;
            set
            {
                req = value;
                OnPropertyChanged(nameof(Req));
            }
        }
        public ObservableCollection<CharakterAttribut> Probes
        {
            set
            {
                probe = value;
                OnPropertyChanged(nameof(Probes));
            }
            get => probe;
        }
        public ObservableCollection<ITalent> FatherTalents
        {
            get => fathertalents;
            set
            {
                fathertalents = value;
                OnPropertyChanged(nameof(fathertalents));
            }
        }
        public ObservableCollection<ITalentDeduction> Deductions
        {
            get => deductions;
            set
            {
                deductions = value;
                OnPropertyChanged(nameof(Deductions));
            }
        }

        public ObservableCollection<ITalent> Talents
        {
            get => talents;
            set
            {
                if(value != talents)
                {
                    talents = value;
                    OnPropertyChanged(nameof(Talents));
                }
            }
        }
    }
}
