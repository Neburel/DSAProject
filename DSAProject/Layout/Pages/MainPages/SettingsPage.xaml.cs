using DSAProject.Classes.Game;
using DSAProject.util;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.BasePages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SettingsPageViewModel viewModel = new SettingsPageViewModel();
        public string AppPAth { get; set; } = ApplicationData.Current.LocalFolder.Path;
        public string AppSavePAth { get; set; } = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Save", "Data");

        public SettingsPage()
        {
            this.InitializeComponent();
            viewModel.AutoDeduction     = Game.Settings.AutoDeduction;
            viewModel.SortAlpabetical   = Game.Settings.OrderTalentAlphabetic;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SettingsPageViewModel.SortAlpabetical))
                {
                    Game.Settings.OrderTalentAlphabetic = viewModel.SortAlpabetical;
                }
                else if (args.PropertyName == nameof(SettingsPageViewModel.AutoDeduction))
                {
                    Game.Settings.AutoDeduction = viewModel.AutoDeduction;
                }
            };
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var task = new Task(async () =>
            {
                try
                {
                    var storage = await StorageFolder.GetFolderFromPathAsync(AppSavePAth);
                    await Windows.System.Launcher.LaunchFolderAsync(storage);
                }
                catch (Exception)
                {
                    var storage2 = await StorageFolder.GetFolderFromPathAsync(AppPAth);
                    await Windows.System.Launcher.LaunchFolderAsync(storage2);
                }
            });
            task.Start();
        }
        public class SettingsPageViewModel : AbstractPropertyChanged
        {
            public bool SortAlpabetical
            {
                get => Get<bool>();
                set => Set(value);
            }
            public bool AutoDeduction
            {
                get => Get<bool>();
                set => Set(value);
            }
        }
    }
}
