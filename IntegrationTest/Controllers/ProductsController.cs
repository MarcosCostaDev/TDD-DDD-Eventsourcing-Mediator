using AutoMapper;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repository;
using IntegrationTest.Infra.UnitOfWork;
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
        private IMapper _mapper;
        private ProductCommands _productCommands;
        private IProductRepository _productRepository;

        public ProductsController(
            IUnitOfWork unitOfWork
            , IMapper mapper
            , IProductRepository productRepository
            , ProductCommands productCommands)
            : base(unitOfWork)
        {
            _mapper = mapper;
            _productCommands = productCommands;
            _productRepository = productRepository;
        }

        [HttpPost("v1")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductModel createProductModel)
        {
            var product = await _productCommands.CreateProductAsync(_mapper.Map<CreateProductCommand>(createProductModel));
            return CommonResponse(product, _productCommands);
        }

        [HttpPut("v1")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductModel createProductModel)
        {
            var product = await _productCommands.CreateProductAsync(_mapper.Map<CreateProductCommand>(createProductModel));
            return CommonResponse(product, _productCommands);
        }

        [HttpGet("v1/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return CommonResponse(product);
        }
    }

    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }

    public class UpdateProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
