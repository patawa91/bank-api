using AutoMapper;

namespace Bank.Application.Mapping;

public class DepositResultProfile : Profile
{
    public DepositResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.DepositResult>()
            .ForMember(dest => dest.Succeeded, opt => opt.MapFrom(src => true));
    }
}
