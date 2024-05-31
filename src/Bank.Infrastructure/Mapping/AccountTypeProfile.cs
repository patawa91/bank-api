using AutoMapper;

namespace Bank.Infrastructure.Mapping;

public class AccountTypeProfile : Profile
{
    public AccountTypeProfile()
    {
        CreateMap<Models.AccountType, Domain.Models.AccountType>();
        CreateMap<Domain.Models.AccountType, Models.AccountType>();
    }
}
