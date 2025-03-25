using Dapper;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using finalproject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finalproject.Infrastructure.Repository
{
    public class UserRepository : IUserService
    {
        private readonly DapperContext _dapperContext;
        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddUserAsync(User user)
        {
            using (var db = _dapperContext.GetConnection()) {
                var query = "INSERT INTO Users (name, email, role, status) " +
                    "VALUES (@name, @email, @role, @status) " +
                    "SELECT SCOPE_IDENTITY();";
                int userId = await db.ExecuteScalarAsync<int>(query, new
                {
                    name = user.name,
                    email = user.email,
                    role = user.role,
                    status = user.status
                });
                return userId;
            }
        }

        

        public async Task<GetUser> GetUsersAsync(string? stringTerm, int pageNumber, int pageSize) {
            using (var db = _dapperContext.GetConnection()) {
                var query = "SELECT * FROM Users " +
                    "ORDER BY id " +
                    "OFFSET ((@pagenumber - 1) * @pagesize) ROWS " +
                    "FETCH NEXT @pagesize ROWS ONLY";
                var totalCountQuery = "SELECT COUNT(*) FROM Users;";
                var res = await db.QueryAsync<User>(query, new {
                    pagenumber = pageNumber,
                    pagesize = pageSize

                });
                int _totalCount = await db.ExecuteScalarAsync<int>(totalCountQuery);
                if (!String.IsNullOrEmpty(stringTerm))
                {
                    var queryForRes = "SELECT * FROM Users " +
                        "where name LIKE @stringTerm OR email LIKE @stringTerm " +
                        "ORDER BY id " +
                        "OFFSET ((@pagenumber - 1) * @pagesize) ROWS " +
                        "FETCH NEXT @pagesize ROWS ONLY;";
                    var queryForTotalCount = "SELECT COUNT(*) FROM Users " +
                        "where name LIKE @stringTerm OR email LIKE @stringTerm;";
                    var res2 = await db.QueryAsync<User>(queryForRes, new
                    {
                        pagenumber = pageNumber,
                        pagesize = pageSize,
                        stringTerm = $"%{stringTerm}%"
                    });

                    int totalCount1 = await db.ExecuteScalarAsync<int>(queryForTotalCount, new
                    {
                        stringTerm = $"%{stringTerm}%"
                    });
                    return new GetUser{
                        users = res2,
                        totalCount = totalCount1
                    };
                }
            return new GetUser{
                users = res,
            totalCount = _totalCount
        };
            }
        }
        public async Task<User> GetUserById(int id)
        {
            using (var db = _dapperContext.GetConnection())
            {
                var query = "SELECT * FROM Users WHERE id = @id";
                return await db.QueryFirstOrDefaultAsync<User>(query, new { id = id });
            }
        }

        public async Task DeleteUserAsync(User user)
        {
            using (var db = _dapperContext.GetConnection()) {
                 var query = "DELETE FROM Users WHERE id = @id";
                 await db.ExecuteAsync(query, new { id = user.id });
            }
        }

        public async Task UpdateUserStatusAsync(User user)
        {
            using (var db = _dapperContext.GetConnection()) {
                var query = "UPDATE Users SET status = @status WHERE id = @id";
                await db.ExecuteAsync(query, new { status = user.status, id = user.id });
            }
        }
    }
}
