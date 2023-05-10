using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Domain.Repositories;
using TheProject.Shared.Dtos.Requests;

namespace TheProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : CommonController
    {
        private IMapper _mapper;
        private IInvoiceAppService _invoiceAppService;
        private IInvoiceRepository _invoiceRepository;

        public InvoicesController(
            IMapper mapper,
            IInvoiceAppService invoiceAppService,
            IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _invoiceAppService = invoiceAppService;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("v1/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return CommonResponse(await _invoiceRepository.GetAsync(id));
        }

        [HttpGet("v1")]
        public async Task<IActionResult> GetAsync()
        {
            return CommonResponse(await _invoiceRepository.ListAsync());
        }

        [HttpPost("v1")]
        public async Task<IActionResult> PostAsync(InvoiceRequest request)
        {
            var result = await _invoiceAppService.CreateInvoiceAsync(request);

            return CommonResponse(result, _invoiceAppService.Notifications);
        }
    }
}
