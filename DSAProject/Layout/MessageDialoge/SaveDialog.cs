using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DSAProject.Layout.MessageDialoge
{
    public static class SaveDialog
    {
        public static async void ShowDialog(util.ErrrorManagment.Error error)
        {
            var title = "Speicher Dialog";
            var message = string.Empty;

            if (error == null)
            {
                message = "Das Speichern war erfolgreich";
            } 
            else
            {
                message = "Beim Speichern ist ein Fehler aufgetreten. Fehler: " + error.Message;
            }

            await new MessageDialog(message, title).ShowAsync();
        }
    }
}
