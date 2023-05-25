namespace WorkoutReservation.Infrastructure.Settings;

public sealed class SqlServerSettings
{
    public const string SectionName = "SqlServer";
    public string ConnectionString { get; set; }
}