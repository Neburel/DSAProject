using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DSAProject.Layout.MessageDialoge
{

    public static class InvestAPDialog_
    {
        public enum InvestAPResult
        {
            Abort,
            Add,
            Remove
        }

        public static async Task<bool> ShowDialog(bool invest)
        {
            string title = string.Empty;
            string message = string.Empty;

            if (invest)
            {
                title = "Investierte Abenteuerpunte";
                message = "Investiere deine Abenteuerpunkte!";
            }
            else
            {
                title = "Aktuelle Abenteuerpunkte";
                message = "Füge weitere Abenteuerpunkte hinzu";
            }
            var dialog = new MessageDialog(message, title);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Abbrechen") { Id = InvestAPResult.Abort });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Entfernen") { Id = InvestAPResult.Remove });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Hinzufügen") { Id = InvestAPResult.Add });

            var result = await dialog.ShowAsync();
            var resultCode = (InvestAPResult)result.Id;



            if ((int)result.Id == 0) { return true; }
            return false;
        }
        public class InvestApResulg
        {

        }
    }
}

