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
  account: {
    register: 'account/register',
    login: 'account/login',
    users: 'account/users'
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
