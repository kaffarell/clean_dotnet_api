using BankingSystem.Application.Queries;
using BankingSystem.Infrastructure;
using MediatR;

namespace BankingSystem.Application.Queries;


public class GetAllUserQuery : IRequest<IEnumerable<User>> {}

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<User>>
{
    private IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository) {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public Task<IEnumerable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken) {
        return Task.FromResult(_userRepository.GetAll());
    }
}
