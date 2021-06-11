using IntegrationTest.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T @event) where T : CommandEvent;
        Task<CommandResult> SendCommandAsync<T>(T command) where T : CommandRequest;
    }
}
