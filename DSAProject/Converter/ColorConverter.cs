using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace DSAProject.Converter
{
    public class ColorConverter : IValueConverter
    {
        public static SolidColorBrush SolidColorBrush { get; set; } = new SolidColorBrush(Windows.UI.Colors.Black);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return SolidColorBrush;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return SolidColorBrush;
        }
    }
}
