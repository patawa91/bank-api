using AutoMapper;

namespace Bank.Application.Mapping;

public class WithdrawalResultProfile : Profile
{
    public WithdrawalResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.WithdrawalResult>()
            .ConstructUsing((src, context) =>
                new Contracts.WithdrawalResult(src.CustomerId, src.AccountId.HasValue ? src.AccountId.Value : 0, src.Balance, true));
    }
}
