using AutoMapper;
using BrixBank.Messeges.Command;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Models;
using NServiceBus;

namespace BrixBank.Services.Services
{
    public class LoanRequestServies : ILoanRequestServies
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageSession _messageSession;
        private readonly IMapper _mapper;
        public LoanRequestServies(ILoanRequestRepository repository, IMessageSession messageSession, IMapper mapper)
        {
            _repository = repository;
            _messageSession = messageSession;
            _mapper = mapper;
        }

        public async void Reqest(LoanRequestModel loanRequestModel)
        {
           var checkLoan =  _repository.Reqest(loanRequestModel);
           var messege= _mapper.Map<CheckLoan>(checkLoan);
           await _messageSession.Send(messege);
        }
    }
}
