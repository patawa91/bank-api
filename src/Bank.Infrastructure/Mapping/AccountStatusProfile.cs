using AutoMapper;

namespace Bank.Infrastructure.Mapping;

public class AccountStatusProfile : Profile
{
    public AccountStatusProfile()
    {
        CreateMap<Models.AccountStatus, Domain.Models.AccountStatus>();
        CreateMap<Domain.Models.AccountStatus, Models.AccountStatus>();
    }
}
