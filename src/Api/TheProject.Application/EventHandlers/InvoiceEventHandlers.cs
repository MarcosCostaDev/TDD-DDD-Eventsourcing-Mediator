using MediatR;
using Microsoft.Extensions.Logging;
using TheProject.Domain.Events;

namespace TheProject.Application.EventHandlers;

public class InvoiceEventHandlers : INotificationHandler<InvoiceCreatedEvent>
{
    private readonly ILogger<InvoiceEventHandlers> _logger;

    public InvoiceEventHandlers(ILogger<InvoiceEventHandlers> logger)
    {
        _logger = logger;
    }
    public Task Handle(InvoiceCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"The invoice {notification.InvoiceId} was created for Customer {notification.CustomerId}");
        return Task.CompletedTask;
    }

}
