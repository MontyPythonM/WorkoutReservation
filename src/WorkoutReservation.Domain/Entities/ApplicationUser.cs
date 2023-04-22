﻿using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class ApplicationUser : Entity
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender? Gender { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsDeleted { get; set; }
    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
    public ICollection<ApplicationRole> ApplicationRoles { get; private set; } = new List<ApplicationRole>();

    private ApplicationUser()
    {
        // required for EF Core
    }
    
    public ApplicationUser(string email, string firstName, string lastName, Gender? gender, DateOnly? dateOfBirth)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PasswordHash = string.Empty;
        IsDeleted = false;
        Valid();
    }

    public void SetRole(ApplicationRole role)
    {
        ApplicationRoles.Clear();
        ApplicationRoles.Add(role);
        Valid();
    }
    
    public void SetRoles(List<ApplicationRole> roles)
    {
        ApplicationRoles.Clear();

        foreach (var role in roles.Distinct())
        {
            ApplicationRoles.Add(role);
        }
        Valid();
    }
    
    public void SetPasswordHash(string passwordHash) => PasswordHash = passwordHash;

    public bool IsInRole(Role role)
    {
        var appRole = ApplicationRole.FromValue((int)role);
        return ApplicationRoles.Any(role => role.Id.Equals(appRole.Id));
    }

    public void SoftDeleteUser()
    {
        AnonymizeSensitiveData();
        IsDeleted = true;
        Valid();
    }

    private void AnonymizeSensitiveData()
    {
        const string anonymizedValue = "ANONYMIZED";

        Email = anonymizedValue;
        FirstName = anonymizedValue;
        LastName = anonymizedValue;
        Gender = Enums.Gender.Unspecified;
        DateOfBirth = null;
        PasswordHash = string.Empty;
        Valid();
    }

    private const int FirstNameLengthLimit = 50;
    private const int LastNameLengthLimit = 50;
    
    protected override void Valid()
    {
        if (!Enum.IsDefined(Gender.Value) && Gender.HasValue)
            throw new GenderOutOfRangeException();

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new FirstNameIsNullOrWhiteSpaceException();

        if (FirstName.Length > FirstNameLengthLimit)
            throw new FirstNameLengthExceedException(FirstNameLengthLimit);

        if (string.IsNullOrWhiteSpace(LastName))
            throw new LastNameIsNullOrWhiteSpaceException();

        if (LastName.Length > LastNameLengthLimit)
            throw new LastNameLengthExceedException(LastNameLengthLimit);

        if (DateOfBirth > DateOnly.FromDateTime(DateTime.Now) && DateOfBirth.HasValue)
            throw new DateOfBirthInFutureException();
    }
}
