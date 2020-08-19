using AutoMapper;
using BrixBank.Messeges.Command;
using BrixBank.Services.Models;

namespace BrixBank.Services.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LoanRequestModel, CheckLoan>();
            CreateMap<CheckLoan, LoanRequestModel>();
        }
    }
}
