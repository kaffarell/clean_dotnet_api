using BankingSystem.Infrastructure;
using BankingSystem.Application.Commands;
using BankingSystem.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DepositAccountCommandTests {
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public DepositAccountCommandTests() {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("InMem").Options;
        
        var context = new Context(options);
        _accountRepository = new AccountRepository(context);
        _userRepository = new UserRepository(context);
    }


    [Fact]
    public async void DepositWithoutAccountExisting()
    {
        var handler = new DepositAccountCommandHandler(_accountRepository);
        var deposit = await handler.Handle(new DepositAccountCommand { AccountId = Guid.NewGuid(), Amount = "0" }, default);
        Assert.False(deposit, "Account does not exist");
    }

    [Fact]
    public async void DepositNormal()
    {
        var userId = await Helper.CreateUser(_userRepository);
        var accountId = await Helper.CreateAccount(userId, _accountRepository, _userRepository);

        var handler = new DepositAccountCommandHandler(_accountRepository);
        var deposit = await handler.Handle(new DepositAccountCommand { AccountId = accountId, Amount = "100" }, default);

        Assert.True(deposit, "error");

        var amount = await Helper.GetCurrencyForAccount(accountId, _accountRepository);
        Assert.Equal(amount, (decimal)200.0);
    }

}