using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data
{
    internal class DummyInstructors
    {
        internal static List<Instructor> GetInstructors()
        {
            var instructors = new List<Instructor>
            {
                new Instructor()
                {
                    FirstName = "Tirumalai",
                    LastName = "Krishnamacharya",
                    Biography = "The “Father of Modern Yoga” is widely known for being the architect of vinyasa and credited with the revival of Hatha Yoga. Mainly known as a healer, he mixed his knowledge of both Ayurveda and yoga to restore health.",
                    Email = "dummy-Tirumalai@Krishnamacharya.com",
                    Gender = Gender.male
                },

                new Instructor()
                {
                    FirstName = "Ewa",
                    LastName = "Chodakowska",
                    Biography = "Polish fitness trainer, personal trainer, specializing in functional training and HIIT (High Intensity Interval Training), as well as blogger, TV presenter and media personality.",
                    Email = "dummy-Ewa@Chodakowska.com",
                    Gender = Gender.female
                },

                new Instructor()
                {
                    FirstName = "Melanie",
                    LastName = "Brown",
                    Biography = "",
                    Email = "dummy-Melanie@Brown.com",
                    Gender = Gender.female
                }
            };

            return instructors;
        }

    }
}
