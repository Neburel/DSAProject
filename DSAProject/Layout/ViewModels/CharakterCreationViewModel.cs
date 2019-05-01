using DSAProject.util;

namespace DSAProject.Layout.ViewModels
{
    public class CharakterCreationViewModel : AbstractPropertyChanged
    {
        #region Variables
        private int airSpeed;
        private int waterSpeed;
        private int groundSpeed;
        private int techstufeMin;
        private int techstufeMax;

        private string playerName;
        private string playerAdressName;
        private string race;
        private string birthDate;
        private string age;
        private string gender;
        private string hairColor;
        private string skinColor;
        private string eyeColor;
        private string faith;
        private string playerHeight;
        private string playerWeight;
        private string culture;
        private string profession;
        private string familyStatus;
        private string currentDate;
        #endregion
        #region Properties
        public int AirSpeed
        {
            get => airSpeed;
            set
            {
                airSpeed = value;
                OnPropertyChanged(nameof(AirSpeed));
            }
        }
        public int WaterSpeed
        {
            get => waterSpeed;
            set
            {
                waterSpeed = value;
                OnPropertyChanged(nameof(WaterSpeed));
            }
        }
        public int GroundSpeed
        {
            get => groundSpeed;
            set
            {
                groundSpeed = value;
                OnPropertyChanged(nameof(GroundSpeed));
            }
        }
        public int TechstufeMin
        {
            get => techstufeMin;
            set
            {
                techstufeMin = value;
                OnPropertyChanged(nameof(TechstufeMin));
            }
        }
        public int TechstufeMax
        {
            get => techstufeMax;
            set
            {
                techstufeMax = value;
                OnPropertyChanged(nameof(TechstufeMax));
            }
        }

        public string PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }
        public string PlayerAdressName
        {
            get => playerAdressName;
            set
            {
                playerAdressName = value;
                OnPropertyChanged(nameof(PlayerAdressName));
            }
        }
        public string Race
        {
            get => race;
            set
            {
                race = value;
                OnPropertyChanged(nameof(Race));
            }
        }
        public string BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public string Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public string CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string EyeColor
        {
            get => eyeColor;
            set
            {
                eyeColor = value;
                OnPropertyChanged(nameof(EyeColor));
            }
        }
        public string HairColor
        {
            get => hairColor;
            set
            {
                hairColor = value;
                OnPropertyChanged(nameof(HairColor));
            }
        }
        public string SkinColor
        {
            get => skinColor;
            set
            {
                skinColor = value;
                OnPropertyChanged(nameof(SkinColor));
            }
        }
        public string Faith
        {
            get => faith;
            set
            {
                faith = value;
                OnPropertyChanged(nameof(Faith));
            }
        }
        public string PlayerHeight
        {
            get => playerHeight;
            set
            {
                playerHeight = value;
                OnPropertyChanged(nameof(PlayerHeight));
            }
        }
        public string PlayerWeight
        {
            get => playerWeight;
            set
            {
                playerWeight = value;
                OnPropertyChanged(nameof(PlayerWeight));
            }
        }
        public string Culture
        {
            get => culture;
            set
            {
                culture = value;
                OnPropertyChanged(nameof(Culture));
            }
        }
        public string Profession
        {
            get => profession;
            set
            {
                profession = value;
                OnPropertyChanged(nameof(Profession));
            }
        }
        public string Familistatus
        {
            get => familyStatus;
            set
            {
                familyStatus = value;
                OnPropertyChanged(Familistatus);
            }
        }
        #endregion
    }
}
