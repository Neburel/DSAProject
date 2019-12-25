using DSALib.Classes.JSON;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DSAProject.Layout.MessageDialoge
{
    public class DeleteCharakterDialog
    {
        public static async Task<bool> ShowDialog(JSON_Charakter jSON_Charakter)
        {
            var title   = "Charakter Löschen";
            var message = "Soll der Folgende Charakter wirklich gelöcht werden? \n" +
                " ID: " + jSON_Charakter.ID + " \n" +
                " Name: " + jSON_Charakter.Name + " \n" +
                " Letze Speicherung: " + jSON_Charakter.SaveTime + " \n";

            var dialog = new MessageDialog(message, title);

            dialog.Commands.Add(new UICommand("Abbrechen") { Id = 0 });
            dialog.Commands.Add(new UICommand("Löschen") { Id = 1 });

            var result = await dialog.ShowAsync();
            if ((int)result.Id == 0) { return false; }
            return true;
        }
    }
}
