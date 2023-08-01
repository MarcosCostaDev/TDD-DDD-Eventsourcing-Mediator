using TheProject.Shared.Dtos.Requests;
using TheProject.Shared.Dtos.Responses;

namespace TheProject.Application.AppServices.Interfaces;

public interface IProductAppService : IAppService
{
    Task<ProductResponse> CreateProductAsync(ProductRequest request, CancellationToken cancellationToken = default);
    Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest request, CancellationToken cancellationToken = default);
}
