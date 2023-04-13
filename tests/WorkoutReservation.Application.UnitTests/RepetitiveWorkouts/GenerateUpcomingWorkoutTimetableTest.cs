using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Common.MappingProfile;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Domain.Extensions;
using Xunit;
using static Moq.It;

namespace WorkoutReservation.Application.UnitTests.RepetitiveWorkouts;

public class GenerateUpcomingWorkoutTimetableTest : IClassFixture<Program>
{
    private static readonly Instructor Instructor = new("FirstName", "LastName", Gender.Unspecified, "Biography", "EmailAddress");
    private static readonly WorkoutType WorkoutType = new("Name", "Description", WorkoutIntensity.Extreme, new List<Instructor>(), new List<WorkoutTypeTag>());
    private readonly List<RepetitiveWorkout> _repetitiveWorkouts = new()
    {
        new RepetitiveWorkout(10, new TimeOnly(10,00), new TimeOnly(12,00), DayOfWeek.Monday, WorkoutType, Instructor), 
    };

    private readonly IMapper _mapperMock;
    private readonly Mock<IRepetitiveWorkoutRepository> _repetitiveWorkoutRepositoryMock = new();
    private readonly Mock<IRealWorkoutRepository> _realWorkoutRepositoryMock = new();
    private readonly Mock<IInstructorRepository> _instructorRepositoryMock = new();
    private readonly Mock<IWorkoutTypeRepository> _workoutTypeRepositoryMock = new();
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly ILogger<GenerateUpcomingWorkoutTimetableCommandHandler> _loggerMock = new Logger<GenerateUpcomingWorkoutTimetableCommandHandler>(new NullLoggerFactory());

    public GenerateUpcomingWorkoutTimetableTest()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<RepetitiveWorkoutProfile>(); });
        _mapperMock = configurationProvider.CreateMapper();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionExist_ThrowValidationException(int testScenario)
    {
        // arrange
        SetInstructorAndWorkoutTypeIds();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(new List<RepetitiveWorkout>()
            {
                new(10, new TimeOnly(10,00), new TimeOnly(12,00), DayOfWeek.Monday, WorkoutType, Instructor)
            });

        _instructorRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Instructor>(){ Instructor });
       
        _workoutTypeRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>()))
            .ReturnsAsync(new List<WorkoutType>(){ WorkoutType });
        
        var upcomingMonday = DateTime.Now.GetFirstDayOfWeekAndAddDays(7);
        
        var existingRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(11,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(9,00), new TimeOnly(11,00), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(10,30), new TimeOnly(11,30), WorkoutType, Instructor, upcomingMonday, false),
            new(10, new TimeOnly(9,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, false)
        };

        var selectedRealWorkout = new List<RealWorkout> { existingRealWorkouts[testScenario] };
        PrepareRealWorkouts(selectedRealWorkout);

        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<DomainException>();
    }
    
    [Fact]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionNotExist_NotThrowValidationException()
    {
        // arrange
        SetInstructorAndWorkoutTypeIds();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var mondayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(13,00), new TimeOnly(15,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(7), false), 
            new(10, new TimeOnly(8,00), new TimeOnly(10,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(7), false)
        };
        
        PrepareRealWorkouts(mondayRealWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<ValidationException>();
        await result.Should().NotThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_TimeCollisionExistButItNotTheSameDay_NotThrowValidationException()
    {
        // arrange
        SetInstructorAndWorkoutTypeIds();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);

        var wednesdayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(10,00), new TimeOnly(12,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(9), false), 
        };
        
        PrepareRealWorkouts(wednesdayRealWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<ValidationException>();
        await result.Should().NotThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handle_RepetitiveWorkoutsNotExists_ThrowNotFoundException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(new List<RepetitiveWorkout>());

        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_ForValidData_NotThrowAnyException()
    {
        // arrange
        SetInstructorAndWorkoutTypeIds();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(false, IsAny<CancellationToken>(), 
                incl => incl.Instructor, incl => incl.WorkoutType))
            .ReturnsAsync(_repetitiveWorkouts);

        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<ValidationException>();
        await result.Should().NotThrowAsync<NotFoundException>();
        await result.Should().NotThrowAsync<DomainException>();
    }
    
    private void PrepareRealWorkouts(List<RealWorkout> returns)
    {
        _realWorkoutRepositoryMock
            .Setup(x => x.GetAllFromDateRangeAsync(DateTime.Now.GetFirstDayOfWeekAndAddDays(7),
                DateTime.Now.GetFirstDayOfWeekAndAddDays(14), true, IsAny<CancellationToken>()))
            .ReturnsAsync(returns);
    }

    private GenerateUpcomingWorkoutTimetableCommand GetCommand() => new(Guid.NewGuid());
    
    private GenerateUpcomingWorkoutTimetableCommandHandler GetHandler() => 
        new(_repetitiveWorkoutRepositoryMock.Object, _realWorkoutRepositoryMock.Object, _mapperMock, _loggerMock,
        _instructorRepositoryMock.Object, _workoutTypeRepositoryMock.Object, _dateTimeProviderMock.Object);
    
    private void SetInstructorAndWorkoutTypeIds()
    {
        _repetitiveWorkouts[0].Instructor.Id = 1;
        _repetitiveWorkouts[0].WorkoutType.Id = 1;
    }
}