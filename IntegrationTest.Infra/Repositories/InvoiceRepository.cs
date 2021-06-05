﻿using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Repositories;
using IntegrationTest.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Repositories
{
    public class InvoiceRepository : Repository<MyDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(MyDbContext context) : base(context)
        {
        }
    }
}