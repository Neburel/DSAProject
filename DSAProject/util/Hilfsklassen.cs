using DSAProject.Classes.Interfaces;
using Windows.UI.Xaml.Media;

namespace DSAProject.util
{
    public class Hilfsklassen
    {
        public class TraitTalentBonus : AbstractPropertyChanged
        {
            public int Value { get; set; }
            public ITalent Talent { get; set; }
            public SolidColorBrush TextColor { get; set; } = new SolidColorBrush(Windows.UI.Colors.Yellow);
        }
    }
}
