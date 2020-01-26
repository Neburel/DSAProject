using DSAProject.util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TextBorderBlockWarning : Page
    {
        #region Variables
        private TextBorderWarningBlockViewModel viewModel = new TextBorderWarningBlockViewModel();
        #endregion
        #region Dependency
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBorderBlock), new PropertyMetadata(null, new PropertyChangedCallback(OnTextValueChanged)));
        public static readonly DependencyProperty TextWarningProperty = DependencyProperty.Register(nameof(WarningText), typeof(string), typeof(TextBorderBlock), new PropertyMetadata(null, new PropertyChangedCallback(OnTextWarningValueChanged)));
        #endregion
        #region Dependency Properties
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public string WarningText
        {
            get => (string)GetValue(TextWarningProperty);
            set
            {
                SetValue(TextWarningProperty, value);
            }
        }
        #endregion
        #region Propertie Event 
        private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TextBorderBlockWarning)d).viewModel.Text = (string)e.NewValue;
            }
        }
        private static void OnTextWarningValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TextBorderBlockWarning)d).viewModel.TextWarning = (string)e.NewValue;
            }
        }
        #endregion
        public TextBorderBlockWarning()
        {
            this.InitializeComponent();
        }
        private class TextBorderWarningBlockViewModel : AbstractPropertyChanged
        {
            public string Text
            {
                get => Get<string>();
                set => Set(value);
            }
            public string TextWarning
            {
                get => Get<string>();
                set => Set(value);
            }
        }
    }
}
