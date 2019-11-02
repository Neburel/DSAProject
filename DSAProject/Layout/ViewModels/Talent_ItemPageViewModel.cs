using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DSAProject.Layout.ViewModels
{
    public class Talent_ItemPageViewModel : AbstractPropertyChanged
    {
        #region Varabiels
        private Visibility isProbeTextVisibility    = Visibility.Collapsed;
        private Visibility isATPAVisibility         = Visibility.Collapsed;

        private string be;
        private string taw;
        private string at;
        private string pa;
        private string probe;
        private string probeText;
        private string talentName;
        private string deductionString;
        private string deductionStringFreeText;
        private string requirementString;
        private string requirementString2;
        #endregion

        public Visibility IsProbeTextVisibility
        {
            get => isProbeTextVisibility;
            set
            {
                isProbeTextVisibility = value;
                OnPropertyChanged(nameof(IsProbeTextVisibility));
            }
        }
        public Visibility IsATPAVisibility
        {
            get => isATPAVisibility;
            set
            {
                isATPAVisibility = value;
                OnPropertyChanged(nameof(IsATPAVisibility));
            }
        }

        public string BE
        {
            get => be;
            set
            {
                be = value;
                OnPropertyChanged(nameof(BE));
            }
        }
        public string AT
        {
            get => at;
            set
            {
                at = value;
                OnPropertyChanged(nameof(AT));
            }
        }
        public string PA
        {
            get => pa;
            set
            {
                pa = value;
                OnPropertyChanged(nameof(PA));
            }
        }
        public string TAW
        {
            get => taw;
            set
            {
                taw = value;
                OnPropertyChanged(nameof(TAW));

                if (TalentName == "Sinnenschärfe")
                {

                }
            }
        }
        public string Probe
        {
            get => probe;
            set
            {
                probe = value;
                OnPropertyChanged(nameof(Probe));
            }
        }
        public string ProbeText
        {
            get => probeText;
            set
            {
                probeText = value;
                OnPropertyChanged(nameof(ProbeText));
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
        public string DeductionStringTalent
        {
            get => deductionString;
            set
            {
                deductionString = value;
                OnPropertyChanged(nameof(DeductionStringTalent));
            }
        }
        public string DeductionStringFreeText
        {
            get
            {
                return deductionStringFreeText;
            }
            set
            {
                deductionStringFreeText = value;
                OnPropertyChanged(nameof(DeductionStringFreeText));
            }
        }
        public string RequirementStringFreeText
        {
            get => requirementString;
            set
            {
                requirementString = value;
                OnPropertyChanged(nameof(RequirementStringFreeText));
            }
        }
        public string RequirementStringRest
        {
            get => requirementString2;
            set
            {
                requirementString2 = value;
                OnPropertyChanged(nameof(RequirementStringRest));
            }
        }
    }
}
