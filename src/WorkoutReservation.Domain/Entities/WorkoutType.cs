using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class WorkoutType : Entity
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public WorkoutIntensity Intensity { get; private set; }
    public ICollection<Instructor> Instructors { get; private set; } = new List<Instructor>();
    public ICollection<WorkoutTypeTag> WorkoutTypeTags { get; private set; } = new List<WorkoutTypeTag>();
    public ICollection<RealWorkout> RealWorkouts { get; private set; } = new List<RealWorkout>();
    public ICollection<RepetitiveWorkout> RepetitiveWorkouts { get; private set; } = new List<RepetitiveWorkout>();

    private WorkoutType()
    {
        // required for EF Core
    }
    
    public WorkoutType(string name, string description, WorkoutIntensity intensity,
        List<Instructor> instructors, List<WorkoutTypeTag> tags)
    {
        Name = name;
        Description = description;
        Intensity = intensity;
        AddTags(tags);
        AddInstructors(instructors);
        Valid();
    }

    public WorkoutType(int id, string name, string description, WorkoutIntensity intensity,
        List<Instructor> instructors, List<WorkoutTypeTag> tags)
    {
        Id = id;
        Name = name;
        Description = description;
        Intensity = intensity;
        AddTags(tags);
        AddInstructors(instructors);
        Valid();
    }
    
    public void Update(string name, string description, WorkoutIntensity intensity, 
        List<Instructor> instructors, List<WorkoutTypeTag> tags)
    {
        Name = name;
        Description = description;
        Intensity = intensity;
        AddTags(tags);
        AddInstructors(instructors);
        Valid();
    }
    
    private const int NameLengthLimit = 50;
    private const int DescriptionLengthLimit = 600;

    protected override void Valid()
    {
        if (!Enum.IsDefined(Intensity))
            throw new IntensityOutOfRangeException();

        if (string.IsNullOrWhiteSpace(Name))
            throw new WorkoutTypeNameCannotBeNullOrWhiteSpace();

        if (Name.Length > NameLengthLimit)
            throw new WorkoutTypeNameLengthExceedException(NameLengthLimit);

        if (string.IsNullOrWhiteSpace(Description))
            throw new DescriptionCannotBeNullOrWhitespaceException();

        if (Description.Length > DescriptionLengthLimit)
            throw new DescriptionLengthExceedException(DescriptionLengthLimit);
    }
    
    private void AddTags(List<WorkoutTypeTag> tags)
    {
        WorkoutTypeTags.Clear();
        tags.ForEach(tag => WorkoutTypeTags.Add(tag));
    }
    
    private void AddInstructors(List<Instructor> instructors)
    {
        Instructors.Clear();
        instructors.ForEach(instructor => Instructors.Add(instructor));
    }
}
