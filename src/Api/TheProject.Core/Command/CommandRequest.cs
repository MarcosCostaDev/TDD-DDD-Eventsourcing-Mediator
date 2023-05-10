using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;

namespace TheProject.Core.Command;

public class CommandRequest<TRequest, TResponse> : IRequest<TResponse>
{
    public CommandRequest(TRequest payload)
    {
        Payload = payload;
    }

    public TRequest Payload { get; private set; }
}

public class NotifiableCommandRequest<TRequest> : CommandRequest<TRequest, IEnumerable<Notification>>
{
    public NotifiableCommandRequest(TRequest payload) : base(payload)
    {
    }
}
