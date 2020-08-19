using BrixBank.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrixBank.Services.Interfaces
{
    public interface IRuleRepository
    {
        void ReadExcelFile();
        bool ToCheck(LoanRequestModel loanRequestModel);
        bool Manager(LoanRequestModel loanRequestModel);
    }
}
