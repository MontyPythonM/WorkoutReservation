namespace WorkoutReservation.Infrastructure.Settings;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireMinutes { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
}