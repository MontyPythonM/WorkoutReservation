namespace WorkoutReservation.API.Settings;

public sealed class CorsSettings
{
    public const string SectionName = "Cors";
    public string PolicyName { get; set; }
    public string OriginUrl { get; set; }
    public string[] AllowedMethods { get; set; }
}