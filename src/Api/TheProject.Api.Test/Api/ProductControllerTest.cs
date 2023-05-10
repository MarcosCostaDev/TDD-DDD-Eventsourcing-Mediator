
using TheProject.Api.Test;
using TheProject.Api.Test.Abstracts;
using TheProject.Api.Test.Extensions;

namespace TheProject.Test.Api;

[Collection("API")]
public class ProductControllerTest : AbstractIntegrationTest
{
    public ProductControllerTest(ITestOutputHelper output, StartupFixture startupFixture)
     : base(output, startupFixture)
    {
    }

    

    [Fact]
    public async void CreateAValidProduct()
    {
        var request = new ProductRequest
        {
            Brand = "Coke",
            Name = "Fork",
            Price = 10
        };

        var response = await Client.PostAsync("/api/products/v1", GetJsonContent(request));
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

        var sut = responseText.SerializeApiResponseTo<Product>();

        sut.Brand.Should().Be(request.Brand);
        sut.Name.Should().Be(request.Name);
        sut.Price.Should().Be(request.Price);

    }


    [Fact]
    public async void Update_a_valid_product()
    {
        var product1 = new ProductRequest
        {
            Brand = "Coke",
            Name = "Fork",
            Price = 10
        };

        var response1 = await Client.PostAsync("/api/products/v1", GetJsonContent(product1));
        response1.EnsureSuccessStatusCode();
        var responseText = await response1.Content.ReadAsStringAsync();

        var createdProduct1 = responseText.SerializeApiResponseTo<Product>();

        var request = new ProductRequest
        {
            Brand = "UPS",
            Name = "Package",
            Price = 20
        };

        var response2 = await Client.PutAsync($"/api/products/v1/{createdProduct1.Id}", GetJsonContent(request));
        response2.EnsureSuccessStatusCode();
        var response2Text = await response2.Content.ReadAsStringAsync();

        var sut = response2Text.SerializeApiResponseTo<ProductResponse>();
        sut.Brand.Should().Be(request.Brand);
        sut.Name.Should().Be(request.Name);
        sut.Price.Should().Be(request.Price);
    }

    [Fact]
    public async void GetProduct()
    {
        var request = new ProductRequest
        {
            Brand = "Coke",
            Name = "Fork",
            Price = 10
        };

        var responseCreatedProduct = await Client.PostAsync("/api/products/v1", GetJsonContent(request));
        responseCreatedProduct.EnsureSuccessStatusCode();
        var responseCreatedProductText = await responseCreatedProduct.Content.ReadAsStringAsync();

        var createdProduct = responseCreatedProductText.SerializeApiResponseTo<Product>();

        var response = await Client.GetAsync($"/api/products/v1/{createdProduct.Id}");
        response.EnsureSuccessStatusCode();

        var responseText = await response.Content.ReadAsStringAsync();

        var sut = responseText.SerializeApiResponseTo<Product>();

        sut.Brand.Should().Be(request.Brand);
        sut.Name.Should().Be(request.Name);
        sut.Price.Should().Be(request.Price);

    }
}
