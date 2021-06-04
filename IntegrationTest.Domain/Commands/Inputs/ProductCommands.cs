using Flunt.Notifications;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Commands.Inputs
{
    public class ProductCommands : Notifiable<Notification>
    {
        private IProductRepository _productRepository;

        public ProductCommands(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(CreateProductCommand command)
        {
            var product = new Product(command.Name, command.Brand, command.Price);

            await _productRepository.AddAsync(product);

            return product;

        }

        public async Task<Product> UpdateProductAsync(UpdateProductCommand command)
        {
            var product = new Product(command.Name, command.Brand, command.Price);

            await _productRepository.AddAsync(product);

            return product;

        }

        public class CreateProductCommand
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }

        public class UpdateProductCommand
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
        }
    }
}

