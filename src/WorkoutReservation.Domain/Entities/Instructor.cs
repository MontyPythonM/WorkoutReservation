using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class Instructor : Entity
{
    public int Id { get; internal set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender? Gender { get; private set; }
    public string Biography { get; private set; }
    public string Email { get; private set; }
    public ICollection<WorkoutType> WorkoutTypes { get; private set; } = new List<WorkoutType>();
    public ICollection<BaseWorkout> BaseWorkouts { get; private set; } = new List<BaseWorkout>();

    private Instructor()
    {
        // required for EF Core
    }
    
    public Instructor(string firstName, string lastName, Gender? gender, string biography, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Biography = biography;
        Email = email;
        Valid();
    }

    public void Update(string firstName, string lastName, Gender? gender, string biography, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Biography = biography;
        Email = email;
        Valid();
    }

    protected override void Valid()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new DomainException(this, FirstName, ExceptionCode.CannotBeNullOrWhiteSpace);

        if (FirstName.Length > 50)
            throw new DomainException(this, FirstName, ExceptionCode.ValueToLarge);
        
        if (string.IsNullOrWhiteSpace(LastName))
            throw new DomainException(this, LastName, ExceptionCode.CannotBeNullOrWhiteSpace);
        
        if (LastName.Length > 50)
            throw new DomainException(this, LastName, ExceptionCode.ValueToLarge);

        if (Biography.Length > 3000)
            throw new DomainException(this, Biography, ExceptionCode.ValueToLarge);
        
        if (string.IsNullOrWhiteSpace(Email))
            throw new DomainException(this, Email, ExceptionCode.CannotBeNullOrWhiteSpace);
        
        if (Email.Length > 50)
            throw new DomainException(this, Email, ExceptionCode.ValueToLarge);
    }
}
