using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCreatedResultProfile : Profile
{
    public AccountCreatedResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.AccountCreatedResult>()
            .ForMember(dest => dest.AccountTypeId, opt => opt.MapFrom(src => src.AccountType))
            .ForMember(dest => dest.Succeeded, opt => opt.MapFrom(src => true));
    }
}
