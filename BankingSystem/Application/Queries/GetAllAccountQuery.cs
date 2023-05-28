using BankingSystem.Application.Queries;
using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Queries;


public class GetAllAccountsQuery : IRequest<IEnumerable<Account>> {}

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
{
    private IAccountRepository _accountRepository;

    public GetAllAccountsQueryHandler(IAccountRepository accountRepository) {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public Task<IEnumerable<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken) {
        return Task.FromResult(_accountRepository.GetAll());
    }
}