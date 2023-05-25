namespace WorkoutReservation.Infrastructure.Settings;

public sealed class SendgridEmailSettings
{
    public const string SectionName = "SendgridEmail";
    public string ApiKey { get; set; }
    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public bool EnableDelivery { get; set; }
    public string DeliveryAddress { get; set; }
}