using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Common.MappingProfile;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Extensions;
using Xunit;
using static Moq.It;

namespace WorkoutReservation.Application.UnitTests.RepetitiveWorkouts;

public class GenerateUpcomingWorkoutTimetableTest : IClassFixture<Program>
{
    private static readonly Instructor Instructor = new("FirstName", "LastName", Gender.Unspecified, "Biography", "EmailAddress");
    private static readonly WorkoutType WorkoutType = new("Name", "Description", WorkoutIntensity.Extreme);
    private static readonly User User = new("email", "firstName", "lastName", Gender.Female, new DateOnly(1996, 10, 20), "passwordHash", "", new DateTime(2023, 01, 14), UserRole.Member);
    private readonly List<RepetitiveWorkout> _repetitiveWorkouts = new()
    {
        new RepetitiveWorkout(10, new TimeOnly(10,00), new TimeOnly(12,00), DayOfWeek.Monday, WorkoutType, Instructor), 
    };

    private readonly IMapper _mapperMock;
    private readonly Mock<IRepetitiveWorkoutRepository> _repetitiveWorkoutRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IRealWorkoutRepository> _realWorkoutRepositoryMock;
    private readonly Mock<ILogger<GenerateUpcomingWorkoutTimetableCommandHandler>> _loggerMock;

    public GenerateUpcomingWorkoutTimetableTest()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<RepetitiveWorkoutProfile>(); });
        _mapperMock = configurationProvider.CreateMapper();
        _repetitiveWorkoutRepositoryMock = new Mock<IRepetitiveWorkoutRepository>();
        _realWorkoutRepositoryMock = new Mock<IRealWorkoutRepository>();
        _loggerMock = new Mock<ILogger<GenerateUpcomingWorkoutTimetableCommandHandler>>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionExist_ThrowValidationException(int testScenario)
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);

        var upcomingMonday = DateTime.Now.GetFirstDayOfWeekAndAddDays(7);
        var existingRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(11,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, User),
            new(10, new TimeOnly(9,00), new TimeOnly(11,00), WorkoutType, Instructor, upcomingMonday, User),
            new(10, new TimeOnly(10,30), new TimeOnly(11,30), WorkoutType, Instructor, upcomingMonday, User),
            new(10, new TimeOnly(9,00), new TimeOnly(13,00), WorkoutType, Instructor, upcomingMonday, User)
        };

        var selectedRealWorkout = new List<RealWorkout> { existingRealWorkouts[testScenario] };
        _realWorkoutRepositoryMock
            .Setup(x => x.GetAllFromDateRangeAsync(DateTime.Now.GetFirstDayOfWeekAndAddDays(7), 
                DateTime.Now.GetFirstDayOfWeekAndAddDays(14), true, IsAny<CancellationToken>()))
            .ReturnsAsync(selectedRealWorkout);
        
        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task Handle_ExistingAndNewRealWorkoutsTimeCollisionNotExist_NotThrowValidationException()
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);

        var mondayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(13,00), new TimeOnly(15,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(7), User), 
            new(10, new TimeOnly(8,00), new TimeOnly(10,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(7), User)
        };
        
        _realWorkoutRepositoryMock
            .Setup(x => x.GetAllFromDateRangeAsync(DateTime.Now.GetFirstDayOfWeekAndAddDays(7), 
                DateTime.Now.GetFirstDayOfWeekAndAddDays(14), true, IsAny<CancellationToken>()))
            .ReturnsAsync(mondayRealWorkouts);
        
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
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);

        var wednesdayRealWorkouts = new List<RealWorkout>
        {
            new(10, new TimeOnly(10,00), new TimeOnly(12,00), WorkoutType, Instructor, DateTime.Now.GetFirstDayOfWeekAndAddDays(9), User), 
        };
        
        _realWorkoutRepositoryMock
            .Setup(x => x.GetAllFromDateRangeAsync(DateTime.Now.GetFirstDayOfWeekAndAddDays(7), 
                DateTime.Now.GetFirstDayOfWeekAndAddDays(14), true, IsAny<CancellationToken>()))
            .ReturnsAsync(wednesdayRealWorkouts);
        
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
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(new List<RepetitiveWorkout>());

        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_RepetitiveWorkoutsExists_NotThrowNotFoundException()
    {
        // arrange
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_ForValidData_NotThrowAnyException()
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);

        var handler = GetHandler();
        var command = GetCommand();

        // act
        Func<Task<Unit>> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<ValidationException>();
        await result.Should().NotThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData(12, 11)]
    [InlineData(23, 22)]
    [InlineData(23, 1)]
    public async Task Handle_StartTimeIsGreaterThenEndTime_ThrowValidationException(int startTimeHour, int endTimeHour)
    {
        // arrange
        var startTime = new TimeOnly(startTimeHour, 00);
        var endTime = new TimeOnly(endTimeHour, 00);

        _repetitiveWorkouts[0].StartTime = startTime;
        _repetitiveWorkouts[0].EndTime = endTime;

        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);

        var handler = GetHandler();
        var command = GetCommand();
        
        // act
        Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);
        
        // assert
        await result.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(-10)]
    public async Task Handle_MaxParticipantNumberEqualsZeroOrLess_ThrowValidationException(int participantsNumber)
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkouts[0].MaxParticipantNumber = participantsNumber;
        
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();
        
        // act
        Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);
        
        // assert
        await result.Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task Handle_InstructorIdEqualNullOrZero_ThrowValidationException(int? instructorId)
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkouts[0].InstructorId = instructorId;
        
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();
        
        // act
        Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);
        
        // assert
        await result.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task Handle_WorkoutTypeIdEqualNullOrZero_ThrowValidationException(int? workoutTypeId)
    {
        // arrange
        SetCorrectInstructorAndWorkoutTypeTestData();
        _repetitiveWorkouts[0].WorkoutTypeId = workoutTypeId;
        
        _repetitiveWorkoutRepositoryMock
            .Setup(x => x.GetAllAsync(IsAny<CancellationToken>()))
            .ReturnsAsync(_repetitiveWorkouts);
        
        var handler = GetHandler();
        var command = GetCommand();
        
        // act
        Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);
        
        // assert
        await result.Should().ThrowAsync<ValidationException>();
    }

    private GenerateUpcomingWorkoutTimetableCommand GetCommand() => new() { IsAutoGenerated = true };
    
    private GenerateUpcomingWorkoutTimetableCommandHandler GetHandler() => 
        new(_repetitiveWorkoutRepositoryMock.Object, _realWorkoutRepositoryMock.Object, 
            _currentUserServiceMock.Object, _mapperMock, _loggerMock.Object);
    
    private void SetCorrectInstructorAndWorkoutTypeTestData()
    {
        _repetitiveWorkouts[0].InstructorId = 1;
        _repetitiveWorkouts[0].WorkoutTypeId = 1;
    }
}