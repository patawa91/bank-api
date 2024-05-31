using AutoMapper;

namespace Bank.Infrastructure.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Models.Customer, Domain.Models.Customer>();
    }
}
