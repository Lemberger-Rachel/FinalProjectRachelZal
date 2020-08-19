using BrixBank.Services.Models;

namespace BrixBank.Services.Interfaces
{
    public interface IRuleRepository
    {
        void ReadExcelFile();
        bool ToCheck(LoanRequestModel loanRequestModel);
        bool Manager(LoanRequestModel loanRequestModel);
    }
}
