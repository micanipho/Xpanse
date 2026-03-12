using server.Dtos;

namespace server.Services.Interfaces
{
    public interface IUser
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<UserResponse> CreateUserAsync(CreateUserRequest user);
        Task<bool> UpdateUserAsync(int id, UpdateUserRequest user);
        Task<bool> DeleteUserAsync(int id);
    }
}
