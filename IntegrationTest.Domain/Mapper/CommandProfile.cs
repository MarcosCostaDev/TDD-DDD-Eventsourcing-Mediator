using AutoMapper;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegrationTest.Domain.Commands.Results.InvoiceCommandResults;
using static IntegrationTest.Domain.Commands.Results.ProductCommandResults;

namespace IntegrationTest.Domain.Mapper
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Invoice, InvoiceCommandResult>();
            CreateMap<Product, CreateProductResult>();
            CreateMap<Product, UpdateProductResult>();
        }
    }
}
