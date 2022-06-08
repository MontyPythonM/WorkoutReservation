using Moq;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.UnitTests.Mocks
{
    public static class WorkoutTypeRepositoryMock
    {
        public static Mock<IWorkoutTypeRepository> RepositoryMock()
        { 
            var workoutTypes = new List<WorkoutType>
            {
                new WorkoutType
                {
                    Id = 1,
                    Name = "TestName1",
                    Description = "TestDescription1",
                    Intensity = WorkoutIntensity.extreme,
                },

                new WorkoutType
                {
                    Id = 2,
                    Name = "TestName2",
                    Description = "TestDescription2",
                    Intensity = WorkoutIntensity.low,
                },

                new WorkoutType
                {
                    Id = 3,
                    Name = "TestName3",
                    Description = "TestDescription3",
                    Intensity = WorkoutIntensity.moderate,
                    Instructors = new List<Instructor>
                    {
                        new Instructor
                        {
                            Id = 1,
                            Biography = "testBiography",
                            Email = "test@email.com",
                            FirstName = "FirstNameTest",
                            LastName = "LastNameTest",
                            Gender = Gender.male
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IWorkoutTypeRepository>();

            repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(workoutTypes);

            repositoryMock
                .Setup(r => r.GetByIdAsync(workoutTypes[0].Id))
                .ReturnsAsync(workoutTypes[0]);

            return repositoryMock;
        }
    }
}
