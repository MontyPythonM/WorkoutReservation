﻿using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts
{
    public interface IUserRepository
    {
        public Task AddUser(User user);
        public Task<User> GetByEmail(string email);
        public Task<User> GetByGuid(Guid guid);
        public Task<List<User>> GetAllAsync();
    }
}
