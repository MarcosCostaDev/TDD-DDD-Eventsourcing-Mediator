using AutoMapper;
using Flunt.Notifications;
using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;
using static IntegrationTest.Domain.Commands.Results.ProductCommandResults;

namespace IntegrationTest.Domain.Commands.Inputs
{
    public class ProductCommands 
        : Notifiable<Notification>
        , IRequestHandler<CreateProductCommand, ICommandResult>
        , IRequestHandler<UpdateProductCommand, ICommandResult>
    {
        private IMapper _mapper;
        private IProductRepository _productRepository;

        public ProductCommands(
            IMapper mapper,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Brand, request.Price);

            await _productRepository.AddAsync(product);

            return _mapper.Map<CreateProductResult>(product);

        }

        public async Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.Id);
            product.Update(request.Name, request.Brand, request.Price);

            await _productRepository.UpdateAsync(product);


            return _mapper.Map<UpdateProductResult>(product);

        }


        public class CreateProductCommand : IRequest<ICommandResult>
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }

        public class UpdateProductCommand : IRequest<ICommandResult>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }
    }
}

