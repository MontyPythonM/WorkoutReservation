namespace WorkoutReservation.Infrastructure.Authorization;

public enum Permission
{
    CreateInstructor = 1,
    UpdateInstructor,
    DeleteInstructor,
    
    CreateWorkoutType,
    UpdateWorkoutType,
    DeleteWorkoutType,
    
    GetAllWorkoutTypeTags,
    CreateWorkoutTypeTag,
    UpdateWorkoutTypeTag,
    DeleteWorkoutTypeTag,

    GetRealWorkoutDetails,
    CreateRealWorkout,
    UpdateRealWorkout,
    DeleteRealWorkout,
    
    GetRepetitiveWorkouts,
    CreateRepetitiveWorkout,
    UpdateRepetitiveWorkout,
    DeleteRepetitiveWorkout,
    DeleteAllRepetitiveWorkouts,
    GenerateNewUpcomingWeek,
    OpenHangfireDashboard,
    
    GetOwnReservations,
    GetSomeoneReservations,
    CreateReservation,
    CancelReservation,
    UpdateReservationStatus,

    GetAllUsers,
    SetUserRole,
    DeleteUserAccount,
    DeleteOwnAccount,
    
    OpenAdministrationPage,
    CanSeeAdministrativeContent
}