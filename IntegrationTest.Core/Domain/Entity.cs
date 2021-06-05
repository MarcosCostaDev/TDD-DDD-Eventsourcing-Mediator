using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Domain
{
    public abstract class Entity : Notifiable<Notification>
    {
    }
}
