using AutoMapper;

namespace Bank.Application.Mapping;

public class AccountCloseResultProfile : Profile
{
    public AccountCloseResultProfile()
    {
        CreateMap<Domain.Models.AccountClose, Contracts.AccountCloseResult>()
            .ForMember(dest => dest.Succeeded, opt => opt.MapFrom(src => true));
    }
}
