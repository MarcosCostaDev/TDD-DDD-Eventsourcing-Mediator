using AutoMapper;
using TheProject.Application.Abstracts;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Core.Bus;
using TheProject.Domain.Entities;
using TheProject.Domain.Events;
using TheProject.Domain.Repositories;
using TheProject.Shared.Dtos.Requests;
using TheProject.Shared.Dtos.Responses;
using TheProject.Shared.Resources;

namespace TheProject.Application.AppServices;

public class ProductAppService : AbstractAppService, IProductAppService
{
    private IMapper _mapper;
    private IMediatorHandler _mediatorHandler;
    private IProductRepository _productRepository;

    public ProductAppService(
        IMapper mapper,
         IMediatorHandler mediatorHandler,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _productRepository = productRepository;

    }

    public async Task<ProductResponse> CreateProductAsync(ProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new Product(request.Name, request.Brand, request.Price);

        await _productRepository.AddAsync(product);

        AddNotifications(product);

        if (cancellationToken.IsCancellationRequested || !IsValid) return null!;

        await CommitAsync(_productRepository);

        await _mediatorHandler.PublishEventAsync(new CreatedProductEvent
        {
            ProductId = product.Id
        });

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetAsync(id);

        if(product == null) {
            AddNotification(nameof(Product), AppResource.ProductNotExist);
            return null!;
        }

        product.Update(request.Name, request.Brand, request.Price);

        AddNotifications(product);

        await _productRepository.UpdateAsync(product);

        if (cancellationToken.IsCancellationRequested || !IsValid) return null!;

        await CommitAsync(_productRepository);

        return _mapper.Map<ProductResponse>(product);
    }
}
