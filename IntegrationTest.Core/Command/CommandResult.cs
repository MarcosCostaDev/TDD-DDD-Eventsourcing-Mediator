using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Command
{
    public sealed class CommandResult : Notifiable<Notification>
    {
        public CommandResult(object data)
        {

            if (data is Notifiable<Notification>)
            {
                AddNotifications(data as Notifiable<Notification>);
            }

            Object = data;
        }

        public CommandResult(object data, Notifiable<Notification> notifiable)
        {
            Object = data;
            AddNotifications(notifiable);
        }

        public CommandResult(object data, IEnumerable<Notification> notifications)
        {
            Object = data;
            notifications.ToList().ForEach(n => AddNotification(n));
        }
        public bool Success => IsValid;
        public object Object { get; private set; }
    }
}
