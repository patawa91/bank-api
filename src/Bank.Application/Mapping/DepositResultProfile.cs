using AutoMapper;

namespace Bank.Application.Mapping;

public class DepositResultProfile : Profile
{
    public DepositResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.DepositResult>()
            .ConstructUsing((src, context) =>
                            new Contracts.DepositResult(src.CustomerId, src.AccountId.HasValue ? src.AccountId.Value : 0, src.Balance, true));
    }
}
