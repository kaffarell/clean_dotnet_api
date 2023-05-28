namespace BankingSystem.Infrastructure;

public interface IAccountRepository {
    void Create(Account account);
    void Delete(Guid id);
    IEnumerable<Account> GetAll();
    Account GetById(Guid id);
    void Edit(Guid id, decimal amount);
    bool SaveChanges();
}