using Flunt.Notifications;
using Flunt.Validations;
using System;
using TheProject.Shared.Resources;

namespace TheProject.Domain.Entities
{
    public class InvoiceProduct : Notifiable<Notification>
    {
        protected InvoiceProduct() { }
        public InvoiceProduct(Guid productId, Guid invoiceId, double quantity)
        {
            var contract = new Contract<Notification>();
            contract.IsNotEmpty(productId, nameof(ProductId), AppResource.ProductIdMustBeInformed)
                    .IsNotEmpty(invoiceId, nameof(InvoiceId), AppResource.InvoiceIdMustBeInformed)
                    .IsGreaterThan(quantity, 0, nameof(Quantity), AppResource.QuantityMustBeGreaterThanZero);

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
