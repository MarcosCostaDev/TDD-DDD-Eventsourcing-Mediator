using AutoMapper;
using IntegrationTest.Core.Bus;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : CommonController
    {
        private IMediatorHandler _mediator;
        private IMapper _mapper;
        private IInvoiceRepository _invoiceRepository;

        public InvoicesController(
            IMediatorHandler mediator
            , IMapper mapper
            , IInvoiceRepository invoiceRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
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
        public async Task<IActionResult> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            var commandResult = await _mediator.SendCommandAsync(_mapper.Map<CreateInvoiceCommand>(request));
            return CommonResponse(commandResult);
        }


        public class CreateInvoiceRequest
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
}
