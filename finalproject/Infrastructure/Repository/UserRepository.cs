using Dapper;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Enums;
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
                var query = "INSERT INTO Users (name, email, role, status) " +
                    "VALUES (@name123, @email, @role, @status) " +
                    "SELECT SCOPE_IDENTITY();";
            var param = new
            {
                name = user.name,
                email = user.email,
                role = user.role,
                status = user.status
            };
            using (var db = _dapperContext.GetConnection()) {
                int userId = await db.ExecuteScalarAsync<int>(query, param);
                return userId;
            }
        }

        

        public async Task<GetUser> GetUsersAsync(string? stringTerm, Role?_role, bool?_status, int pageNumber, int pageSize) {
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
                if (!String.IsNullOrEmpty(stringTerm) || _role.HasValue || _status.HasValue)
                {
                    var queryForRes = "SELECT * FROM Users Where 1=1";

                    var queryForTotalCount = "SELECT COUNT(*) FROM Users Where 1=1";

                    if (!String.IsNullOrEmpty(stringTerm)) {
                        queryForRes += "AND(name LIKE @stringTerm OR email LIKE @stringTerm)";
                        queryForTotalCount += "AND(name LIKE @stringTerm OR email LIKE @stringTerm)";
                    }
                    if (_role.HasValue)
                    {
                        queryForRes += "AND role = @role ";
                        queryForTotalCount += "AND role = @role ";
                    }
                    if (_status.HasValue)
                    {
                        queryForRes += "AND status = @status ";
                        queryForTotalCount += "AND status = @status ";
                    }

                    queryForRes += "ORDER BY id " +
                   "OFFSET ((@pagenumber - 1) * @pagesize) ROWS " +
                   "FETCH NEXT @pagesize ROWS ONLY;";


                    var res2 = await db.QueryAsync<User>(queryForRes, new
                    {
                        pagenumber = pageNumber,
                        pagesize = pageSize,
                        stringTerm = $"%{stringTerm}%",
                        role = _role,
                        status = _status
                    });

                    int totalCount1 = await db.ExecuteScalarAsync<int>(queryForTotalCount, new
                    {
                        stringTerm = $"%{stringTerm}%",
                        role = _role,
                        status = _status
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
        public async Task<User> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE id = @id";
            using (var db = _dapperContext.GetConnection())
            {
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


