using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntegrationTest.Core.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void RollBack()
        {
            
        }

        public Task RoolbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
