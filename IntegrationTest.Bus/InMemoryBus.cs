using IntegrationTest.Core.Bus;
using IntegrationTest.Core.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEventAsync<T>(T @event) where T : CommandEvent
        {
            await _mediator.Publish(@event);
        }

        public async Task<CommandResult> SendCommandAsync<T>(T command) where T : CommandRequest
        {
           return await _mediator.Send(command);
        }
    }
}
