using Moq;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.UnitTests.Mocks;

public static class WorkoutTypeRepositoryMock
{
    public static List<WorkoutType> GetDummyList()
    {
        var workoutTypes = new List<WorkoutType>
        {
            new WorkoutType()
            {
                Id = 1,
                Name = "Test-Hatha yoga",
                Description = "The purpose of Hatha Yoga is relief from three types of pain.",
                Intensity = WorkoutIntensity.Low,
                WorkoutTypeTags = new List<WorkoutTypeTag>() { new("Relax") }
            },

            new WorkoutType()
            {
                Id = 2,
                Name = "Test-Vinyasa yoga",
                Description = "Vinyasa is a style of yoga",
                Intensity = WorkoutIntensity.Moderate,
                WorkoutTypeTags = new List<WorkoutTypeTag>() { new("Stretch") }
            },               
        };

        return workoutTypes;
    }

    public static Mock<IWorkoutTypeRepository> GetRepositoryMock()
    {
        var workoutTypes = GetDummyList();
        var repositoryMock = new Mock<IWorkoutTypeRepository>();

        // GetAllAsync
        repositoryMock
            .Setup(r => r.GetAllAsync(false, CancellationToken.None))
            .ReturnsAsync(workoutTypes);

        // GetByIdAsync
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), false, CancellationToken.None))
            .ReturnsAsync((int id) =>
            {
                var workoutType = workoutTypes.FirstOrDefault(w => w.Id == id);
                return workoutType;
            });

        // AddAsync
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<WorkoutType>(), CancellationToken.None))
            .ReturnsAsync((WorkoutType workoutType) => 
            { 
                workoutTypes.Add(workoutType);
                return workoutType;
            });

        // DeleteAsync
        repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<WorkoutType>(), CancellationToken.None))
            .Callback<WorkoutType>((entity) =>                 
            {
                workoutTypes.Remove(entity);
            });

        // UpdateAsync
        repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<WorkoutType>(), CancellationToken.None))
            .Callback<WorkoutType>((entity) =>
            {
                workoutTypes.Remove(entity);
                workoutTypes.Add(entity);
            });

        return repositoryMock;
    }
}
