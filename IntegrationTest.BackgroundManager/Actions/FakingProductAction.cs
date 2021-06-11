using Hangfire;
using IntegrationTest.Core.Bus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.BackgroundManager.Actions
{
    public interface IFakingProductAction
    {
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        Task GenerateProducts();
    }
    public class FakingProductAction : IFakingProductAction
    {
        private readonly ILogger<FakingInvoiceAction> _logger;
        private readonly IMediatorHandler _mediator;

        public FakingProductAction(ILogger<FakingInvoiceAction> logger, IMediatorHandler mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task GenerateProducts()
        {
            var random = new Random();
            var command = new CreateProductCommand
            {
                Brand = Faker.Lorem.Words(1).FirstOrDefault(),
                Name = Faker.Lorem.Words(30).ElementAt(random.Next(0, 29)),
                Price = Faker.RandomNumber.Next(1, 100)
            };

            var commandResult = await _mediator.SendCommandAsync(command);

            if (!commandResult.IsValid)
            {
                var messages = string.Join(Environment.NewLine, commandResult.Notifications.Select(p => p.Message));
                var message = $"Product was not created. Reasons: {messages}";
                _logger.LogWarning(message);
                throw new InvalidOperationException(message);
               
            }
        }
    }
}
