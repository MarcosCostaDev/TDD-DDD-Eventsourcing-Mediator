namespace TheProject.Shared.Dtos.Requests;

public class InvoiceRequest
{
    public Guid CustomerId { get; set; }
    public double Discount { get; set; }
    public IEnumerable<ItemRequest> Items { get; set; }
}
