namespace WorkoutReservation.Infrastructure.Settings;

public sealed class AuthenticationSettings
{
    public const string SectionName = "Authentication";
    public string JwtKey { get; set; }
    public int JwtExpireMinutes { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
}