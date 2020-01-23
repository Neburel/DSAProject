using DSAProject.util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.utilPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TaWItem : Page
    {
        #region Variables
        private TawItem_ViewModel viewModel = new TawItem_ViewModel();
        #endregion
        #region Dependency
        public static readonly DependencyProperty TAWProperty       = DependencyProperty.Register(nameof(TAW), typeof(int), typeof(TaWItem), new PropertyMetadata(null, new PropertyChangedCallback(OnAKTValueChanged)));
        public static readonly DependencyProperty TAW2Property      = DependencyProperty.Register(nameof(TAW2), typeof(string), typeof(TaWItem), new PropertyMetadata(null, new PropertyChangedCallback(OnToolTipValueChanged)));
        #endregion
        #region Dependency Properties
        public int TAW
        {
            get => (int)GetValue(TAWProperty);
            set => SetValue(TAWProperty, value);
        }
        public string TAW2
        {
            get => (string)GetValue(TAW2Property);
            set
            {
                SetValue(TAW2Property, value);
                SetTooltip(value);
            }
        }
        #endregion
        public TaWItem()
        {
            this.InitializeComponent();
            viewModel.PropertyChanged += (s, o) =>
            {
                if(o.PropertyName == nameof(TAW))
                {
                    TAW = viewModel.TAW;
                }
            };
        }
        #region Propertie Event 
        private static void OnAKTValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TaWItem)d).viewModel.TAW = (int)e.NewValue;
            }
        }
        private static void OnToolTipValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((TaWItem)d).viewModel.ToolTip = (string)e.NewValue;                
            }
        }
        #endregion
        #region Events
        private void XAML_PlusButton_Clicked(object sender, object e)
        {
            viewModel.TAW++;
        }
        private void XAML_MinusButton_Clicked(object sender, object e)
        {
            viewModel.TAW--;
        }
        #endregion

        public void SetTooltip(string toolTipText)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.Content = toolTipText;
            ToolTipService.SetToolTip(XAML_TextBlock, toolTip);
        }

        private class TawItem_ViewModel : AbstractPropertyChanged
        {
            public int TAW
            {
                get => Get<int>();
                set => Set(value);
            }
            public string ToolTip
            {
                get => Get<string>();
                set => Set(value);
            }
        }
    }
}
