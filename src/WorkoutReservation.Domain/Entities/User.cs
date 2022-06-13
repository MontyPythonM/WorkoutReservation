﻿using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string PasswordHash { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
