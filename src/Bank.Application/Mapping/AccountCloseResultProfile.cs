using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCloseResultProfile : Profile
{
    public AccountCloseResultProfile()
    {
        CreateMap<Domain.Models.AccountClose, Contracts.AccountCloseResult>()
            .ConstructUsing((src, context) =>
                            new Contracts.AccountCloseResult(src.CustomerId, src.AccountId, true));
            
    }
}
