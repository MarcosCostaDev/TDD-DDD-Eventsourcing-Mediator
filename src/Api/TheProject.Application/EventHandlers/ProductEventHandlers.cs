using MediatR;
using Microsoft.Extensions.Logging;
using TheProject.Domain.Events;

namespace TheProject.Application.EventHandlers;

public class ProductEventHandlers : INotificationHandler<CreatedProductEvent>
{
    private readonly ILogger<ProductEventHandlers> _logger;

    public ProductEventHandlers(ILogger<ProductEventHandlers> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreatedProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"The product {notification.ProductId} was created.");
        return Task.CompletedTask;
    }
}
