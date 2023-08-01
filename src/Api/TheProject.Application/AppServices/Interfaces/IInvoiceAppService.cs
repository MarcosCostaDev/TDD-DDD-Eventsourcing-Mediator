using TheProject.Shared.Dtos.Requests;
using TheProject.Shared.Dtos.Responses;

namespace TheProject.Application.AppServices.Interfaces;

public interface IInvoiceAppService : IAppService
{
    Task<InvoiceResponse> CreateInvoiceAsync(InvoiceRequest request, CancellationToken cancellationToken = default);
}
