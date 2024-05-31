using AutoMapper;

namespace Bank.Infrastructure.Mapping;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Models.Account, Domain.Models.Account>()
            .ConstructUsing((src, context) => 
                new Domain.Models.Account(src.AccountId, src.CustomerId, src.Balance, context.Mapper.Map<Domain.Models.AccountType>(src.AccountType), context.Mapper.Map<Domain.Models.AccountStatus>(src.AccountStatus)));

        CreateMap<Domain.Models.Account, Models.Account>()
            .ConstructUsing((src, context) => 
                new Models.Account(src.AccountId.HasValue ? src.AccountId.Value : 0, src.CustomerId, src.Balance, context.Mapper.Map<Models.AccountType>(src.AccountType), context.Mapper.Map<Models.AccountStatus>(src.Status)));
            //.ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId.HasValue ? src.AccountId.Value : 0));
    }
}
