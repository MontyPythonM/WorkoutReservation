using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class WorkoutType : Entity
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public WorkoutIntensity Intensity { get; private set; }
    public ICollection<Instructor> Instructors { get; private set; } = new List<Instructor>();
    public ICollection<WorkoutTypeTag> WorkoutTypeTags { get; private set; } = new List<WorkoutTypeTag>();
    public ICollection<BaseWorkout> BaseWorkouts { get; private set; } = new List<BaseWorkout>();

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
    
    protected override void Valid()
    {
        // TODO add validation for enum 
        
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException(this, Name, ExceptionCode.CannotBeNullOrWhiteSpace);
        
        if (Name.Length > 50)
            throw new DomainException(this, Name, ExceptionCode.ValueToLarge);
        
        if (string.IsNullOrWhiteSpace(Description))
            throw new DomainException(this, Description, ExceptionCode.CannotBeNullOrWhiteSpace);
        
        if (Description.Length > 600)
            throw new DomainException(this, Description, ExceptionCode.ValueToLarge);
    }
    
    private void AddTags(List<WorkoutTypeTag> tags)
    {
        WorkoutTypeTags.Clear();
        tags.ForEach(tag => WorkoutTypeTags.Add(tag));
        Valid();
    }
    
    private void AddInstructors(List<Instructor> instructors)
    {
        Instructors.Clear();
        instructors.ForEach(instructor => Instructors.Add(instructor));
        Valid();
    }
}
