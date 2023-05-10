using Flunt.Notifications;
using Xunit.Abstractions;

namespace TheProject.Api.Test.Abstracts;

public class AbstractTest : IWriter
{
    protected readonly ITestOutputHelper Output;
    public AbstractTest(ITestOutputHelper output)
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
