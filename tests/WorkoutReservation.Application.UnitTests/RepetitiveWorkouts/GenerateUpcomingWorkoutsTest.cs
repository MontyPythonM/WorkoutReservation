using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Shouldly;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkouts;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Shared.TypesExtensions;
using Xunit;
using static Moq.It;

namespace WorkoutReservation.Application.UnitTests.RepetitiveWorkouts;

public class GenerateUpcomingWorkoutsTest : IClassFixture<Program>
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionExist_ThrowValidationException(int testScenario)
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(new List<RepetitiveWorkout>()
            {
                new(10, new TimeOnly(10,00), new TimeOnly(12,00), DayOfWeek.Monday, WorkoutType, Instructor, true)
            });

        var upcomingMonday = DateTime.Now.GetFirstDayOfUpcomingWeek();
        
        SetDateTimeProviderMock(upcomingMonday);
        SetInstructorMock();
        SetWorkoutTypeMock();
        
        var existingRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(11,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(9,00), new TimeOnly(11,00), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(10,30), new TimeOnly(11,30), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(9,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, false)
        };

        var selectedRealWorkout = new List<RealWorkout> { existingRealWorkouts[testScenario] };
        SetRealWorkoutMock(selectedRealWorkout);
        
        // act
        Func<Task<Unit>> result = async () => await GetHandler().Handle(GetCommand(), CancellationToken.None);

        // assert
        await result.ShouldThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionNotExist_NotThrowValidationException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var mondayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(13,00), new TimeOnly(15,00), WorkoutType, Instructor, DateTime.Now.GetLastDayOfCurrentWeek().AddDays(1), false), 
            new(10, new TimeOnly(8,00), new TimeOnly(10,00), WorkoutType, Instructor, DateTime.Now.GetLastDayOfCurrentWeek().AddDays(1), false)
        };
        
        SetRealWorkoutMock(mondayRealWorkouts);
        SetInstructorMock();
        SetWorkoutTypeMock();
        
        // act
        Func<Task<Unit>> result = async () => await GetHandler().Handle(GetCommand(), CancellationToken.None);

        // assert
        await result.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Handle_TimeCollisionExistButItNotTheSameDay_NotThrowValidationException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);

        var wednesdayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(10,00), new TimeOnly(12,00), WorkoutType, Instructor, DateTime.Now.GetLastDayOfCurrentWeek().AddDays(3), false), 
        };
        
        SetRealWorkoutMock(wednesdayRealWorkouts);
        SetInstructorMock();
        SetWorkoutTypeMock();
        
        // act
        Func<Task<Unit>> result = async () => await GetHandler().Handle(GetCommand(), CancellationToken.None);

        // assert
        await result.ShouldNotThrowAsync();
    }
    
    [Fact]
    public async Task Handle_RepetitiveWorkoutsNotExists_ThrowNotFoundException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(new List<RepetitiveWorkout>());

        // act
        Func<Task<Unit>> result = async () => await GetHandler().Handle(GetCommand(), CancellationToken.None);

        // assert
        await result.ShouldThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_ForValidData_NotThrowAnyException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);

        var mondayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(13,00), new TimeOnly(15,00), WorkoutType, Instructor, DateTime.Now.GetLastDayOfCurrentWeek().AddDays(1), false), 
            new(10, new TimeOnly(8,00), new TimeOnly(10,00), WorkoutType, Instructor, DateTime.Now.GetLastDayOfCurrentWeek().AddDays(1), false)
        };
        
        SetInstructorMock();
        SetWorkoutTypeMock();
        SetRealWorkoutMock(mondayRealWorkouts);
        
        // act
        Func<Task<Unit>> result = async () => await GetHandler().Handle(GetCommand(), CancellationToken.None);

        // assert
        await result.ShouldNotThrowAsync();
    }

    #region Arrange

    private static readonly Instructor Instructor = new(1 ,"FirstName", "LastName", Gender.Unspecified, "Biography", "email@address.com");
    private static readonly WorkoutType WorkoutType = new(1, "Name", "Description", WorkoutIntensity.Extreme, new List<Instructor>(), new List<WorkoutTypeTag>());
    private readonly List<RepetitiveWorkout> _repetitiveWorkouts = new()
    {
        new RepetitiveWorkout(10, new TimeOnly(10,00), new TimeOnly(12,00), DayOfWeek.Monday, WorkoutType, Instructor, true), 
    };

    private readonly IMapper _mapperMock;
    private readonly Mock<IRepetitiveWorkoutRepository> _repetitiveWorkoutRepositoryMock = new();
    private readonly Mock<IRealWorkoutRepository> _realWorkoutRepositoryMock = new();
    private readonly Mock<IInstructorRepository> _instructorRepositoryMock = new();
    private readonly Mock<IWorkoutTypeRepository> _workoutTypeRepositoryMock = new();
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly ILogger<GenerateUpcomingWorkoutsHandler> _loggerMock = new Logger<GenerateUpcomingWorkoutsHandler>(new NullLoggerFactory());

    public GenerateUpcomingWorkoutsTest()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<RepetitiveWorkoutProfile>(); });
        _mapperMock = configurationProvider.CreateMapper();
    }
    
    private GenerateUpcomingWorkoutsCommand GetCommand() => new(IsAutoGenerated: true);
    
    private GenerateUpcomingWorkoutsHandler GetHandler() => 
        new(_repetitiveWorkoutRepositoryMock.Object, _realWorkoutRepositoryMock.Object, _mapperMock, _loggerMock,
            _instructorRepositoryMock.Object, _workoutTypeRepositoryMock.Object, _dateTimeProviderMock.Object);
    
    private void SetDateTimeProviderMock(DateOnly upcomingMonday)
    {
        _dateTimeProviderMock
            .Setup(x => x.CalculateDateInUpcomingWeek(DayOfWeek.Monday))
            .Returns(upcomingMonday);
    }

    private void SetWorkoutTypeMock()
    {
        _workoutTypeRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>()))
            .ReturnsAsync(new List<WorkoutType>() { WorkoutType });
    }

    private void SetInstructorMock()
    {
        _instructorRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Instructor>() { Instructor });
    }
    
    private void SetRealWorkoutMock(List<RealWorkout> returns)
    {
        _dateTimeProviderMock.Setup(x => x.GetFirstDayOfUpcomingWeek())
            .Returns(DateTime.Now.GetFirstDayOfUpcomingWeek());
        
        _dateTimeProviderMock.Setup(x => x.GetLastDayOfUpcomingWeek())
            .Returns(DateTime.Now.GetLastDayOfUpcomingWeek());
        
        _realWorkoutRepositoryMock
            .Setup(x => x.GetAllFromDateRangeAsync(DateTime.Now.GetFirstDayOfUpcomingWeek(),
                DateTime.Now.GetLastDayOfUpcomingWeek(), true, IsAny<CancellationToken>()))
            .ReturnsAsync(returns);
    }
    
    #endregion
}