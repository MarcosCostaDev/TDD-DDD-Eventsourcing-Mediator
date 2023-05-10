using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheProject.Core.Command;

namespace TheProject.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : CommandEvent;
        Task SendCommandAsync(IRequest command, CancellationToken cancellationToken = default);
        Task<TResponse> SendCommandAsync<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default);
    }
}
