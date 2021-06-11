using IntegrationTest.Core.Command;
using IntegrationTest.Core.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Events.InvoiceEvents;

namespace IntegrationTest.Domain.Events
{
    public class InvoiceEvents : INotificationHandler<InvoiceCreatedEvent>
    {
        private readonly ILogger<InvoiceEvents> _logger;

        public InvoiceEvents(ILogger<InvoiceEvents> logger)
        {
            _logger = logger;
        }
        public Task Handle(InvoiceCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The invoice {notification.InvoiceId} was created for Customer {notification.CustomerId}");
            return Task.CompletedTask;
        }

        public class InvoiceCreatedEvent : CommandEvent
        {
            public Guid InvoiceId { get; set; }
            public Guid CustomerId { get; internal set; }
        }
    }
}
