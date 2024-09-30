using System;
using System.Collections.Generic;

namespace calendify_app.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, string email, string password, int recurringDays, string role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            RecurringDays = recurringDays;
            Role = role;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RecurringDays { get; set; }
        public string Role { get; set; }

        public List<EventAttendance> EventAttendance { get; } = new List<EventAttendance>();
        public List<Event> Events { get; } = new List<Event>();
    }
}
