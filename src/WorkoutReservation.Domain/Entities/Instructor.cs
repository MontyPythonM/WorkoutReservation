using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

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
    public ICollection<RealWorkout> RealWorkouts { get; private set; } = new List<RealWorkout>();
    public ICollection<RepetitiveWorkout> RepetitiveWorkouts { get; private set; } = new List<RepetitiveWorkout>();

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

    private const int FirstNameLengthLimit = 50;
    private const int LastNameLengthLimit = 50;
    private const int BiographyLengthLimit = 3000;
    
    protected override void Valid()
    {
        if (!Enum.IsDefined(Gender.Value) && Gender is not null)
            throw new GenderOutOfRangeException();

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new FirstNameIsNullOrWhiteSpaceException();

        if (FirstName.Length > FirstNameLengthLimit)
            throw new FirstNameLengthExceedException(FirstNameLengthLimit);

        if (string.IsNullOrWhiteSpace(LastName))
            throw new LastNameIsNullOrWhiteSpaceException();

        if (LastName.Length > LastNameLengthLimit)
            throw new LastNameLengthExceedException(LastNameLengthLimit);

        if (Biography.Length > BiographyLengthLimit)
            throw new BiographyLengthLimitExceedException(BiographyLengthLimit);

        if (string.IsNullOrWhiteSpace(Email))
            throw new EmailCannotBeNullOrWhitespaceException();
        
        // TODO: add email format policy
    }
}
