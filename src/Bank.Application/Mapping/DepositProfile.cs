using AutoMapper;
using Bank.Application.Models;

namespace Bank.Application.Mapping;

public class DepositProfile : Profile
{
    public DepositProfile()
    {
        CreateMap<Contracts.Deposit, Domain.Models.Deposit>();
        CreateMap<Domain.Models.Deposit, Contracts.Deposit>();
        CreateMap<Domain.Models.Deposit, InputErrorActionInfo>();
    }
}
