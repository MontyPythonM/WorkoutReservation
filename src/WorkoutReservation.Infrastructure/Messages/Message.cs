﻿namespace WorkoutReservation.Infrastructure.Messages;

public sealed class Message
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Error { get; set; }
}