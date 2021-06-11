using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Command
{
    public class CommandRequest : IRequest<CommandResult>
    {
    }
}
