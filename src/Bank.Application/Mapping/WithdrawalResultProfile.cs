using AutoMapper;

namespace Bank.Application.Mapping;

public class WithdrawalResultProfile : Profile
{
    public WithdrawalResultProfile()
    {
        CreateMap<Domain.Models.Account, Contracts.WithdrawalResult>()
            .ForMember(dest => dest.Succeeded, opt => opt.MapFrom(src => true));
    }
}
