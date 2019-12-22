using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DSAProject.Layout.MessageDialoge
{
    public static class CreateTraitDialog
    {
        

        public static async Task<bool> ShowDialog(bool traitExist = false)
        {
            var sem = new SemaphoreSlim(0);


            var title   = "Trait erstellen?";
            var message = "Was soll mit dem Trait Geschehen?";

            var dialog = new MessageDialog(message, title);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Erstellen") { Id = 0 });
            if (traitExist)
            {
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Löschen") { Id = 1 });
            }
           dialog.Commands.Add(new Windows.UI.Popups.UICommand("Verwerfen") { Id = 1 });

            var result = await dialog.ShowAsync();
            if((int)result.Id == 0) { return true; }
            return false;
        }
    }
}
