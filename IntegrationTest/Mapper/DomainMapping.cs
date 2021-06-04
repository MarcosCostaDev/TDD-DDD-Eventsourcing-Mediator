using AutoMapper;
using IntegrationTest.Controllers;
using static IntegrationTest.Domain.Commands.Inputs.ProductCommands;

namespace IntegrationTest.Mapper
{
    public class DomainMapping : Profile
    {
        public DomainMapping()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();            
        }
       
    }
}
