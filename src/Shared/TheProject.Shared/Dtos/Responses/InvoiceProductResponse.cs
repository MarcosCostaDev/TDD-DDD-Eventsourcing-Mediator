namespace TheProject.Shared.Dtos.Responses;

public class InvoiceProductResponse
{
    public Guid ProductId { get; set; }
    public Guid InvoiceId { get; set; }
    public double Quantity { get; set; }
    public InvoiceResponse Invoice { get; set; }
    public ProductResponse Product { get; set; }
}
