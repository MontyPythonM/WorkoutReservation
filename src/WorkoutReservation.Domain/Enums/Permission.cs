﻿namespace WorkoutReservation.Domain.Enums;

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
    CanSeeApplicationStatistics,
    
    GetOwnReservations,
    GetAllReservations,
    CreateReservation,
    CancelReservation,
    UpdateReservation,

    GetAllUsers,
    SetUserRole,
    DeleteUserAccount,
    DeleteOwnAccount,
    
    OpenAdministrationPage,
    CanSeeAdministrativeContent,
    
    GetOwnReservationDetails,
    GetSomeoneReservationDetails,
    GetWorkoutTypes,
    
    CreateHomePageContent
}