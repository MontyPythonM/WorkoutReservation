﻿namespace WorkoutReservation.Infrastructure.Exceptions;

public class ConversionException : Exception
{
    public ConversionException(string message) : base(message)
    {
    }
}