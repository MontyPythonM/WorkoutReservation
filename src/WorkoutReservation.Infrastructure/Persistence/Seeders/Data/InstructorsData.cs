using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class InstructorsData
{
    private const string KrishnamacharyaBiography =
        "The “Father of Modern Yoga” is widely known for being the architect of vinyasa and credited with the revival of Hatha Yoga. " +
        "Mainly known as a healer, he mixed his knowledge of both Ayurveda and yoga to restore health.";

    private const string ChodakowskaBiography =
        "Polish fitness trainer, personal trainer, specializing in functional training and HIIT (High Intensity Interval Training), " +
        "as well as blogger, TV presenter and media personality.";
    
    internal static IEnumerable<Instructor> Create()
    {
        var instructors = new List<Instructor>
        {
            new("Tirumalai", "Krishnamacharya", Gender.Male, KrishnamacharyaBiography, "tirumalai.krishnamacharya@workout.com"),
            new("Ewa", "Chodakowska", Gender.Female, ChodakowskaBiography, "ewa.chodakowska@workout.com"),
            new("Melanie", "Brown", Gender.Female, string.Empty, "m.brown@workout.com"),
            new("Bully", "Juice", Gender.Male, string.Empty, "bully.juice@workout.com")
        };
        
        return instructors;
    }
}
