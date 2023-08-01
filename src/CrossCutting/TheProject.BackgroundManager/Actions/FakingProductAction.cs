using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheProject.Core.Bus;
using TheProject.Core.Command;
using TheProject.Shared.Dtos.Requests;
using TheProject.Shared.Dtos.Responses;

namespace TheProject.BackgroundManager.Actions
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
            var command = new ProductRequest
            {
                Brand = Faker.Lorem.Words(1).FirstOrDefault(),
                Name = Faker.Lorem.Words(30).ElementAt(random.Next(0, 29)),
                Price = Faker.RandomNumber.Next(1, 100)
            };

            var notifications = await _mediator.SendCommandAsync(new NotifiableCommandRequest<ProductRequest>(command));

            if (!notifications.Any()) return;

            var messages = string.Join(Environment.NewLine, notifications.Select(p => p.Message));
            var message = $"Product was not created. Reasons: {messages}";
            _logger.LogWarning(message);
            throw new InvalidOperationException(message);
        }
    }
}
