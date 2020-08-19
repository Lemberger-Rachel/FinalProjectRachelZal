using AutoMapper;
using BrixBank.Data.Entities;
using BrixBank.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrixBank.Data.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer, CustomerModel>();
            CreateMap<LoanRequest, LoanRequestModel>();
            CreateMap<LoanRequestModel, LoanRequest>();

        }
    }
}
