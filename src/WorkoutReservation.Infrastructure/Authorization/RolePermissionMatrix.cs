using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Authorization;

internal static class RolePermissionMatrix
{
    public static IEnumerable<ApplicationRolePermission> Create()
    {
        return new List<ApplicationRolePermission>
        {
            Create(ApplicationRole.SystemAdministrator, Permission.CreateInstructor),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateInstructor),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteInstructor),
            Create(ApplicationRole.SystemAdministrator, Permission.CreateWorkoutType),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateWorkoutType),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteWorkoutType),
            Create(ApplicationRole.SystemAdministrator, Permission.GetAllWorkoutTypeTags),
            Create(ApplicationRole.SystemAdministrator, Permission.CreateWorkoutTypeTag),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateWorkoutTypeTag),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteWorkoutTypeTag),
            Create(ApplicationRole.SystemAdministrator, Permission.GetRealWorkoutDetails),
            Create(ApplicationRole.SystemAdministrator, Permission.CreateRealWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateRealWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteRealWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.GetRepetitiveWorkouts),
            Create(ApplicationRole.SystemAdministrator, Permission.CreateRepetitiveWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateRepetitiveWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteRepetitiveWorkout),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteAllRepetitiveWorkouts),
            Create(ApplicationRole.SystemAdministrator, Permission.GenerateNewUpcomingWeek),
            Create(ApplicationRole.SystemAdministrator, Permission.OpenHangfireDashboard),
            Create(ApplicationRole.SystemAdministrator, Permission.GetOwnReservations),
            Create(ApplicationRole.SystemAdministrator, Permission.GetAllReservations),
            Create(ApplicationRole.SystemAdministrator, Permission.GetOwnReservationDetails),
            Create(ApplicationRole.SystemAdministrator, Permission.GetSomeoneReservationDetails),
            Create(ApplicationRole.SystemAdministrator, Permission.CreateReservation),
            Create(ApplicationRole.SystemAdministrator, Permission.CancelReservation),
            Create(ApplicationRole.SystemAdministrator, Permission.UpdateReservation),
            Create(ApplicationRole.SystemAdministrator, Permission.GetAllUsers),
            Create(ApplicationRole.SystemAdministrator, Permission.SetUserRole),
            Create(ApplicationRole.SystemAdministrator, Permission.DeleteUserAccount),
            Create(ApplicationRole.SystemAdministrator, Permission.OpenAdministrationPage),
            Create(ApplicationRole.SystemAdministrator, Permission.CanSeeAdministrativeContent),
            Create(ApplicationRole.SystemAdministrator, Permission.GetWorkoutTypes),

            Create(ApplicationRole.BusinessAdministrator, Permission.CreateInstructor),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateInstructor),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteInstructor),
            Create(ApplicationRole.BusinessAdministrator, Permission.CreateWorkoutType),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateWorkoutType),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteWorkoutType),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetAllWorkoutTypeTags),
            Create(ApplicationRole.BusinessAdministrator, Permission.CreateWorkoutTypeTag),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateWorkoutTypeTag),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteWorkoutTypeTag),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetRealWorkoutDetails),
            Create(ApplicationRole.BusinessAdministrator, Permission.CreateRealWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateRealWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteRealWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetRepetitiveWorkouts),
            Create(ApplicationRole.BusinessAdministrator, Permission.CreateRepetitiveWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateRepetitiveWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteRepetitiveWorkout),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteAllRepetitiveWorkouts),
            Create(ApplicationRole.BusinessAdministrator, Permission.OpenHangfireDashboard),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetAllReservations),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetSomeoneReservationDetails),
            Create(ApplicationRole.BusinessAdministrator, Permission.UpdateReservation),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetAllUsers),
            Create(ApplicationRole.BusinessAdministrator, Permission.SetUserRole),
            Create(ApplicationRole.BusinessAdministrator, Permission.DeleteOwnAccount),
            Create(ApplicationRole.BusinessAdministrator, Permission.OpenAdministrationPage),
            Create(ApplicationRole.BusinessAdministrator, Permission.CanSeeAdministrativeContent),
            Create(ApplicationRole.BusinessAdministrator, Permission.GetWorkoutTypes),
            
            Create(ApplicationRole.Manager, Permission.GetAllWorkoutTypeTags),
            Create(ApplicationRole.Manager, Permission.GetRealWorkoutDetails),
            Create(ApplicationRole.Manager, Permission.UpdateRealWorkout),
            Create(ApplicationRole.Manager, Permission.GetRepetitiveWorkouts),
            Create(ApplicationRole.Manager, Permission.GetOwnReservations),
            Create(ApplicationRole.Manager, Permission.GetAllReservations),
            Create(ApplicationRole.Manager, Permission.GetOwnReservationDetails),
            Create(ApplicationRole.Manager, Permission.GetSomeoneReservationDetails),
            Create(ApplicationRole.Manager, Permission.CreateReservation),
            Create(ApplicationRole.Manager, Permission.CancelReservation),
            Create(ApplicationRole.Manager, Permission.UpdateReservation),
            Create(ApplicationRole.Manager, Permission.GetAllUsers),
            Create(ApplicationRole.Manager, Permission.DeleteOwnAccount),
            Create(ApplicationRole.Manager, Permission.OpenAdministrationPage),
            Create(ApplicationRole.Manager, Permission.CanSeeAdministrativeContent),
            Create(ApplicationRole.Manager, Permission.GetWorkoutTypes),

            Create(ApplicationRole.Member, Permission.GetRealWorkoutDetails),
            Create(ApplicationRole.Member, Permission.GetOwnReservations),
            Create(ApplicationRole.Member, Permission.GetOwnReservationDetails),
            Create(ApplicationRole.Member, Permission.CreateReservation),
            Create(ApplicationRole.Member, Permission.CancelReservation),
            Create(ApplicationRole.Member, Permission.DeleteOwnAccount),
        };
    }

    private static ApplicationRolePermission Create(ApplicationRole role, Permission permission)
    {
        return new ApplicationRolePermission
        {
            ApplicationRoleId = role.Id,
            ApplicationPermissionId = (int)permission
        };
    }
}

