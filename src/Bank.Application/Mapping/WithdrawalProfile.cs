using AutoMapper;
using Bank.Application.Models;

namespace Bank.Application.Mapping;

public class WithdrawalProfile : Profile
{
    public WithdrawalProfile()
    {
        CreateMap<Contracts.Withdrawal, Domain.Models.Withdrawal>();
        CreateMap<Domain.Models.Withdrawal, Contracts.Withdrawal>();
        CreateMap<Domain.Models.Withdrawal, InputErrorActionInfo>();
    }
}
