using AutoMapper;
using TheProject.Application.Abstracts;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Core.Bus;
using TheProject.Domain.Entities;
using TheProject.Domain.Events;
using TheProject.Domain.Repositories;
using TheProject.Shared.Dtos.Requests;
using TheProject.Shared.Dtos.Responses;

namespace TheProject.Application.AppServices;

public class InvoiceAppService : AbstractAppService, IInvoiceAppService
{
    private IMapper _mapper;
    private IMediatorHandler _mediatorHandler;
    private IInvoiceRepository _invoiceRepository;
    private IProductRepository _productRepository;
    private IInvoiceProductsRepository _invoiceProductsRepository;

    public InvoiceAppService(
        IMapper mapper,
        IMediatorHandler mediatorHandler,
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository,
        IInvoiceProductsRepository invoiceProductsRepository)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _invoiceRepository = invoiceRepository;
        _productRepository = productRepository;
        _invoiceProductsRepository = invoiceProductsRepository;

    }
    public async Task<InvoiceResponse> CreateInvoiceAsync(InvoiceRequest request, CancellationToken cancellationToken = default)
    {

        var invoice = new Invoice(request.CustomerId);

        var products = await _productRepository.ListAsync(request.Items.Select(p => p.ProductId));

        invoice.SetTotal(products, request.Discount);

        var invoiceProducts = new List<InvoiceProduct>();

        foreach (var item in request.Items)
        {
            var productInvoice = new InvoiceProduct(item.ProductId, invoice.Id, item.Quantity);
            AddNotifications(productInvoice);
            invoiceProducts.Add(productInvoice);
        }

        await _invoiceRepository.AddAsync(invoice);

        await _invoiceProductsRepository.AddAsync(invoiceProducts);

        AddNotifications(invoice);

        await CommitAsync(_invoiceProductsRepository);

        if (cancellationToken.IsCancellationRequested || !IsValid) return null!;

        await _mediatorHandler.PublishEventAsync(new InvoiceCreatedEvent
        {
            InvoiceId = invoice.Id,
            CustomerId = request.CustomerId
        });

        return _mapper.Map<InvoiceResponse>(invoice);
    }
}
