using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCreatedResultProfile : Profile
{
    public AccountCreatedResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.AccountCreatedResult>()
            .ConstructUsing((src, context) =>
                new Contracts.AccountCreatedResult(src.CustomerId, src.AccountId.HasValue ? src.AccountId.Value : 0, context.Mapper.Map<Contracts.AccountType>(src.AccountType), src.Balance, true));
    }
}
