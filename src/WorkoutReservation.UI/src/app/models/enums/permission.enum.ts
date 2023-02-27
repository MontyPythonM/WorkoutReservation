export enum Permission {
  CreateInstructor = "CreateInstructor",
  UpdateInstructor = "UpdateInstructor",
  DeleteInstructor = "DeleteInstructor",

  CreateWorkoutType = "CreateWorkoutType",
  UpdateWorkoutType = "UpdateWorkoutType",
  DeleteWorkoutType = "DeleteWorkoutType",

  GetAllWorkoutTypeTags = "GetAllWorkoutTypeTags",
  CreateWorkoutTypeTag = "CreateWorkoutTypeTag",
  UpdateWorkoutTypeTag = "UpdateWorkoutTypeTag",
  DeleteWorkoutTypeTag = "DeleteWorkoutTypeTag",

  GetRealWorkoutDetails = "GetRealWorkoutDetails",
  CreateRealWorkout = "CreateRealWorkout",
  UpdateRealWorkout = "UpdateRealWorkout",
  DeleteRealWorkout = "DeleteRealWorkout",

  GetRepetitiveWorkouts = "GetRepetitiveWorkouts",
  CreateRepetitiveWorkout = "CreateRepetitiveWorkout",
  UpdateRepetitiveWorkout = "UpdateRepetitiveWorkout",
  DeleteRepetitiveWorkout = "DeleteRepetitiveWorkout",
  DeleteAllRepetitiveWorkouts = "DeleteAllRepetitiveWorkouts",
  GenerateNewUpcomingWeek = "GenerateNewUpcomingWeek",
  OpenHangfireDashboard = "OpenHangfireDashboard",

  GetOwnReservations = "GetOwnReservations",
  GetSomeoneReservations = "GetSomeoneReservations",
  CreateReservation = "CreateReservation",
  CancelReservation = "CancelReservation",
  UpdateReservation = "UpdateReservation",

  GetAllUsers = "GetAllUsers",
  SetUserRole = "SetUserRole",
  DeleteUserAccount = "DeleteUserAccount",
  DeleteOwnAccount = "DeleteOwnAccount",

  OpenAdministrationPage = "OpenAdministrationPage",
  CanSeeAdministrativeContent = "CanSeeAdministrativeContent",

  GetOwnReservationDetails = "GetOwnReservationDetails",
  GetSomeoneReservationDetails = "GetSomeoneReservationDetails",
}
