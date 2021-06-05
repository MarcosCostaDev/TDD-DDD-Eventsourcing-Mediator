using AutoMapper;
using Flunt.Notifications;
using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repository;
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
        Notifiable<Notification> 
        , IRequestHandler<CreateInvoiceCommand, ICommandResult> 
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

        public async Task<ICommandResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
              var invoice = new Invoice(request.CustomerId);
              return _mapper.Map<InvoiceCommandResult>(invoice);
        }
       
    }

    public class CreateInvoiceCommand : IRequest<ICommandResult>
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
