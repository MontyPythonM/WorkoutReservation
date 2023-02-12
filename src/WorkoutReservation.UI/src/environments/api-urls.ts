export const apiUrl = {
  instructor: 'instructor/',
  workoutType: 'workout-type/',
  realWorkout: {
    current: 'real-workout/current-week',
    upcoming: 'real-workout/upcoming-week'
  },
  repetitiveWorkout: {
    generateUpcomingWeek: "repetitive-workout/generate-upcoming-week",
    deleteAll: "repetitive-workout/delete-all"
  },
  reservation: 'reservation/',
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
