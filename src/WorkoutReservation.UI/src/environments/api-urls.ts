export const apiUrl = {
  instructor: 'instructor/',
  workoutType: 'workout-type/',
  realWorkout:  'real-workout/',
  repetitiveWorkout: {
    generateUpcomingWeek: "repetitive-workout/generate-upcoming-week",
    deleteAll: "repetitive-workout/delete-all"
  },
  reservation: {
    getOwn: "reservation/own",
    getAll: "reservation/all",
    getDetails: "reservation/details",
    create: "reservation",
    updateReservation: "reservation/edit-reservation",
    cancel: "reservation/cancel-reservation"
  },
  user: {
    users: 'user/users/',
    setUserRole: 'user/set-user-role/',
    deleteUser: 'user/delete-user/',
  },
  account: {
    register: 'account/register',
    login: 'account/login',
    users: 'account/users',
    currentUser: 'account/current-user'
  },
  workoutTypeTag: {
    onlyActive: 'workout-type-tag/only-active',
    all: 'workout-type-tag/',
    create: "workout-type-tag/",
    deactivate: "workout-type-tag/",
    delete: "workout-type-tag/",
    update: "workout-type-tag/"
  }
}
