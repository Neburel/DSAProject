using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.BasePages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        public string AppPAth { get; set; } = ApplicationData.Current.LocalFolder.Path;
        public string AppSavePAth { get; set; } = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Save", "Data");

        public InfoPage()
        {
            this.InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var task = new Task(async () =>
            {
                var storage = await StorageFolder.GetFolderFromPathAsync(AppSavePAth);
                await Windows.System.Launcher.LaunchFolderAsync(storage);
            });
            task.Start();
        }
    }
}
