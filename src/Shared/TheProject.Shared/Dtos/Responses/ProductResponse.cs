namespace TheProject.Shared.Dtos.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Price { get; set; }
    public IEnumerable<InvoiceProductResponse> InvoiceProducts { get; set; }
}
