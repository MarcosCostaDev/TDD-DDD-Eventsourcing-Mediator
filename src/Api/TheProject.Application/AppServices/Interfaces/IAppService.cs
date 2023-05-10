using Flunt.Notifications;

namespace TheProject.Application.AppServices.Interfaces;

public interface IAppService
{
    bool IsValid { get; }
    IReadOnlyCollection<Notification> Notifications { get; }
}
