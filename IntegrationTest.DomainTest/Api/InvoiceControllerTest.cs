using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Helpers;
using IntegrationTest.DomainTest.Attributes;
using IntegrationTest.DomainTest.Factories;
using IntegrationTest.DomainTest.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using static IntegrationTest.Domain.Commands.Inputs.CreateInvoiceCommand;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;
using static IntegrationTest.Domain.Commands.Results.ProductCommandResults;

namespace IntegrationTest.DomainTest.Api
{
    [Collection("API")]
    [TestCaseOrderer("IntegrationTest.DomainTest.PriorityOrderer", "IntegrationTest.DomainTest")]

    public class InvoiceControllerTest : AbstractIntegrationTest
    {
        public InvoiceControllerTest(ITestOutputHelper output, TestServerFixture testServerFixture)
        : base(output, testServerFixture)
        {
        }

        public Product CreatedProduct { get; private set; }

        [Fact]
        [TestPriority(1)]
        public async void Create_a_valid_Invoice()
        {
            //Arrange
            IList<CreateProductResult> productResults = new List<CreateProductResult>();
            using var scope = Server.Host.Services.CreateScope();
            var productCommands = scope.ServiceProvider.GetRequiredService<ProductCommands>();

            foreach (var createProductcommand in CreateProdutCommands())
            {
                var result = await productCommands.Handle(createProductcommand, CancellationToken.None);
                productResults.Add(result.Object as CreateProductResult);
            }
            var command = new CreateInvoiceCommand
            {
                CustomerId = Guid.NewGuid(),
                Discount = 0.1,
                Items = productResults.Select(p => new ItemsCommand
                {
                    ProductId = p.Id,
                    Quantity = 2
                }).ToList()
            };
            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            //Act
            var sut = await Client.PostAsync("/api/invoices/v1", content);
            sut.EnsureSuccessStatusCode();
            var responseText = await sut.Content.ReadAsStringAsync();


            var settings = new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            var serializer = JsonSerializer.Create(settings);

            var dynObj = (JObject)JsonConvert.DeserializeObject(responseText);

            WriteLine(responseText);


            var assert = await Client.GetAsync($"/api/invoices/v1/{dynObj["object"]["id"]}");
            sut.EnsureSuccessStatusCode();
            var responseAssert = await sut.Content.ReadAsStringAsync();

            var dynAssertObj = (JObject)JsonConvert.DeserializeObject(responseText);



            Assert.True(bool.Parse(dynObj.GetValue("success").ToString()));
            Assert.Equal(dynObj["object"]["id"], dynAssertObj["object"]["id"]);
        }

        private IEnumerable<CreateProductCommand> CreateProdutCommands()
        {
            yield return new CreateProductCommand
            {
                Brand = "Kitchen stuff +",
                Name = "Knife",
                Price = 50
            };

            yield return new CreateProductCommand
            {
                Brand = "Kitchen stuff +",
                Name = "Fork",
                Price = 30
            };
            yield return new CreateProductCommand
            {
                Brand = "Kitchen stuff +",
                Name = "Spoon",
                Price = 20
            };
            yield return new CreateProductCommand
            {
                Brand = "Coke",
                Name = "Fanta"
            };

            yield return new CreateProductCommand
            {
                Brand = "Coke",
                Name = "Guarana"
            };
        }

    }
}
