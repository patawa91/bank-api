using AutoMapper;
using Bank.Application.Models;

namespace Bank.Application.Mapping;

public class InputErrorAccountInfoProfile : Profile
{
    public InputErrorAccountInfoProfile() 
    {
        CreateMap<Domain.Models.Account, InputErrorAccountInfo>()
            .ConstructUsing((src, context) =>
                        new InputErrorAccountInfo(src.AccountId.HasValue ? src.AccountId: 0, src.CustomerId,src.Balance, src.AccountType.ToString(), src.Status.ToString()));
    }
}
