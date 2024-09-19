namespace calendify_app.Models;

public class Admin
{
    public Admin(Guid id, string username, string password, string email)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
    }

    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }

}