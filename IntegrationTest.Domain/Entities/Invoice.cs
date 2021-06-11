using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Entities
{
    public class Invoice : Notifiable<Notification>
    {
        public Invoice(Guid customerId)
        {
            var contract = new Contract<Notification>();
            contract.IsNotEmpty(customerId, "CustomerId", "Customer must be informed");
            AddNotifications(contract);
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            CustomerId = customerId;
        }

        public void SetTotal(IList<Product> products, double discount)
        {
            var contract = new Contract<Notification>();
            if(discount > 0.1)
            {
                contract.AddNotification("Discount", "Discount max allowed is 10 percent.");
            }
            AddNotifications(contract);
            Discount = discount;
            Total = products.Sum(p => p.Price);
            TotalWithDiscount = Total - (Total * discount);
        }
        
        

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public double Discount { get; private set; }
        public double Total { get; private set; }
        public double TotalWithDiscount { get; private set; }
        public IReadOnlyCollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
