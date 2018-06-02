using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace MyBackgroundTask
{
    class TimerTriger : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral = null;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            UpdateTime();
            _deferral.Complete();
        }

        private void UpdateTime()
        {
            string str = DateTime.Now.ToString("hh:mm:ss");
            XmlDocument xdoc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
            xdoc.GetElementsByTagName("text")[0].InnerText = str;
            TileNotification notifi = new TileNotification(xdoc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notifi);
        }
    }
}
