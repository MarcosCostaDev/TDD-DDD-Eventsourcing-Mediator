using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheProject.Core.Repositories;
using TheProject.Domain.Entities;

namespace TheProject.Domain.Repositories
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
