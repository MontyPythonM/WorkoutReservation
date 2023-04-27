namespace WorkoutReservation.Infrastructure.Settings;

public sealed class SendgridEmailSettings
{
    public const string SectionName = "SendgridEmail";
    public string ApiKey { get; init; }
    public string FromName { get; init; }
    public string FromAddress { get; init; }
    public bool EnableDelivery { get; init; }
    public string DeliveryAddress { get; init; }
}