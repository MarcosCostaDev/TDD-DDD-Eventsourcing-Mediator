using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace IntegrationTest.DomainTest
{
    public class BaseTest : IWriter
    {
        protected readonly ITestOutputHelper Output;
        public BaseTest(ITestOutputHelper output)
        {
            Output = output;
        }

        public void WriteLine(string text)
        {
            Output.WriteLine(text);
        }

        public void AddNotifications(Notifiable<Notification> notifiable)
        {
            foreach (var notification in notifiable.Notifications)
            {
                WriteLine($"{notification.Key} - {notification.Message}");
            } 
        }
    }


    public interface IWriter
    {
        void WriteLine(string text);

        void AddNotifications(Notifiable<Notification> notifiable);
    }
}
