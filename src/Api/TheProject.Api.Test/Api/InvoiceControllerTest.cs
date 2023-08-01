using TheProject.Api.Test;
using TheProject.Api.Test.Abstracts;
using TheProject.Api.Test.Extensions;

namespace TheProject.Test.Api
{
    [Collection("API")]

    public class InvoiceControllerTest : AbstractIntegrationTest
    {
        public InvoiceControllerTest(ITestOutputHelper output, StartupFixture startupFixture)
        : base(output, startupFixture)
        {
        }

        public Product CreatedProduct { get; private set; }


        [Fact]
        public async void CreateInvoiceReturnsSuccess()
        {
            // arrange
            var product01Request = new ProductRequest
            {
                Brand = "Brand01",
                Name = "Fork",
                Price = 10
            };
            var product02Request = new ProductRequest
            {
                Brand = "Brand02",
                Name = "Knife",
                Price = 1
            };
            var product03Request = new ProductRequest
            {
                Brand = "Brand03",
                Name = "Spoon",
                Price = 10
            };

            var product01response = await Client.PostAsync("/api/products/v1", GetJsonContent(product01Request));
            var product02response = await Client.PostAsync("/api/products/v1", GetJsonContent(product02Request));
            var product03response = await Client.PostAsync("/api/products/v1", GetJsonContent(product03Request));

            product01response.EnsureSuccessStatusCode();
            product02response.EnsureSuccessStatusCode();
            product03response.EnsureSuccessStatusCode();

            var product01Text = await product01response.Content.ReadAsStringAsync();
            var product02Text = await product02response.Content.ReadAsStringAsync();
            var product03Text = await product03response.Content.ReadAsStringAsync();

            // act
            var request = new InvoiceRequest
            {
                CustomerId = Guid.NewGuid(),
                Discount = 0.1,
                Items = new List<ItemRequest>
                {
                    new ItemRequest{ ProductId =  product01Text.SerializeApiResponseTo<ProductResponse>().Id, Quantity = 10 },
                    new ItemRequest{ ProductId =  product02Text.SerializeApiResponseTo<ProductResponse>().Id, Quantity = 20 },
                    new ItemRequest{ ProductId =  product03Text.SerializeApiResponseTo<ProductResponse>().Id, Quantity = 30 }
                }
            };

            var response = await Client.PostAsync("/api/invoices/v1", GetJsonContent(request));
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            var sut = responseText.SerializeApiResponseTo<InvoiceResponse>();

            sut.TotalWithDiscount.Should().Be(18.9);
            sut.Total.Should().Be(21);
            sut.CustomerId.Should().Be(request.CustomerId);
            sut.Discount.Should().Be(request.Discount);
            sut.InvoiceProducts.Should().HaveCount(3);
            sut.InvoiceProducts.ElementAt(0).Quantity.Should().Be(request.Items.ElementAt(0).Quantity);
            sut.InvoiceProducts.ElementAt(0).ProductId.Should().Be(request.Items.ElementAt(0).ProductId);
            sut.InvoiceProducts.ElementAt(1).Quantity.Should().Be(request.Items.ElementAt(1).Quantity);
            sut.InvoiceProducts.ElementAt(1).ProductId.Should().Be(request.Items.ElementAt(1).ProductId);
            sut.InvoiceProducts.ElementAt(2).Quantity.Should().Be(request.Items.ElementAt(2).Quantity);
            sut.InvoiceProducts.ElementAt(2).ProductId.Should().Be(request.Items.ElementAt(2).ProductId);
        }

        private IEnumerable<ProductRequest> CreateProdutCommands()
        {
            yield return new ProductRequest
            {
                Brand = "Kitchen stuff +",
                Name = "Knife",
                Price = 50
            };

            yield return new ProductRequest
            {
                Brand = "Kitchen stuff +",
                Name = "Fork",
                Price = 30
            };
            yield return new ProductRequest
            {
                Brand = "Kitchen stuff +",
                Name = "Spoon",
                Price = 20
            };
            yield return new ProductRequest
            {
                Brand = "Coke",
                Name = "Fanta"
            };

            yield return new ProductRequest
            {
                Brand = "Coke",
                Name = "Guarana"
            };
        }

    }
}
