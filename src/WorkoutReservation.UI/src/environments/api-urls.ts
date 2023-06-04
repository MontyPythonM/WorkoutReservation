export const apiUrl = {
  instructor: 'instructor/',
  workoutType: {
    baseUrl: 'workout-type/',
    all: 'workout-type/all'
  },
  realWorkout:  'real-workout/',
  repetitiveWorkout: {
    baseUrl: "repetitive-workout/",
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
    getUsers: 'user/users/',
    setUserRoles: 'user/set-user-roles/',
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
    baseUrl: 'workout-type-tag/'
  },
  content: {
    base: 'content/',
    homePage: 'content/home-page/'
  }
}
