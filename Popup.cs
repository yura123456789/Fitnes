using Fitnes_Gump.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulpep.NotificationWindow;

namespace Fitnes_Gump
{
    public class Popup
    {
        public static void Popup_pop()
        {
            PopupNotifier popup = new PopupNotifier();            
            popup.TitleText = "Уведомление";
            popup.ContentText = "Успешно";
            popup.Image = Resources.Галка;
            popup.ImageSize = new System.Drawing.Size(50, 50);
            popup.Size = new System.Drawing.Size(250,70);
            popup.Popup();
        }
        public static void Popup_error()
        {
            Exception exception = new Exception();
            PopupNotifier notifier = new PopupNotifier();
            notifier.TitleText = "Уведомление";
            notifier.ContentText = exception.Message;
            notifier.Image = Resources.Error;
            notifier.ImageSize = new System.Drawing.Size(50, 50);
            notifier.Size = new System.Drawing.Size(250, 70);
            notifier.Popup();
        }

    }
}
