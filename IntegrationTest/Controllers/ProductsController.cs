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
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CommonController
    {
        private IMediatorHandler _mediator;
        private IMapper _mapper;
        private IProductRepository _productRepository;

        public ProductsController(
            IMediatorHandler mediator
            , IMapper mapper
            , IProductRepository productRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpPost("v1")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest createProductModel)
        {
            var commandResult = await _mediator.SendCommandAsync(_mapper.Map<CreateProductCommand>(createProductModel));
            return CommonResponse(commandResult);
        }

        [HttpPut("v1")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest createProductModel)
        {
            var commandResult = await _mediator.SendCommandAsync(_mapper.Map<UpdateProductCommand>(createProductModel));
            return CommonResponse(commandResult);
        }

        [HttpGet("v1/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] Guid id)
        {
            var commandResult = await _productRepository.GetAsync(id);
            return CommonResponse(commandResult);
        }

        [HttpGet("v1")]
        public async Task<IActionResult> ListAsync()
        {
            var commandResult = await _productRepository.ListAllAsync();
            return CommonResponse(commandResult);
        }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }

    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
