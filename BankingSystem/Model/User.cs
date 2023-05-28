namespace BankingSystem;

public class User
{
    public User(Guid id, string username) {
        this.Id = id;
        this.Username = username;
    }

    public Guid Id { get; set; }

    public string Username { get; set; }
}
