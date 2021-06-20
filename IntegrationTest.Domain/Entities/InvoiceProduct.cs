using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace IntegrationTest.Domain.Entities
{
    public class InvoiceProduct : Notifiable<Notification>
    {
        protected InvoiceProduct() { }
        public InvoiceProduct(Guid productId, Guid invoiceId, double quantity)
        {
            var contract = new Contract<Notification>();
            contract.IsNotEmpty(productId, "ProductId", "ProductId must be informed.")
                    .IsNotEmpty(invoiceId, "InvoiceId", "InvoiceId must be informed.")
                    .IsGreaterThan(quantity, 0, "Quantity", "Quantity must be greater  0");

            AddNotifications(contract);
            ProductId = productId;
            InvoiceId = invoiceId;
            Quantity = quantity;
        }

        public Guid ProductId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public double Quantity { get; private set; }

        public Invoice Invoice { get; private set; }
        public Product Product { get; private set; }

    }
}
