using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task AddAsync(Product product);
        Task<IList<Product>> ListAllAsync();
        Task<Product> GetAsync(Guid id);
        Task<IList<Product>> ListAsync(IEnumerable<Guid> ids);
        Task UpdateAsync(Product product);
    }
}
