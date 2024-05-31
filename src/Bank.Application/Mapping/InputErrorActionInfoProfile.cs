using AutoMapper;
using Bank.Application.Models;

namespace Bank.Application.Mapping;

public class InputErrorActionInfoProfile : Profile
{
    public InputErrorActionInfoProfile()
    {
        CreateMap<Domain.Models.AccountClose, InputErrorActionInfo>();
        //CreateMap<Domain.Models.AccountCustomerAction, InputErrorActionInfo>();
    }
}
