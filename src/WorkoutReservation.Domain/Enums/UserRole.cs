namespace WorkoutReservation.Domain.Enums
{
    public enum UserRole
    {
        Member = 1,
        Manager,
        Administrator,            
        NotConfirmedMember
    }

    public static class UserRoleString
    {
        public const string Member = "Member";
        public const string Manager = "Manager";
        public const string Administrator = "Administrator";
        public const string AdministratorOrManager = "Administrator, Manager";
    }
}
