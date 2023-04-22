using WorkoutReservation.Shared.Attributes;

namespace WorkoutReservation.Shared.Exceptions;

public enum ExceptionCode
{
    [StringValue("Cannot be null")]
    CannotBeNull,
    
    [StringValue("Cannot be null or whitespace")]
    CannotBeNullOrWhiteSpace,

    [StringValue("Not exist")]
    NotExists,
    
    [StringValue("Already exists")]
    AlreadyExists,
    
    [StringValue("Invalid value")]
    InvalidValue,
    
    [StringValue("Value is to large")]
    ValueToLarge,
    
    [StringValue("Value is to small")]
    ValueToSmall,
    
    [StringValue("Value is out of range")]
    ValueOutOfRange,
    
    [StringValue("Cannot be overwritten")]
    CannotBeOverwritten
}