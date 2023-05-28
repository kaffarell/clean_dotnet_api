using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Commands;

public class CreateUserCommand : IRequest<Guid> {
    public string Username {get; set;}
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository) {
        this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        _userRepository.Create(new User(newGuid, request.Username));
        _userRepository.SaveChanges();
        return Task.FromResult(newGuid);
    }
}