using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCloseProfile : Profile
{
    public AccountCloseProfile()
    {
        CreateMap<Contracts.AccountClose, Domain.Models.AccountClose>()
            .ConstructUsing((src, context) =>
                            new Domain.Models.AccountClose(src.CustomerId, src.AccountId, 0));
        CreateMap<Domain.Models.AccountClose, Contracts.AccountClose>();
    }
}
