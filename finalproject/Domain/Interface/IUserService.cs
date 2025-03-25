using finalproject.Application.Users.Commands;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;


namespace finalproject.Domain.Interface
{
    public interface IUserService
    {
        Task <GetUser> GetUsersAsync(string? searchTerm, int pageNumber, int pageSize);
        Task<int> AddUserAsync(User user);
        Task<User> GetUserById(int id);
        Task DeleteUserAsync(User user);
        Task UpdateUserStatusAsync(User user);
    }
}
