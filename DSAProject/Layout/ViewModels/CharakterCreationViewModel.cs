using DSAProject.Classes.Charakter.Description;
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

        private Descriptor age                  = new Descriptor { };
        private Descriptor race                 = new Descriptor { };
        private Descriptor faith                = new Descriptor { };
        private Descriptor gender               = new Descriptor { };
        private Descriptor culture              = new Descriptor { };
        private Descriptor eyeColor             = new Descriptor { };
        private Descriptor hairColor            = new Descriptor { };
        private Descriptor skinColor            = new Descriptor { };
        private Descriptor birthDate            = new Descriptor { };
        private Descriptor playerName           = new Descriptor { };
        private Descriptor profession           = new Descriptor { };
        private Descriptor currentDate          = new Descriptor { };
        private Descriptor familyStatus         = new Descriptor { };
        private Descriptor playerHeight         = new Descriptor { };
        private Descriptor playerWeight         = new Descriptor { };
        private Descriptor playerAdressName     = new Descriptor { };
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

        public Descriptor Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public Descriptor Race
        {
            get => race;
            set
            {
                race = value;
                OnPropertyChanged(nameof(Race));
            }
        }
        public Descriptor Faith
        {
            get => faith;
            set
            {
                faith = value;
                OnPropertyChanged(nameof(Faith));
            }
        }
        public Descriptor PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }
        public Descriptor PlayerAdressName
        {
            get => playerAdressName;
            set
            {
                playerAdressName = value;
                OnPropertyChanged(nameof(PlayerAdressName));
            }
        }
        public Descriptor BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public Descriptor CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        public Descriptor Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public Descriptor EyeColor
        {
            get => eyeColor;
            set
            {
                eyeColor = value;
                OnPropertyChanged(nameof(EyeColor));
            }
        }
        public Descriptor HairColor
        {
            get => hairColor;
            set
            {
                hairColor = value;
                OnPropertyChanged(nameof(HairColor));
            }
        }
        public Descriptor SkinColor
        {
            get => skinColor;
            set
            {
                skinColor = value;
                OnPropertyChanged(nameof(SkinColor));
            }
        }
        public Descriptor PlayerHeight
        {
            get => playerHeight;
            set
            {
                playerHeight = value;
                OnPropertyChanged(nameof(PlayerHeight));
            }
        }
        public Descriptor PlayerWeight
        {
            get => playerWeight;
            set
            {
                playerWeight = value;
                OnPropertyChanged(nameof(PlayerWeight));
            }
        }
        public Descriptor Culture
        {
            get => culture;
            set
            {
                culture = value;
                OnPropertyChanged(nameof(Culture));
            }
        }
        public Descriptor Profession
        {
            get => profession;
            set
            {
                profession = value;
                OnPropertyChanged(nameof(Profession));
            }
        }
        public Descriptor Familistatus
        {
            get => familyStatus;
            set
            {
                familyStatus = value;
                OnPropertyChanged(nameof(Familistatus));
            }
        }
        #endregion
    }
}
