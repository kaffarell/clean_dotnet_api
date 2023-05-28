using BankingSystem.Infrastructure;
using BankingSystem.Application.Commands;
using BankingSystem.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class WithdrawAccountCommandTests {
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public WithdrawAccountCommandTests() {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("InMem").Options;
        
        var context = new Context(options);
        _accountRepository = new AccountRepository(context);
        _userRepository = new UserRepository(context);
    }


    [Fact]
    public async void WithdrawFromMinimum()
    {
        var userId = await Helper.CreateUser(_userRepository);
        var accountId = await Helper.CreateAccount(userId, _accountRepository, _userRepository);

        var handler = new WithdrawAccountCommandHandler(_accountRepository);
        var withdraw = await handler.Handle(new WithdrawAccountCommand { AccountId = accountId, Amount = "100" }, default);

        Assert.False(withdraw, "Cannot withdraw under minimum.");
    }

    [Fact]
    public async void WithdrawNormal()
    {
        var userId = await Helper.CreateUser(_userRepository);
        var accountId = await Helper.CreateAccount(userId, _accountRepository, _userRepository);

        var handler = new DepositAccountCommandHandler(_accountRepository);
        var deposit = await handler.Handle(new DepositAccountCommand { AccountId = accountId, Amount = "200" }, default);
        Assert.True(deposit, "Deposit failure");

        var handler1 = new WithdrawAccountCommandHandler(_accountRepository);
        var withdraw = await handler1.Handle(new WithdrawAccountCommand { AccountId = accountId, Amount = "100" }, default);
        Assert.True(withdraw, "Withdraw failure");

        var amount = await Helper.GetCurrencyForAccount(accountId, _accountRepository);
        Assert.Equal(amount, (decimal)200.0);
    }

    [Fact]
    public async void WithdrawMoreThan90Percent()
    {
        var userId = await Helper.CreateUser(_userRepository);
        var accountId = await Helper.CreateAccount(userId, _accountRepository, _userRepository);

        var handler = new DepositAccountCommandHandler(_accountRepository);
        var deposit = await handler.Handle(new DepositAccountCommand { AccountId = accountId, Amount = "900" }, default);
        Assert.True(deposit, "Deposit failure");

        var handler1 = new WithdrawAccountCommandHandler(_accountRepository);
        var withdraw = await handler1.Handle(new WithdrawAccountCommand { AccountId = accountId, Amount = "900" }, default);
        Assert.False(withdraw, "Withdraw failure");
    }

}