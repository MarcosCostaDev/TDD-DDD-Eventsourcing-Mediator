using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Helpers;
using IntegrationTest.DomainTest.Attributes;
using IntegrationTest.DomainTest.Factories;
using IntegrationTest.DomainTest.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.DomainTest.Api
{
    [Collection("API")]
    [TestCaseOrderer("IntegrationTest.DomainTest.PriorityOrderer", "IntegrationTest.DomainTest")]
    public class ProductControllerTest : AbstractIntegrationTest
    {
        public ProductControllerTest(ITestOutputHelper output, TestServerFixture testServerFixture)
         : base(output, testServerFixture)
        {
        }


        public static Product CreatedProduct { get; set; }

        [Fact]
        [TestPriority(1)]
        public async void ApiHealth()
        {
            var response = await Client.GetAsync("/api/health");
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();

            WriteLine(responseText);
        }

        [Fact]
        [TestPriority(2)]
        public async void Create_a_valid_product()
        {
            var command = new CreateProductCommand
            {
                Brand = "Coke",
                Name = "Fork",
                Price = 10
            };

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/api/products/v1", content);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();


            var settings = new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            var serializer = JsonSerializer.Create(settings);         

            var dynObj = (JObject)JsonConvert.DeserializeObject(responseText);
            CreatedProduct = dynObj.GetValue("object").ToObject<Product>(serializer);
            WriteLine(responseText);
            Assert.True(bool.Parse(dynObj.GetValue("success").ToString()));
        }


        [Fact]
        [TestPriority(3)]
        public async void Update_a_valid_product()
        {
            var command = new UpdateProductCommand
            {
                Id = CreatedProduct.Id,
                Brand = "UPS",
                Name = "Package",
                Price = 20
            };

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync("/api/products/v1", content);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            var serializer = JsonSerializer.Create(settings);

            var dynObj = (JObject)JsonConvert.DeserializeObject(responseText);
            CreatedProduct = dynObj.GetValue("object").ToObject<Product>(serializer);
            WriteLine(responseText);
        }

        [Fact]
        [TestPriority(4)]
        public async void GetProduct()
        {
            var response = await Client.GetAsync($"/api/products/v1/{CreatedProduct.Id}");
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();

            WriteLine(responseText);
        }


    }
}
