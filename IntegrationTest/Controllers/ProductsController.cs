using AutoMapper;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repository;
using IntegrationTest.Infra.UnitOfWork;
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
        private IMediator _mediator;
        private IMapper _mapper;
        private ProductCommands _productCommands;
        private IProductRepository _productRepository;

        public ProductsController(
            IMediator mediator
            , IUnitOfWork unitOfWork
            , IMapper mapper
            , IProductRepository productRepository
            , ProductCommands productCommands)
            : base(unitOfWork)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productCommands = productCommands;
            _productRepository = productRepository;
        }

        [HttpPost("v1")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest createProductModel)
        {
            var product = await _mediator.Send(_mapper.Map<CreateProductCommand>(createProductModel));
            return CommonResponse(product, _productCommands);
        }

        [HttpPut("v1")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest createProductModel)
        {
            var product = await _mediator.Send(_mapper.Map<UpdateProductCommand>(createProductModel));
            return CommonResponse(product, _productCommands);
        }

        [HttpGet("v1/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return CommonResponse(product);
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
