using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Commands;

public class DepositAccountCommand : IRequest<bool> {
    public Guid AccountId {get; set;}
    public string Amount {get; set;}
}

public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, bool>
{
    private IAccountRepository _accountRepository;

    public DepositAccountCommandHandler(IAccountRepository accountRepository) {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public Task<bool> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
    {
        var requestAmount = Convert.ToDecimal(request.Amount);
        if(requestAmount > 0 && requestAmount <= 10_000) {
            var acc = _accountRepository.GetById(request.AccountId);
            if(acc != null) {
                _accountRepository.Edit(request.AccountId, acc.Amount + requestAmount);
                _accountRepository.SaveChanges();
                return Task.FromResult(true);
            }
        }
        return Task.FromResult(false);
    }
}