using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Core.Bus;
using TheProject.Domain.Repositories;
using TheProject.Shared.Dtos.Requests;

namespace TheProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CommonController
    {
        private IMediatorHandler _mediator;
        private IMapper _mapper;
        private IProductRepository _productRepository;
        private readonly IProductAppService _productAppService;

        public ProductsController(
            IMediatorHandler mediator,
            IMapper mapper,
            IProductRepository productRepository,
            IProductAppService productAppService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productRepository = productRepository;
            _productAppService = productAppService;
        }

        [HttpPost("v1")]
        public async Task<IActionResult> PostAsync([FromBody] ProductRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _productAppService.CreateProductAsync(request, cancellationToken);

            return CommonResponse(result, _productAppService.Notifications);
        }

        [HttpPut("v1/{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] Guid id, [FromBody] ProductRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _productAppService.UpdateProductAsync(id, request, cancellationToken);

            return CommonResponse(result, _productAppService.Notifications);
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

}
