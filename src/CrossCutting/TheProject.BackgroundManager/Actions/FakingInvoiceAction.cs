using Flunt.Notifications;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheProject.Core.Bus;
using TheProject.Core.Command;
using TheProject.Core.Extensions;
using TheProject.Domain.Repositories;
using TheProject.Shared.Dtos.Requests;

namespace TheProject.BackgroundManager.Actions
{
    public interface IFakingInvoiceAction
    {
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        Task GenerateInvoices();
    }
    public class FakingInvoiceAction : IFakingInvoiceAction
    {
        private readonly ILogger<FakingInvoiceAction> _logger;
        private readonly IMediatorHandler _mediator;
        private readonly IProductRepository _productRepository;

        public FakingInvoiceAction(ILogger<FakingInvoiceAction> logger, IMediatorHandler mediator, IProductRepository productRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task GenerateInvoices()
        {
            var random = new Random();
            var products = await _productRepository.ListAllAsync();

            var request = new InvoiceRequest
            {
                CustomerId = Guid.NewGuid(),
                Discount = random.NextDoubleRange(0.0, 0.09)
            };
            var quantityProducts = random.Next(1, 10);

            var items = new List<ItemRequest>();

            for (int i = 0; i < quantityProducts; i++)
            {
                var selectedProduct = random.Next(0, products.Count - 1);
                items.Add(new ItemRequest
                {
                    ProductId = products[selectedProduct].Id,
                    Quantity = random.Next(1, 20)
                });
            }
            request.Items = items;

            var notifications = await _mediator.SendCommandAsync(new NotifiableCommandRequest<InvoiceRequest>(request));

            if (!notifications.Any()) return;

            var messages = string.Join(Environment.NewLine, notifications.Select(p => p.Message));
            var message = $"Invoice was not created. Reasons: {messages}";
            _logger.LogWarning(message);
            throw new InvalidOperationException(message);
        }
    }
}
