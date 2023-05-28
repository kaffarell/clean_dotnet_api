namespace BankingSystem;

public class Account
{
    public Account(Guid id, Guid userId, decimal amount) {
        this.Id = id;
        this.Amount = amount;
        this.UserId = userId;
    }

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }
}
