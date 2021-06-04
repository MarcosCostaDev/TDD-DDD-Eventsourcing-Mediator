using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.DomainTest.Factories;
using IntegrationTest.DomainTest.Fixtures;
using IntegrationTest.Infra.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.DomainTest.Commands
{
    [Collection("Integration tests collection")]
    public class ProductCommandsTest : AbstractIntegrationTest
    {
        
        public ProductCommandsTest(ITestOutputHelper output, TestServerFixture testServerFixture) 
            : base(output, testServerFixture)
        {
        }

        [Fact]
        public async Task CreateProductAsync()
        {
            using var scope = Server.Host.Services.CreateScope();

            var sut = scope.ServiceProvider.GetRequiredService<ProductCommands>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var command = new CreateProductCommand
            {
                Brand = "Coke",
                Name = "Fork"
            };
            var result = await sut.CreateProductAsync(command);
            await uow.CommitAsync();
            AddNotifications(result);
            Assert.True(result.IsValid);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Brand, result.Brand);
        }
    }
}
