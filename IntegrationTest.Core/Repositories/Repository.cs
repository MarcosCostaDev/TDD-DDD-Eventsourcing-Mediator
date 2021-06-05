using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTest.Core.Repositories
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
