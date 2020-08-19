using BrixBank.Services.Models;

namespace BrixBank.Services.Interfaces
{
    public interface ILoanRequestRepository
    {
        LoanRequestModel Reqest(LoanRequestModel loanRequestModel);
    }
}
