using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repository;
using IntegrationTest.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Product>> ListAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
