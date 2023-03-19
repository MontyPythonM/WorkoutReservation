﻿using System.Reflection;
using WorkoutReservation.Domain.Attributes;

namespace WorkoutReservation.Domain.Extensions;

public static class EnumExtension
{
    public static string StringValue(this object arg) =>
        arg switch
        {
            Enum argEnum => argEnum.GetStringValue(),
            _ => arg.ToString() ?? string.Empty
        };

    private static string GetStringValue(this Enum arg)
    {
        var stringValueAttribute = arg.GetType().GetMember(arg.ToString())?.FirstOrDefault()?.GetCustomAttribute<StringValueAttribute>();
        return stringValueAttribute?.Value ?? arg.ToString();
    }
}