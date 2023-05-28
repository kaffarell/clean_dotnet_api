using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Commands;

public class WithdrawAccountCommand : IRequest<bool> {
    public Guid AccountId {get; set;}
    public string Amount {get; set;}
}

public class WithdrawAccountCommandHandler : IRequestHandler<WithdrawAccountCommand, bool>
{
    private IAccountRepository _accountRepository;

    public WithdrawAccountCommandHandler(IAccountRepository accountRepository) {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public Task<bool> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
    {
        var amount = _accountRepository.GetById(request.AccountId).Amount;
        var requestAmount = Convert.ToDecimal(request.Amount);

        bool amountIsNotNegative = requestAmount > 0;
        bool amountNotUnder100 = (amount - requestAmount) >= 100;
        bool amountNotBiggerThan90Percent = (amount * Convert.ToDecimal(0.9)) > requestAmount;

        if(amountIsNotNegative && amountNotUnder100 && amountNotBiggerThan90Percent) {
            _accountRepository.Edit(request.AccountId, amount - requestAmount);
            _accountRepository.SaveChanges();
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}