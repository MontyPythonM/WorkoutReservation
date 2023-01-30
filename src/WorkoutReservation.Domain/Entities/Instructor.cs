using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public string Biography { get; set; }
    public string Email { get; set; }

    public ICollection<WorkoutType> WorkoutTypes { get; set; } = new List<WorkoutType>();
    public ICollection<BaseWorkout> BaseWorkouts { get; set; } = new List<BaseWorkout>();

    public Instructor()
    {
        // fore EF
    }

    public Instructor(string firstName, string lastName, Gender? gender, string biography, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Biography = biography;
        Email = email;
    }
}
