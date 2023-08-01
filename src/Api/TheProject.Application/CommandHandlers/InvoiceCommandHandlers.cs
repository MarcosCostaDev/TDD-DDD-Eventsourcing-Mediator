using Flunt.Notifications;
using MediatR;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Core.Command;
using TheProject.Shared.Dtos.Requests;

namespace TheProject.Application.CommandHandlers;

public class InvoiceCommandHandlers : IRequestHandler<NotifiableCommandRequest<InvoiceRequest>, IEnumerable<Notification>>
{
    private readonly IInvoiceAppService _invoiceAppService;

    public InvoiceCommandHandlers(IInvoiceAppService invoiceAppService)
    {
        _invoiceAppService = invoiceAppService;
    }

    public async Task<IEnumerable<Notification>> Handle(NotifiableCommandRequest<InvoiceRequest> request, CancellationToken cancellationToken)
    {
        await _invoiceAppService.CreateInvoiceAsync(request.Payload, cancellationToken);
        return _invoiceAppService.Notifications;
    }
}
