using BrixBank.Messeges.Command;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using NServiceBus;
using System;

namespace BrixBank.Services.Services
{
    public class RuleService : IRuleService
    {

        private readonly IRuleRepository _repository;
        private readonly IMessageSession _messageSession;
        public RuleService(IRuleRepository repository,IMessageSession messageSession)
        {
            _repository = repository;
            _messageSession = messageSession;
        }

        public bool Manager(LoanRequestModel loanRequestModel)
        {
            throw new NotImplementedException();
        }

        public void ReadExcelFile()
        {
            _repository.ReadExcelFile();
        }

        public bool ToCheck(LoanRequestModel loanRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
