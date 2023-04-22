using WorkoutReservation.Shared.Attributes;

namespace WorkoutReservation.Domain.Enums;

public enum SortBySelector
{
    [StringValue("Reservation id")]
    ReservationId,
    
    [StringValue("Reservation status")]
    ReservationStatus,
    
    [StringValue("Workout date")]
    WorkoutDate,
    
    [StringValue("Workout name")]
    WorkoutName,
    
    [StringValue("Workout intensity")]
    WorkoutIntensity,
    
    [StringValue("Created date")]
    CreatedDate,
    
    [StringValue("Last modified date")]
    LastModifiedDate,
    
    [StringValue("User id")]
    UserId,
    
    [StringValue("User email")]
    UserEmail,
    
    [StringValue("User first name")]
    UserFirstName,
    
    [StringValue("User last name")]
    UserLastName,
}