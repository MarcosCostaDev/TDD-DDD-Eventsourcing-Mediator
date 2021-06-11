using Hangfire;
using IntegrationTest.Core.Bus;
using IntegrationTest.Core.Extensions;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.BackgroundManager.Actions
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

            var command = new CreateInvoiceCommand
            {
                CustomerId = Guid.NewGuid(),
                Discount = random.NextDoubleRange(0.0, 0.09)
            };
            var quantityProducts = random.Next(1, 10);
            command.Items = new List<CreateInvoiceCommand.ItemsCommand>();
            for (int i = 0; i < quantityProducts; i++)
            {
                var selectedProduct = random.Next(0, products.Count - 1);
                command.Items.Add(new CreateInvoiceCommand.ItemsCommand
                {
                    ProductId = products[selectedProduct].Id,
                    Quantity = random.Next(1, 20)
                });
            }

            var commandResult = await _mediator.SendCommandAsync(command);

            if(!commandResult.IsValid)
            {
                var messages = string.Join(Environment.NewLine, commandResult.Notifications.Select(p => p.Message));
                var message = $"Invoice was not created. Reasons: {messages}";
                _logger.LogWarning(message);
                throw new InvalidOperationException(message);
            } 
            


        }
    }
}
