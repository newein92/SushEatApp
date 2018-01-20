using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SushEat.Droid
{
    class Constants
    {
        public const string ListenConnectionString = "Endpoint=sb://susheatnotificationshub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=Yq9cfjzjbASE/E+8g6raozOoik8A9aaDIq6fagPDuE8=";
        public const string NotificationHubName = "SushEatNotificationsHub";
    }
}