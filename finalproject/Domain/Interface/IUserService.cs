using finalproject.Application.Users.Commands;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Enums;


namespace finalproject.Domain.Interface
{
    public interface IUserService
    {
        Task <GetUser> GetUsersAsync(string? searchTerm, Role?role, bool?status, int pageNumber, int pageSize);
        Task<int> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task DeleteUserAsync(User user);
        Task UpdateUserStatusAsync(User user);
    }
}
