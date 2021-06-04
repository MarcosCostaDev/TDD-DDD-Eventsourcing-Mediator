using Flunt.Notifications;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Commands.Inputs
{
    public class InvoiceCommands : Notifiable<Notification>
    {
        private IInvoiceRepository _invoiceRepository;
        private IProductRepository _productRepository;
        private IInvoiceProductsRepository _invoiceProductsRepository;

        public InvoiceCommands(
            IInvoiceRepository invoiceRepository
            , IProductRepository productRepository
            , IInvoiceProductsRepository invoiceProductsRepository
            )
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _invoiceProductsRepository = invoiceProductsRepository;
        }

        public async Task<Invoice> CreateInvoice(CreateInvoiceCommand command)
        {
            var invoice = new Invoice(command.CustomerId);

            return invoice;

        }
        public class CreateInvoiceCommand
        {
            public Guid CustomerId { get; set; }
            public double Discount { get; set; }
            public class ItemsCommand
            {
                public Guid ProductId { get; set; }
                public double Quantity { get; set; }
                public double Discount { get; set; }
            }
        }
    }
}
