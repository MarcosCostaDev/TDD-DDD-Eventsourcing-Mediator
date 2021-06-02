using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Entities
{
    public class InvoiceProduct :Notifiable<Notification>
    {
        protected InvoiceProduct() { }
        public InvoiceProduct(Guid productId, Guid invoiceId, double quantity)
        {
            var contract = new Contract<Notification>();
            contract.IsNotEmpty(productId, "ProductId", "ProductId must be informed.")
                    .IsNotEmpty(invoiceId, "InvoiceId", "InvoiceId must be informed.")
                    .IsGreaterThan(quantity, 0, "Quantity", "Quantity must be greater  0");

            AddNotifications(contract);
            Id = Guid.NewGuid();
            ProductId = productId;
            InvoiceId = invoiceId;
            Quantity = quantity;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid InvoiceId { get; set; }
        public double Quantity { get; set; }

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }

    }
}
