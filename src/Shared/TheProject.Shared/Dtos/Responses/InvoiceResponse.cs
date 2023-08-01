namespace TheProject.Shared.Dtos.Responses;

public class InvoiceResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }
    public double Discount { get; set; }
    public double Total { get; set; }
    public double TotalWithDiscount { get; set; }
    public IEnumerable<InvoiceProductResponse> InvoiceProducts { get; set; }
}
