namespace BankingSystem.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private Context _context;

    public AccountRepository(Context context) {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Edit(Guid id, decimal amount)
    {
        var acc = _context.Accounts.FirstOrDefault(a => a.Id == id);
        if(acc != null) {
            acc.Amount = amount;
        }
    }

    public void Create(Account account)
    {
        _context.Accounts.Add(account);
    }

    public void Delete(Guid id)
    {
        _context.Accounts.Remove(this.GetById(id));
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public Account GetById(Guid id)
    {
        return _context.Accounts.FirstOrDefault(a => a.Id == id);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}