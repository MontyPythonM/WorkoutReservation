using Shouldly;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Exceptions;
using Xunit;

namespace WorkoutReservation.Domain.UnitTests.Entities;

public class RealWorkoutTest
{
    [Theory]
    [InlineData("2023-05-30 00:00", "yyyy-MM-dd HH:mm")]
    [InlineData("2023-06-01 11:00", "yyyy-MM-dd HH:mm")]
    [InlineData("2022-07-01 00:00", "yyyy-MM-dd HH:mm")]
    public void AddReservation_ThrowRealWorkoutAlreadyStartedException_WhenRealWorkoutIsExpired(
        string dateValue, string dateFormat)
    {
        // arrange
        var now = new DateTime(2023, 06, 01, 12, 00, 00);
        var invalidDate = DateTime.ParseExact(dateValue, dateFormat, null);
        
        var user = _user;
        var realWorkout = new RealWorkout(10, TimeOnly.FromDateTime(invalidDate), 
            TimeOnly.FromDateTime(invalidDate), _workoutType, _instructor, 
            DateOnly.FromDateTime(invalidDate), false);

        // act
        var exception = Record.Exception(() => realWorkout.AddReservation(user, now));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RealWorkoutAlreadyStartedException>();
    }
    
    [Fact]
    public void AddReservation_IncrementCurrentParticipantNumber_WhenValid()
    {
        // arrange
        var user = _user;
        var realWorkout = new RealWorkout(10, new TimeOnly(10,00), new TimeOnly(12,00), 
            _workoutType, _instructor, new DateOnly(2023, 06, 01), false);

        var initialParticipants = realWorkout.CurrentParticipantNumber;
        
        // act
        realWorkout.AddReservation(user, new DateTime(2023, 06, 01));
        
        // assert
        realWorkout.CurrentParticipantNumber.ShouldBeGreaterThan(initialParticipants);
        realWorkout.CurrentParticipantNumber.ShouldBeEquivalentTo(initialParticipants + 1);
    }

    [Fact]
    public void Valid_ThrowMaxParticipantNumberLessOrEqualZeroException_WhenMaxParticipantNumberIsInvalid()
    {
        // arrange
        var invalidMaxParticipantNumber = -1;
        
        // act
        var exception = Record.Exception(() => new RealWorkout(invalidMaxParticipantNumber, 
            new TimeOnly(10,00), new TimeOnly(12,00), _workoutType, _instructor, 
            new DateOnly(2023, 06, 01), false)) ;
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<MaxParticipantNumberLessOrEqualZeroException>();
    }

    [Theory]
    [InlineData("00:00:00", "HH:mm:ss")]
    [InlineData("11:59:59", "HH:mm:ss")]
    public void Valid_ThrowStartTimeGreaterThanEndTimeException_WhenStartTimeIsGraterThanEndTime(
        string timeValue, string timeFormat)
    {
        // arrange
        var startTime = new TimeOnly(12, 00, 00);
        var invalidTime = TimeOnly.ParseExact(timeValue, timeFormat, null);
        
        // act
        var exception = Record.Exception(() => new RealWorkout(10, startTime, invalidTime, 
            _workoutType, _instructor, new DateOnly(2023, 06, 01), false)) ;
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<StartTimeGreaterThanEndTimeException>();
    }

    [Fact]
    public void Valid_ThrowWorkoutTypeCannotBeNullException_WhenWorkoutTypeIsNull()
    {
        // arrange
        // act
        var exception = Record.Exception(() => new RealWorkout(10, new TimeOnly(10,00), 
            new TimeOnly(12,00), null, _instructor, new DateOnly(2023, 06, 01), false)) ;
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<WorkoutTypeCannotBeNullException>();
    }

    [Fact]
    public void Valid_ThrowInstructorCannotBeNullException_WhenInstructorIsNull()
    {
        // arrange
        // act
        var exception = Record.Exception(() => new RealWorkout(10, new TimeOnly(10,00), 
            new TimeOnly(12,00), _workoutType, null, new DateOnly(2023, 06, 01), false));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InstructorCannotBeNullException>();
    }
    
    #region Arrange

    private static ApplicationUser _user = 
        new("email@email.com", "firstName", "LastName", Gender.Unspecified, null, "passwordHash");
    
    private static Instructor _instructor = 
        new(1, "firstName", "lastName", Gender.Unspecified, "biography", "email@email.com");
    
    private static WorkoutType _workoutType = 
        new(1, "name", "description", WorkoutIntensity.Low, new List<Instructor>(), new List<WorkoutTypeTag>());
    
    #endregion
}