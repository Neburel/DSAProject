using DSAProject.util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TextBorderBlock : Page
    {
        #region Variables
        private TextBorderBlockViewModel viewModel = new TextBorderBlockViewModel();
        #endregion
        #region Dependency
        public static readonly DependencyProperty TextProperty          = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBorderBlock), new PropertyMetadata(null, new PropertyChangedCallback(OnTextValueChanged)));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(TextBorderBlock), new PropertyMetadata(null, new PropertyChangedCallback(OnTextAlignmentValueChanged)));
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
        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set
            {
                SetValue(TextAlignmentProperty, value);
            }
        }
        #endregion
        #region Propertie Event 
        private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TextBorderBlock)d).viewModel.Text = (string)e.NewValue;
            }
        }
        private static void OnTextAlignmentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TextBorderBlock)d).viewModel.TextAlignment = (TextAlignment)e.NewValue;
            }
        }
        #endregion
        public TextBorderBlock()
        {
            this.InitializeComponent();
        }
        private class TextBorderBlockViewModel : AbstractPropertyChanged
        {
            public string Text
            {
                get => Get<string>();
                set => Set(value);
            }
            public TextAlignment TextAlignment
            {
                get => Get<TextAlignment>();
                set => Set(value);
            }
        }
    }
}
