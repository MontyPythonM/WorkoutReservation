namespace WorkoutReservation.Infrastructure.Settings;

public sealed class FirstAdministratorSettings
{
    public const string SectionName = "FirstAdministrator";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}