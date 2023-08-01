using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheProject.Core.Bus;
using TheProject.Core.Command;
using TheProject.Core.Messaging;

namespace TheProject.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : CommandEvent
        {
            return _mediator.Publish(@event, cancellationToken);
        }

        public Task SendCommandAsync(IRequest command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task<TResponse> SendCommandAsync<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}
