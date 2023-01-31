using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class InstructorsData
{
    internal static IEnumerable<Instructor> Create()
    {
        var instructors = new List<Instructor>
        {
            new()
            {
                FirstName = "Tirumalai",
                LastName = "Krishnamacharya",
                Biography = "The “Father of Modern Yoga” is widely known for being the architect of vinyasa and credited with the revival of Hatha Yoga. Mainly known as a healer, he mixed his knowledge of both Ayurveda and yoga to restore health.",
                Email = "dummy-Tirumalai@Krishnamacharya.com",
                Gender = Gender.Male
            },

            new()
            {
                FirstName = "Ewa",
                LastName = "Chodakowska",
                Biography = "Polish fitness trainer, personal trainer, specializing in functional training and HIIT (High Intensity Interval Training), as well as blogger, TV presenter and media personality.",
                Email = "dummy-Ewa@Chodakowska.com",
                Gender = Gender.Female
            },

            new()
            {
                FirstName = "Melanie",
                LastName = "Brown",
                Biography = "",
                Email = "dummy-Melanie@Brown.com",
                Gender = Gender.Female
            }
        };
        return instructors;
    }
}
