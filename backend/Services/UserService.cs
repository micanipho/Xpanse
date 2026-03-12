using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos;
using server.Models;
using server.Services.Interfaces;

namespace server.Services
{
    public class UserService(AppDbContext context) : IUser
    {
        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            return await context.Users
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null) return null;

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return new UserResponse
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                CreatedAt = newUser.CreatedAt
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest user)
        {
            var existing = await context.Users.FindAsync(id);
            if (existing is null) return false;

            existing.Name = user.Name;
            existing.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Password))
                existing.Password = user.Password;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null) return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
