using AutoMapper;
using TheProject.Domain.Entities;
using TheProject.Shared.Dtos.Responses;

namespace TheProject.Application.MappingProfiles;

public class InvoiceMapProfile : Profile
{
    public InvoiceMapProfile()
    {
        CreateMap<Invoice, InvoiceResponse>();
        CreateMap<Product, ProductResponse>();
        CreateMap<InvoiceProduct, InvoiceProductResponse>();
    }
}
