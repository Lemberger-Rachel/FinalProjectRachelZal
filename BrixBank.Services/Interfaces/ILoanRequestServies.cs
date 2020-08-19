using BrixBank.Services.Models;

namespace BrixBank.Services.Interfaces
{
    public interface ILoanRequestServies
    {
        void Reqest(LoanRequestModel loanRequestModel);
    }
}
