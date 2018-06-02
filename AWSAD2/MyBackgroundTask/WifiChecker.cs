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
    public sealed class WifiChecker : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            // do something.
            UpdateInfo();
            _deferral.Complete();
        }

        private void UpdateInfo()
        {
            var networkInfo = Windows.Networking.Connectivity.NetworkInformation.GetConnectionProfiles();
            string msg = "";
            if (networkInfo != null)
                msg = "connected to internet success";
            else
                msg = "no internet found";
            XmlDocument xdoc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);
            xdoc.GetElementsByTagName("text")[0].InnerText = msg;
            TileNotification notification = new TileNotification(xdoc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
