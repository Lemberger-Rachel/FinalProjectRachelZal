using BrixBank.Services.Interfaces;
using NServiceBus;

namespace BrixBank.Services.Services
{
    public class RuleService : IRuleService
    {

        private readonly IRuleRepository _repository;
        private readonly IMessageSession _messageSession;
        public RuleService(IRuleRepository repository, IMessageSession messageSession)
        {
            _repository = repository;
            _messageSession = messageSession;
        }

        public void ReadExcelFile()
        {
            _repository.ReadExcelFile();
        }

    }
}
