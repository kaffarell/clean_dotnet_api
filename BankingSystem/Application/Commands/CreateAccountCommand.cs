using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Commands;


public class CreateAccountCommand : IRequest<Guid> {
    public Guid UserId {get; set;}
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private IAccountRepository _accountRepository;
    private IUserRepository _userRepository;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository) {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        if(_userRepository.GetById(request.UserId) != null) {
            _accountRepository.Create(new Account(newGuid, request.UserId, 100)); 
            _accountRepository.SaveChanges();
        }
        return Task.FromResult(newGuid);
    }
}