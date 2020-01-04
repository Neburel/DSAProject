using DSALib.Charakter.Other;
using DSAProject.util;
using Windows.UI.Xaml.Media;

namespace DSAProject.Layout.Wrapper
{
    public class TraitWrapper : AbstractPropertyChanged
    {
        private bool apVisible;
        private Trait trait;
        private SolidColorBrush textColor = new SolidColorBrush(Windows.UI.Colors.Black);

        public bool APVisible
        {
            get => apVisible;
            set
            {
                apVisible = value;
                OnPropertyChanged(nameof(apVisible));
            }
        }
        public Trait Trait
        {
            get => trait;
            set
            {
                trait = value;
                OnPropertyChanged(nameof(Trait));
            }
        }
        public SolidColorBrush TextColor
        {
            get => textColor;
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }

    }
}
