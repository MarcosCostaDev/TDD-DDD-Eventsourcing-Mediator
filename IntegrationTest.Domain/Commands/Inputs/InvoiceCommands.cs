using AutoMapper;
using Flunt.Notifications;
using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Commands.Results.InvoiceCommandResults;

namespace IntegrationTest.Domain.Commands.Inputs
{
    public class InvoiceCommands :
        CommandHandler
        , IRequestHandler<CreateInvoiceCommand, CommandResult>
    {
        private IMapper _mapper;
        private IInvoiceRepository _invoiceRepository;
        private IProductRepository _productRepository;
        private IInvoiceProductsRepository _invoiceProductsRepository;

        public InvoiceCommands(
            IMapper mapper,
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository,
            IInvoiceProductsRepository invoiceProductsRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _invoiceProductsRepository = invoiceProductsRepository;
        }

        public async Task<CommandResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice(request.CustomerId);
            var products = await _productRepository.ListAsync(request.Items.Select(p => p.ProductId));
            invoice.SetTotal(products, request.Discount);
          
            foreach (var item in request.Items)
            {
                var productInvoice = new InvoiceProduct(item.ProductId, invoice.Id, item.Quantity);
                AddNotifications(productInvoice);
                await _invoiceProductsRepository.AddAsync(productInvoice);

            }

            AddNotifications(invoice);



            return new CommandResult(_mapper.Map<InvoiceCommandResult>(invoice), this);
        }

    }

    public class CreateInvoiceCommand : IRequest<CommandResult>
    {
        public Guid CustomerId { get; set; }
        public double Discount { get; set; }
        public IEnumerable<ItemsCommand> Items { get; set; }
        public class ItemsCommand
        {
            public Guid ProductId { get; set; }
            public double Quantity { get; set; }
        }
    }
}
