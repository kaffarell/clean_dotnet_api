using BankingSystem.Infrastructure;
using BankingSystem.Application.Commands;
using BankingSystem.Application.Queries;

public static class Helper {
    public static async Task<Guid> CreateUser(IUserRepository userRepository) {
        var handler = new CreateUserCommandHandler(userRepository);
        var userId = await handler.Handle(new CreateUserCommand{Username = "test"}, default);
        return userId;
    }

    public static async Task<Guid> CreateAccount(Guid userId, IAccountRepository accountRepository, IUserRepository userRepository) {
        var handler = new CreateAccountCommandHandler(accountRepository, userRepository);
        var accountId = await handler.Handle(new CreateAccountCommand{UserId = userId}, default);
        return accountId;
    }

    public static async Task<decimal> GetCurrencyForAccount(Guid accountId, IAccountRepository accountRepository) {
        var handler = new GetAllAccountsQueryHandler(accountRepository);
        var allAccounts = await handler.Handle(new GetAllAccountsQuery{}, default);
        return allAccounts.FirstOrDefault(e => e.Id == accountId).Amount;
    }
}