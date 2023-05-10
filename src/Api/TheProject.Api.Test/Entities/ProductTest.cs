using TheProject.Api.Test.Abstracts;

namespace TheProject.Test.Entities;

public class ProductTest : AbstractTest
{
    public ProductTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData("", "", -3)]
    [InlineData("spoon", "", 2)]
    [InlineData("", "knife", 1)]
    [InlineData("spoon", "knife", -30)]
    public void Product_returnsFalse(string name, string brand, double price)
    {
        var sut = new Product(name, brand, price);

        Assert.False(sut.IsValid);
        Assert.Equal(name, sut.Name);
        Assert.Equal(brand, sut.Brand);
        Assert.Equal(price, sut.Price);
    }

    [Theory]
    [InlineData("knife", "brand-ten", 4)]
    [InlineData("spoon", "maximun", 10)]
    [InlineData("spoon", "underscore", 40)]
    public void Product_returnsTrue(string name, string brand, double price)
    {
        var sut = new Product(name, brand, price);

        Assert.True(sut.IsValid);
        Assert.Equal(name, sut.Name);
        Assert.Equal(brand, sut.Brand);
        Assert.Equal(price, sut.Price);
    }


}
