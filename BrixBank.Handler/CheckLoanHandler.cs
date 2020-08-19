using BrixBank.Messeges.Command;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using NServiceBus;
using System.Threading.Tasks;

namespace BrixBank.Handler
{
    public class CheckLoanHandler : IHandleMessages<CheckLoan>
    {
        private readonly IRuleRepository _repository;
        public CheckLoanHandler(IRuleRepository repository)
        {
            _repository = repository;
        }
        public Task Handle(CheckLoan message, IMessageHandlerContext context)
        {
            LoanRequestModel loanRequestModel = new LoanRequestModel()
            {
                DictionaryData = message.DictionaryData,
                FirstName = message.FirstName,
                Id = message.Id,
                LastName = message.LastName,
                LoanRequestrId = message.LoanRequestrId,
                LoanSupplied = message.LoanSupplied
            };
            bool SystemResponse = _repository.ToCheck(loanRequestModel);
            System.Console.WriteLine(SystemResponse+ " SystemResponse");
            if (SystemResponse == false)
            {
                bool ManagerResponse = _repository.Manager(loanRequestModel);
                System.Console.WriteLine(ManagerResponse + " The manager's response to this unusual request");
            }
            return Task.CompletedTask;

        }
    }
}
