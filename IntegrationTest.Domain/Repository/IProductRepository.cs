using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Repository
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<IList<Product>> ListAllAsync();
    }
}
