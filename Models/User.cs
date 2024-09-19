namespace calendify_app.Models;

public class User
{
    public User(Guid id, string firstName, string lastName, string email, string password, int recurringDays)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        RecurringDays = recurringDays;
    }

    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required int RecurringDays { get; set; }
}