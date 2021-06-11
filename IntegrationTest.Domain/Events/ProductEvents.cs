using IntegrationTest.Core.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Events.ProductEvents;

namespace IntegrationTest.Domain.Events
{
    public class ProductEvents : INotificationHandler<CreatedProductEvent>
    {
        private readonly ILogger<InvoiceEvents> _logger;

        public ProductEvents(ILogger<InvoiceEvents> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreatedProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The product {notification.ProductId} was created.");
            return Task.CompletedTask;
        }

        public class CreatedProductEvent : CommandEvent
        {
            public Guid ProductId { get; set; }
        }
    }
}
