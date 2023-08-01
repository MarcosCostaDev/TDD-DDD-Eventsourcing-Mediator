using Microsoft.EntityFrameworkCore;

namespace TheProject.Core.Repositories
{
    public abstract class Repository<DatabaseContext> : UnitOfWork.UnitOfWork
        where DatabaseContext : DbContext
    {
        protected DatabaseContext Context;
        protected Repository(DatabaseContext context) : base(context)
        {
            Context = context;
        }
    }
}
