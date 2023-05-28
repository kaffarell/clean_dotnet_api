namespace BankingSystem.Infrastructure;

public class UserRepository : IUserRepository
{
    private Context _context;

    public UserRepository(Context context) {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(User user)
    {
        _context.Users.Add(user);
    }

    public void Delete(Guid id)
    {
        _context.Users.Remove(this.GetById(id));
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}