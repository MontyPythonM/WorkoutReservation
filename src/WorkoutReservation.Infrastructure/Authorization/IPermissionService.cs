﻿namespace WorkoutReservation.Infrastructure.Authorization;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid? userId);
}