using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCloseProfile : Profile
{
    public AccountCloseProfile()
    {
        CreateMap<Contracts.AccountClose, Domain.Models.AccountClose>();
        CreateMap<Domain.Models.AccountClose, Contracts.AccountClose>();
    }
}
