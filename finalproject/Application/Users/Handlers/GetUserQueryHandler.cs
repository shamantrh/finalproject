using finalproject.Application.Users.Queries;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using finalproject.Infrastructure.Repository;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUser>
    {
        private readonly IUserService _userService;
        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<GetUser> Handle(GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            var res = await _userService.GetUsersAsync(getUserQuery._searchTerm, getUserQuery._pageNumber, getUserQuery._pageSize);
            int totalCount = (int)Math.Ceiling((double)res.totalCount / getUserQuery._pageSize);
            return new GetUser
            {
                users = res.users,
                totalCount = totalCount
            };
        }
    }
}
