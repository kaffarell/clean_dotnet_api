namespace BankingSystem.Infrastructure;

public interface IUserRepository {
    void Create(User user);
    void Delete(Guid id);
    IEnumerable<User> GetAll();
    User GetById(Guid id);
    bool SaveChanges();
}