using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Project_Notifications.Notifications
{
    public class NotificationEventArgs: EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        
    }
}
