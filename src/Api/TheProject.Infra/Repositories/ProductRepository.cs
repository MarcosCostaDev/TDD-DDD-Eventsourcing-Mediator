using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheProject.Core.Repositories;
using TheProject.Domain.Entities;
using TheProject.Domain.Repositories;
using TheProject.Infra.Contexts;

namespace TheProject.Infra.Repositories
{
    public class ProductRepository : Repository<MyDbContext>, IProductRepository
    {

        public ProductRepository(MyDbContext context) : base(context)
        {
        }


        public async Task AddAsync(Product product)
        {
            await Context.Products.AddAsync(product);
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Product>> ListAllAsync()
        {
            return await Context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IList<Product>> ListAsync(IEnumerable<Guid> ids)
        {
            return await Context.Products.AsNoTracking().Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public Task UpdateAsync(Product product)
        {
            Context.Products.Attach(product).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
