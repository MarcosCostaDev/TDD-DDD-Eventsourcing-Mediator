using MediatR;
using TheProject.Core.Messaging;

namespace TheProject.Core.Command;

public class CommandEvent : Event, INotification
{
}
