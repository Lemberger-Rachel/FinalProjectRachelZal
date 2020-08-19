using AutoMapper;
using BrixBank.Data.Entities;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using System;
using System.Linq;

namespace BrixBank.Data.Repositories
{
    public class LoanRequestRepository : ILoanRequestRepository
    {

        private readonly BrixBankContext _context;
        private readonly IMapper _mapper;
        public LoanRequestRepository(BrixBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public LoanRequestModel Reqest(LoanRequestModel loanRequestModel)
        {
            try
            {
                LoanRequest loanRequest = new LoanRequest();
                loanRequest.DictionaryData = loanRequestModel.DictionaryData;
                loanRequest.LoanSupplied=loanRequestModel.LoanSupplied;
                loanRequest.FirstName = loanRequestModel.FirstName;
                loanRequest.LastName = loanRequestModel.LastName;
                loanRequest.LoanRequestrId = loanRequestModel.LoanRequestrId;
                var LoanIsExists = _context.LoanRequests.FirstOrDefault(l => l.LoanRequestrId == loanRequestModel.LoanRequestrId);
                if (LoanIsExists == null)
                {
                    _context.LoanRequests.Add(loanRequest);
                    _context.SaveChanges();
                }
                Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == loanRequestModel.LoanSupplied);
                if (customer == null)
                {
                    throw new Exception("Custemer not exist");
                }
                var listRule = _context.Rules.Where(item => item.CustomerId2.CustomerId == loanRequestModel.LoanSupplied).ToList();
                return _mapper.Map<LoanRequestModel> (loanRequest);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}