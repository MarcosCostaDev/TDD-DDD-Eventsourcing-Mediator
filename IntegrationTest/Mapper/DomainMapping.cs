using AutoMapper;
using IntegrationTest.Controllers;
using IntegrationTest.Domain.Commands.Inputs;
using static IntegrationTest.Controllers.InvoicesController;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.Mapper
{
    public class DomainMapping : Profile
    {
        public DomainMapping()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
          
            CreateMap<CreateInvoiceRequest, CreateInvoiceCommand>();
            CreateMap<CreateInvoiceRequest.ItemsCommand, CreateInvoiceCommand.ItemsCommand>();
        }
       
    }
}
