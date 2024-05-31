using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountTypeProfile : Profile
{
    public AccountTypeProfile()
    {
        CreateMap<Domain.Models.AccountType, Contracts.AccountType>();
        CreateMap<Contracts.AccountType, Domain.Models.AccountType>();
    }
}
