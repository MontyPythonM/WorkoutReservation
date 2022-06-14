using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Presistence;
using WorkoutReservation.Infrastructure.Seeders.Data;

namespace WorkoutReservation.Infrastructure.Seeders
{
    public class SeedFirstAdmin
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<SeedFirstAdmin> _logger;
        private readonly FirstAdminSettings _firstAdminSettings;

        public SeedFirstAdmin(AppDbContext dbContext,
                              ILogger<SeedFirstAdmin> logger,
                              FirstAdminSettings firstAdminSettings)
        {
            _dbContext = dbContext;
            _logger = logger;
            _firstAdminSettings = firstAdminSettings;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var anyAdmin = _dbContext.Users.FirstOrDefault(x => x.UserRole == UserRole.administrator);

                if (anyAdmin is null)
                {
                    var firstAdmin = FirstAdminAccount.GerFirstAdmin(_firstAdminSettings);
                    _dbContext.Users.Add(firstAdmin);
                    _dbContext.SaveChanges();
                    _logger.LogWarning("Default administrator was seeded into database.");
                }
            }
            else
            {
                throw new InternalServerError("Cannot connect with database.");
            }
        }
    }
}
