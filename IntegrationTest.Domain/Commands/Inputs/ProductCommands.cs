using AutoMapper;
using Flunt.Notifications;
using IntegrationTest.Core.Bus;
using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;
using static IntegrationTest.Domain.Commands.Results.ProductCommandResults;
using static IntegrationTest.Domain.Events.ProductEvents;

namespace IntegrationTest.Domain.Commands.Inputs
{
    public class ProductCommands 
        : CommandHandler
        , IRequestHandler<CreateProductCommand, CommandResult>
        , IRequestHandler<UpdateProductCommand, CommandResult>
    {
        private IMapper _mapper;
        private IMediatorHandler _mediatorHandler;
        private IProductRepository _productRepository;

        public ProductCommands(
            IMapper mapper,
             IMediatorHandler mediatorHandler,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _productRepository = productRepository;

        }

        public async Task<CommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Brand, request.Price);

            await _productRepository.AddAsync(product);
            AddNotifications(product);

            await CommitAsync(_productRepository);
            if (IsValid)
            {
                await _mediatorHandler.PublishEventAsync(new CreatedProductEvent
                {
                    ProductId = product.Id
                });
            }

            return new CommandResult(_mapper.Map<CreateProductResult>(product), this);

        }

        public async Task<CommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.Id);
            product.Update(request.Name, request.Brand, request.Price);

            await _productRepository.UpdateAsync(product);


            await CommitAsync(_productRepository);
            return new CommandResult(_mapper.Map<UpdateProductResult>(product), this);

        }


        public class CreateProductCommand : CommandRequest
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }

        public class UpdateProductCommand : CommandRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }
    }
}

