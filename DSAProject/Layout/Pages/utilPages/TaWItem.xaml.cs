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
        public static readonly DependencyProperty TAWProperty = DependencyProperty.Register(nameof(TAW), typeof(int), typeof(TaWItem), new PropertyMetadata(null, new PropertyChangedCallback(OnAKTValueChanged)));
        #endregion
        #region Dependency Properties
        public int TAW
        {
            get => (int)GetValue(TAWProperty);
            set => SetValue(TAWProperty, value);
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

        private class TawItem_ViewModel : AbstractPropertyChanged
        {
            private int taw = 0;

            public int TAW 
            {
                get => taw;
                set
                {
                    if(taw != value)
                    {
                        taw = value;
                        OnPropertyChanged(nameof(TAW));
                    }
                }
            } 
        }
    }
}
