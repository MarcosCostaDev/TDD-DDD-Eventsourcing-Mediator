using Hangfire;
using IntegrationTest.Core.Bus;
using IntegrationTest.Core.Extensions;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repositories;
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
        private readonly IMediatorHandler _mediator;
        private readonly IProductRepository _productRepository;

        public FakingInvoiceAction(IMediatorHandler mediator, IProductRepository productRepository)
        {
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
                Discount = random.NextDoubleRange(0.0, 0.1)
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

            await _mediator.SendCommandAsync(command);
            


        }
    }
}
